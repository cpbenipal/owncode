(function () {
    appModule.controller('tenant.views.settings.index',[
        '$scope', '$uibModal', 'abp.services.app.tenantSettings', 'appSession', 'FileUploader',
        function ($scope, $uibModal , tenantSettingsService, appSession, fileUploader) {
                var vm = this;

                var usingDefaultTimeZone = false;
                var initialTimeZone = null;

                vm.logoUploader = null;
                vm.customCssUploader = null;

                $scope.$on('$viewContentLoaded',
                    function () {
                        App.initAjax();
                    });

                vm.isMultiTenancyEnabled = abp.multiTenancy.isEnabled;
                vm.showTimezoneSelection = abp.clock.provider.supportsMultipleTimezone;
                vm.activeTabIndex = (!vm.isMultiTenancyEnabled || vm.showTimezoneSelection) ? 0 : 1;
                vm.loading = false;
                vm.settings = null;
                vm.tenant = appSession.tenant;

                vm.logoUploader = undefined;
                vm.customCssUploader = undefined;

                vm.getSettings = function () {
                    vm.loading = true;
                    tenantSettingsService.getAllSettings()
                        .then(function (result) {
                            vm.settings = result.data;
                            initialTimeZone = vm.settings.general.timezone;
                            usingDefaultTimeZone = vm.settings.general.timezoneForComparison ===
                                abp.setting.values["Abp.Timing.TimeZone"];
                        }).finally(function () {
                            vm.loading = false;
                        });
                };

                $scope.currencyList = []; //list of Currencies
                vm.getCurrency = function () {
                    tenantSettingsService.getCurrencies()
                        .then(function (ins_obj) {
                            angular.forEach(ins_obj.data, function (insvalue, key1) {
                                $scope.currencyList.push({
                                    name: insvalue.currencyType,
                                    id: insvalue.currencyCode
                                });
                            });

                        }).finally(function () {
                            vm.loading = false;
                        });
                };

                $scope.countriesList = []; //list of Countries
                vm.getCountry = function () {
                    tenantSettingsService.getCountries()
                        .then(function (ins_obj) {
                            angular.forEach(ins_obj.data, function (insvalue, key1) {
                                $scope.countriesList.push({
                                    name: insvalue.country,
                                    id: insvalue.code
                                });
                            });

                        }).finally(function () {
                            vm.loading = false;
                        });
                };

                $scope.timezoneList = []; //list of timezones
                vm.getTimezone = function () {
                    tenantSettingsService.getTimeZones()
                        .then(function (ins_obj) {
                            angular.forEach(ins_obj.data, function (insvalue, key1) {
                                $scope.timezoneList.push({
                                    name: insvalue.displayName,
                                    id: insvalue.id
                                });
                            });

                        }).finally(function () {
                            vm.loading = false;
                        });
                };

                $scope.plansList = []; //list of timezones
                vm.getPlans = function () {
                    tenantSettingsService.getSignOnPlans()
                        .then(function (ins_obj) {
                            angular.forEach(ins_obj.data, function (insvalue, key1) {
                                $scope.plansList.push({
                                    name: insvalue.planName,
                                    id: insvalue.id
                                });
                            });

                        }).finally(function () {
                            vm.loading = false;
                        });
                };

                function initUploaders() {
                    vm.logoUploader = createUploader(
                        "TenantCustomization/UploadLogo",
                        [{
                            name: 'imageFilter',
                            fn: function (item, options) {
                                var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
                                if ('|jpg|jpeg|png|gif|'.indexOf(type) === -1) {
                                    abp.message.warn(app.localize('UploadLogo_Info'));
                                    return false;
                                }

                                if (item.size > 30720) //30KB
                                {
                                    abp.message.warn(app.localize('UploadLogo_Info'));
                                    return false;
                                }

                                return true;
                            }
                        }],
                        function (result) {
                            appSession.tenant.logoFileType = result.fileType;
                            appSession.tenant.logoId = result.id;
                            $('#LogoFileInput').val(null);
                        }
                    );

                    vm.customCssUploader = createUploader(
                        "TenantCustomization/UploadCustomCss",
                        null,
                        function (result) {
                            appSession.tenant.customCssId = result.id;
                            $('#TenantCustomCss').remove();
                            $('head').append('<link id="TenantCustomCss" href="' + abp.appPath + 'TenantCustomization/GetCustomCss?id=' + appSession.tenant.customCssId + '" rel="stylesheet"/>');
                            $('#CustomCssFileInput').val(null);
                        }
                    );
                }

                function createUploader(url, filters, success) {
                    var uploader = new fileUploader({
                        url: abp.appPath + url,
                        headers: {
                            "X-XSRF-TOKEN": abp.security.antiForgery.getToken()
                        },
                        queueLimit: 1,
                        removeAfterUpload: true
                    });

                    if (filters) {
                        uploader.filters = filters;
                    }

                    uploader.onSuccessItem = function (item, ajaxResponse, status) {
                        if (ajaxResponse.success) {
                            abp.notify.info(app.localize('SavedSuccessfully'));
                            success && success(ajaxResponse.result);
                        } else {
                            abp.message.error(ajaxResponse.error.message);
                        }
                    };

                    return uploader;
                }

                vm.uploadLogo = function () {
                    vm.logoUploader.uploadAll();
                };

                vm.uploadCustomCss = function () {
                    vm.customCssUploader.uploadAll();
                };

                vm.clearLogo = function () {
                    tenantSettingsService.clearLogo().then(function () {
                        appSession.tenant.logoFileType = null;
                        appSession.tenant.logoId = null;
                        abp.notify.info(app.localize('ClearedSuccessfully'));
                        $('#LogoFileInput').val(null);
                    });
                }

                vm.clearCustomCss = function () {
                    tenantSettingsService.clearCustomCss().then(function () {
                        appSession.tenant.customCssId = null;
                        $('#TenantCustomCss').remove();
                        abp.notify.info(app.localize('ClearedSuccessfully'));
                        $('#CustomCssFileInput').val(null);
                    });
                }

                vm.saveAll = function () {
                    tenantSettingsService.updateAllSettings(
                        vm.settings
                    ).then(function () {
                        abp.notify.info(app.localize('SavedSuccessfully'));

                        if (abp.clock.provider.supportsMultipleTimezone &&
                            usingDefaultTimeZone &&
                            initialTimeZone !== vm.settings.general.timezone) {
                            abp.message.info(app.localize('TimeZoneSettingChangedRefreshPageNotification'))
                                .done(function () {
                                    window.location.reload();
                                });
                        }
                    });
                };

                vm.confirmcompany = function () { 
                    openEditCompany(null); 
                };

                function openEditCompany(tenantId) { 
                    var modalInstance = $uibModal.open({
                        templateUrl: '~/App/tenant/views/settings/editCompany.cshtml',
                        controller: 'tenant.views.settings.editCompany as vm',
                        backdrop: 'static',
                        resolve: {
                            tenantId: function () {
                                return tenantId;
                            }
                        }
                    });

                    modalInstance.result.then(function (result) {
                        vm.getCompany();
                    });
                }

                vm.confirmregister = function () { 
                    openEditRegister(null); 
                };

                function openEditRegister(tenantId) { 
                    var modalInstance = $uibModal.open({
                        templateUrl: '~/App/tenant/views/settings/editRegister.cshtml',
                        controller: 'tenant.views.settings.editRegister as vm',
                        backdrop: 'static',
                        resolve: {
                            tenantId: function () {
                                return tenantId;
                            }
                        }
                    });

                    modalInstance.result.then(function (result) {
                        vm.getRegister();
                    });
                }
                
                vm.changeLogo = function () { 
                    openUploaded(null); 
                };
                
                function openUploaded(tenantId) { 
                alert('tenantId');
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/tenant/views/settings/changeLogo.cshtml',
                    controller: 'tenant.views.settings.changeLogo as vm',
                    backdrop: 'static',
                    resolve: {
                        tenantId: function () {
                            return tenantId;
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getCompany();
                });
                }      


                vm.getCompany = function () {
                    vm.loading = true;
                    tenantSettingsService.getCompanyInfo()
                        .then(function (result) {
                            vm.settings.company = result.data;
                        }).finally(function () {
                            vm.loading = false;
                        });
                };
                vm.getRegister = function () {
                    vm.loading = true;
                    tenantSettingsService.getRegisteredInfo()
                        .then(function (result) {
                            vm.settings.register = result.data;
                        }).finally(function () {
                            vm.loading = false;
                        });
                };          

               

                vm.getSettings();                                            
                vm.getCompany();                 
                vm.getRegister();
                initUploaders();
            }
        ]);
})();