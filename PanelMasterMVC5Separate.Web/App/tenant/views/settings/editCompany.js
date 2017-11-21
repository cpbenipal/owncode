(function () {
    appModule.controller('tenant.views.settings.editCompany', [
        '$scope', '$uibModalInstance', 'abp.services.app.tenantSettings', 'tenantId',
        function ($scope, $uibModalInstance, tenantSettingsService, tenantId) {
            var vm = this;
           
            vm.saving = false;                 
            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            vm.save = function () { 

                bootbox.prompt(app.localize('EnterOTP'), function (result) {
                    if (result === "00-00") {
                        vm.saving = true;
                        tenantSettingsService.updateTenantCompany(vm.company).then(function () {
                            abp.notify.info(app.localize('SavedSuccessfully'));
                            $uibModalInstance.close();
                        }).finally(function () {
                            vm.saving = false;
                        });

                    }
                    else {
                        abp.notify.info(app.localize('OTPMismatched'));
                    }
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
            function init() { 
                tenantSettingsService.getCompanyInfo().then(function (result) {
                    vm.company = result.data;                     
                });
            }

            init();
            vm.getCountry();
            vm.getTimezone();
            vm.getCurrency(); 
        }
    ]);
})();