(function () {

    appModule.controller('tenant.views.adminforms.index', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.functions',
        function ($scope, $uibModal, $stateParams, uiGridConstants, userService) {
            var vm = this;
            vm.job = {};
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.loadingcc = false;
            vm.loadingccb = false;
            vm.loadingsp = false;
            vm.advancedFiltersAreShown = false;
            vm.filterText = $stateParams.filterText || '';
            vm.currentUserId = abp.session.userId;

            vm.permissions = {
                create: abp.auth.hasPermission('Pages.Administration.Users.Create'),
                edit: abp.auth.hasPermission('Pages.Administration.Users.Edit'),
                changePermissions: abp.auth.hasPermission('Pages.Administration.Users.ChangePermissions'),
                impersonation: abp.auth.hasPermission('Pages.Administration.Users.Impersonation'),
                'delete': abp.auth.hasPermission('Pages.Administration.Users.Delete'),
                roles: abp.auth.hasPermission('Pages.Administration.Roles')
            };

            vm.requestParams = {
                permission: '',
                role: '',
                skipCount: 0,
                maxResultCount: app.consts.grid.defaultPageSize,
                sorting: null
            };

            vm.userGridOptions = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableVerticalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                paginationPageSizes: app.consts.grid.defaultPageSizes,
                paginationPageSize: app.consts.grid.defaultPageSize,
                useExternalPagination: true,
                useExternalSorting: true,
                appScopeProvider: vm,
                rowTemplate: '<div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader, \'text-muted\': !row.entity.isActive }"  ui-grid-cell></div>',
                columnDefs: [
                    {
                        name: app.localize('Actions'),
                        enableSorting: false,
                        width: 120,
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '<a ng-click="grid.appScope.saveDescription(row.entity)">' + app.localize('Update') + '</a></li>' +
                        '</div>'
                    },
                    {
                        name: app.localize('Description'),
                        enableSorting: true,
                        width: 300,
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '<input ng-model="row.entity.description" type="text">' +
                        '</div>'
                    }
                    , {
                        name: app.localize('Status'),
                        field: 'enabled',
                        cellTemplate:
                        '<div ng-show="row.entity.showSwitch" class=\"ui-grid-cell-contents\">' +
                        '<div ng-show="row.entity.enabled" ng-click="grid.appScope.Status(row.entity)" class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-mini bootstrap-switch-id-test{{row.entity.id}} bootstrap-switch-animate bootstrap-switch-on" style="width: 66px;"><div class="bootstrap-switch-container" style="width: 96px; margin-left: 0px;">' +
                        '<span class="bootstrap-switch-handle-on bootstrap-switch-primary" style="width: 32px;"> ON</span>' +
                        '<span class="bootstrap-switch-label" style="width: 32px;">&nbsp;</span>' +
                        '<span class="bootstrap-switch-handle-off bootstrap-switch-default" style="width: 32px;">OFF</span>' +
                        '<input ng-checked="row.entity.enabled" ng-model="row.entity.enabled"  class="make-switch" data-size="mini" type="checkbox"></div></div>' +
                        '<div ng-show="!row.entity.enabled" ng-click="grid.appScope.Status(row.entity)" class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-mini bootstrap-switch-id-test{{row.entity.id}} bootstrap-switch-animate bootstrap-switch-off" style="width: 66px;"><div class="bootstrap-switch-container" style="width: 96px; margin-left: -32px;"><span class="bootstrap-switch-handle-on bootstrap-switch-primary" style="width: 32px;">ON</span><span class="bootstrap-switch-label" style="width: 32px;">&nbsp;</span><span class="bootstrap-switch-handle-off bootstrap-switch-default" style="width: 32px;">OFF</span>' +
                        '</div></div>' +
                        '</div>',
                        minWidth: 10
                    }
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    $scope.gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
                        if (!sortColumns.length || !sortColumns[0].field) {
                            vm.requestParams.sorting = null;
                        } else {
                            vm.requestParams.sorting = sortColumns[0].field + ' ' + sortColumns[0].sort.direction;
                        }

                        vm.getData();
                    });
                    gridApi.pagination.on.paginationChanged($scope, function (pageNumber, pageSize) {
                        vm.requestParams.skipCount = (pageNumber - 1) * pageSize;
                        vm.requestParams.maxResultCount = pageSize;

                        vm.getData();
                    });
                },
                data: []
            };

            vm.getData = function () {
                vm.loading = true;
                var index = $scope.vm.tables;

                if (index != undefined)
                    $scope.IsVisible = true;
                else
                    $scope.IsVisible = false;

                userService.getMasterRecords($.extend({ filter: vm.filterText, tableIndex: index }, vm.requestParams))
                    .then(function (result) {
                        vm.userGridOptions.totalItems = result.data.totalCount;
                        vm.userGridOptions.data = addRoleNamesField(result.data.items);
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            function addRoleNamesField(users) {
                for (var i = 0; i < users.length; i++) {
                    var user = users[i];
                    user.getRoleNames = function () {
                        var roleNames = '';
                        for (var j = 0; j < this.roles.length; j++) {
                            if (roleNames.length) {
                                roleNames = roleNames + ', ';
                            }
                            roleNames = roleNames + this.roles[j].roleName;
                        };

                        return roleNames;
                    }
                }

                return users;
            }

            vm.impersonate = function (user) {
                app.utils.removeCookie(abp.security.antiForgery.tokenCookieName);
                abp.ajax({
                    url: abp.appPath + 'Account/Impersonate',
                    data: JSON.stringify({
                        tenantId: abp.session.tenantId,
                        userId: user.id
                    })
                });
            };

            vm.addnew = function () {
                if ($scope.vm.newdescription != undefined) {
                    var newjob = {};
                    newjob.id = 0;
                    newjob.description = $scope.vm.newdescription;
                    vm.saveDescription(newjob);
                } else {
                    alert("Fill all fields");
                }
            };

            vm.saveDescription = function (primeryKey) {
                vm.saving = true;
                vm.job.tableIndex = $scope.vm.tables;
                vm.job.id = primeryKey.id;
                vm.job.description = primeryKey.description;
                userService.createOrUpdateDescription(vm.job).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $uibModalInstance.close()
                }).finally(function () {
                    vm.saving = false;
                    vm.getData();
                });

            };

            vm.Status = function (quote) {
                abp.message.confirm(
                    app.localize('AreYouSure', quote.description),
                    function (isConfirmed) {
                        if (isConfirmed) {
                            
                                userService.changeStatus({
                                    id: quote.id,
                                    isActive: !quote.isActive,
                                    tableIndex: $scope.vm.tables
                                }).then(function () {
                                    vm.getData();
                                    if (!quote.isActive)
                                        abp.notify.success(app.localize('SuccessfullyEnabled'));
                                    else
                                        abp.notify.warn(app.localize('SuccessfullyDisabled'));
                                });                            
                        }
                    }
                );
            };
            $scope.tableList = []; //list of masks
            vm.getTables = function () {
                $scope.tableList.push({
                    name: "Roles Category",
                    id: 1
                });
                $scope.tableList.push({
                    name: "Repair Type",
                    id: 2
                });
                $scope.tableList.push({
                    name: "Quote Status",
                    id: 3
                });
                $scope.tableList.push({
                    name: "Job status",
                    id: 4
                });
                //$scope.tableList.push({
                //    name: "Country",
                //    id: 5
                //});
                //$scope.tableList.push({
                //    name: "Currency",
                //    id: 6
                //});
                //$scope.tableList.push({
                //    name: "JobstatusMask",
                //    id: 7
                //});
                //$scope.tableList.push({
                //    name: "Banks",
                //    id: 8
                //});
                //$scope.tableList.push({
                //    name: "SignonPlans",
                //    id: 9
                //});
                $scope.tableList.push({
                    name: "JobstatusMask",
                    id: 10
                });
            };


            $scope.tblDataList = []; //list of masks
            vm.getCodes = function () {
                $scope.tblDataList.push({
                    name: "Currencies",
                    id: 1
                });
                $scope.tblDataList.push({
                    name: "Countries",
                    id: 2
                });
            };

            $scope.countryList = []; //list of Banks
            vm.getCountries = function () {

                userService.getCountry()
                    .then(function (ins_obj) {

                        angular.forEach(ins_obj.data.items, function (insvalue, key1) {
                            $scope.countryList.push({
                                name: insvalue.country + " (" + insvalue.code + ")",
                                id: insvalue.id
                            });
                        });

                    }).finally(function () {
                        vm.loadingccb = false;
                    });
            };



            vm.getCurrencies = function () {
                vm.loadingcc = true;
                var index = $scope.vm.countrycurrrency;

                if (index != undefined)
                    $scope.IsthisVisible = true;
                else
                    $scope.IsthisVisible = false;

                userService.getCountryOrCurrency($.extend({ filter: vm.filterText, tableIndex: index }, vm.requestParams))
                    .then(function (result) {
                        vm.currencyGridOptions.totalItems = result.data.totalCount;
                        vm.currencyGridOptions.data = addRoleNamesField(result.data.items);
                    }).finally(function () {
                        vm.loadingcc = false;
                    });
            };
            vm.addnewcode = function () {
                if ($scope.vm.description != undefined && $scope.vm.code != undefined) {
                    var newjob = {};
                    newjob.id = 0;
                    newjob.description = $scope.vm.description;
                    newjob.code = $scope.vm.code;
                    vm.saveCurrency(newjob);
                }
            };
            vm.saveCurrency = function (primeryKey) {
                vm.saving = true;
                vm.job.tableIndex = $scope.vm.countrycurrrency;
                vm.job.id = primeryKey.id;
                vm.job.description = primeryKey.description;
                vm.job.code = primeryKey.code;
                userService.createOrUpdateCodes(vm.job).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $uibModalInstance.close()
                }).finally(function () {
                    vm.saving = false;
                    vm.getCurrencies();
                });
            };

            vm.currencyGridOptions = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableVerticalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                paginationPageSizes: app.consts.grid.defaultPageSizes,
                paginationPageSize: app.consts.grid.defaultPageSize,
                useExternalPagination: true,
                useExternalSorting: true,
                appScopeProvider: vm,
                rowTemplate: '<div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader, \'text-muted\': !row.entity.isActive }"  ui-grid-cell></div>',
                columnDefs: [
                    {
                        name: app.localize('Actions'),
                        enableSorting: false,
                        width: 120,
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '<a ng-click="grid.appScope.saveCurrency(row.entity)">' + app.localize('Update') + '</a></li>' +
                        '</div>'
                    },
                    {
                        name: app.localize('Code'),
                        enableSorting: true,
                        width: 100,
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '<input ng-model="row.entity.code" type="text">' +
                        '</div>'
                    },
                    ,
                    {
                        name: app.localize('Description'),
                        enableSorting: true,
                        width: 300,
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '<input ng-model="row.entity.description" type="text">' +
                        '</div>'
                    }
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    $scope.gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
                        if (!sortColumns.length || !sortColumns[0].field) {
                            vm.requestParams.sorting = null;
                        } else {
                            vm.requestParams.sorting = sortColumns[0].field + ' ' + sortColumns[0].sort.direction;
                        }

                        vm.getCurrencies();
                    });
                    gridApi.pagination.on.paginationChanged($scope, function (pageNumber, pageSize) {
                        vm.requestParams.skipCount = (pageNumber - 1) * pageSize;
                        vm.requestParams.maxResultCount = pageSize;

                        vm.getCurrencies();
                    });
                },
                data: []
            };


            // Banks
            vm.getAllBanks = function () {
                vm.loadingbank = true;
                userService.getBanks($.extend({ filter: vm.filterText }, vm.requestParams))
                    .then(function (result) {
                        vm.bankGridOptions.totalItems = result.data.totalCount;
                        vm.bankGridOptions.data = addRoleNamesField(result.data.items);
                    }).finally(function () {
                        vm.loadingccb = false;
                    });
            };

            vm.addnewbank = function () {
                if ($scope.vm.newbank != "" && $scope.vm.countryCode != "") {
                    var newjob = {};
                    newjob.id = 0;
                    newjob.bankName = $scope.vm.newbank;
                    $scope.vm.newbank = "";                    
                    newjob.countryCode = $scope.vm.country;
                    $scope.vm.country = "";
                    vm.saveBank(newjob);
                } else {
                    alert("Fill all fields");
                }
            };

            vm.saveBank = function (primeryKey) {
             
                vm.saving = true;
                vm.job.id = primeryKey.id;
                vm.job.description = primeryKey.bankName;
                vm.job.countryID = primeryKey.countryCode;
                userService.createOrUpdateBank(vm.job).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $uibModalInstance.close()
                }).finally(function () {
                    vm.saving = false;
                    vm.getAllBanks();
                });
            };

            vm.bankGridOptions = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableVerticalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                paginationPageSizes: app.consts.grid.defaultPageSizes,
                paginationPageSize: app.consts.grid.defaultPageSize,
                useExternalPagination: true,
                useExternalSorting: true,
                appScopeProvider: vm,
                rowTemplate: '<div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader, \'text-muted\': !row.entity.isActive }"  ui-grid-cell></div>',
                columnDefs: [
                    {
                        name: app.localize('Actions'),
                        enableSorting: false,
                        width: 120,
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '<a ng-click="grid.appScope.saveBank(row.entity)">' + app.localize('Update') + '</a></li>' +
                        '</div>'
                    },
                    { 
                         name: app.localize('CountryCode'),
                        enableSorting: true,
                        width: 100,
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '<input ng-model="row.entity.countryCode" type="text">' +
                        '</div>'
                    },
                    {
                        name: app.localize('BankName'),
                        enableSorting: true,
                        width: 300,
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '<input ng-model="row.entity.bankName" type="text">' +
                        '</div>'
                    },

                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    $scope.gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
                        if (!sortColumns.length || !sortColumns[0].field) {
                            vm.requestParams.sorting = null;
                        } else {
                            vm.requestParams.sorting = sortColumns[0].field + ' ' + sortColumns[0].sort.direction;
                        }

                        vm.getAllBanks();
                    });
                    gridApi.pagination.on.paginationChanged($scope, function (pageNumber, pageSize) {
                        vm.requestParams.skipCount = (pageNumber - 1) * pageSize;
                        vm.requestParams.maxResultCount = pageSize;

                        vm.getAllBanks();
                    });
                },
                data: []
            };


            // SignOnPlans
             
            vm.getSPlans = function () {
                vm.loadingbank = true;
                userService.getSignOnPlans($.extend({ filter: vm.filterText }, vm.requestParams))
                    .then(function (result) {
                        vm.plansGridOptions.totalItems = result.data.totalCount;
                        vm.plansGridOptions.data = addRoleNamesField(result.data.items);
                    }).finally(function () {
                        vm.loadingsp = false;
                    });
            };

            vm.addnewplan = function () {
                if ($scope.vm.planname != "" && $scope.vm.price != "" && $scope.vm.headercolor != "" && $scope.vm.members != "") {
                    var newjob = {};
                    newjob.id = 0;
                    newjob.planName = $scope.vm.planname;
                    $scope.vm.planname = "";
                    newjob.price = $scope.vm.price;
                    $scope.vm.price = "";
                    newjob.headerColor = $scope.vm.headercolor;
                    $scope.vm.headercolor = "";
                    newjob.members = $scope.vm.members;
                    $scope.vm.members = "";
                    
                    vm.savePlan(newjob);
                }
                else {
                    alert("Fill all fields");
                }
            };

            vm.savePlan = function (primeryKey) {                
                vm.saving = true;
                vm.job.id = primeryKey.id;
                vm.job.planName = primeryKey.planName;
                vm.job.price = primeryKey.price;
                vm.job.headerColor = primeryKey.headerColor;
                vm.job.members = primeryKey.members;               
                userService.createOrUpdateSignOnPlan(vm.job).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $uibModalInstance.close()
                }).finally(function () {
                    vm.saving = false;
                    vm.getSPlans();
                });
            };

            vm.plansGridOptions = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableVerticalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                paginationPageSizes: app.consts.grid.defaultPageSizes,
                paginationPageSize: app.consts.grid.defaultPageSize,
                useExternalPagination: true,
                useExternalSorting: true,
                appScopeProvider: vm,
                rowTemplate: '<div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader, \'text-muted\': !row.entity.isActive }"  ui-grid-cell></div>',
                columnDefs: [
                    {
                        name: app.localize('Actions'),
                        enableSorting: false,
                        width: 70,
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '<a ng-click="grid.appScope.savePlan(row.entity)">' + app.localize('Update') + '</a></li>' +
                        '</div>'
                    },
                    {
                        name: app.localize('PlanName'),
                        enableSorting: true,
                        width: 200,
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '<input ng-model="row.entity.planName" type="text">' +
                        '</div>'
                    },
                    {
                        name: app.localize('Price'),
                        enableSorting: true,
                        width: 100,
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '<input ng-model="row.entity.price" type="text">' +
                        '</div>'
                    },
                    {
                        name: app.localize('HeaderColor'),
                        enableSorting: true,
                        width: 200,
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '<input ng-model="row.entity.headerColor" type="text">' +
                        '</div>'
                    },
                    {
                        name: app.localize('Members'),
                        enableSorting: true,
                        width: 100,
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '<input ng-model="row.entity.members" type="text">' +
                        '</div>'
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    $scope.gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
                        if (!sortColumns.length || !sortColumns[0].field) {
                            vm.requestParams.sorting = null;
                        } else {
                            vm.requestParams.sorting = sortColumns[0].field + ' ' + sortColumns[0].sort.direction;
                        }

                        vm.getSPlans();
                    });
                    gridApi.pagination.on.paginationChanged($scope, function (pageNumber, pageSize) {
                        vm.requestParams.skipCount = (pageNumber - 1) * pageSize;
                        vm.requestParams.maxResultCount = pageSize;

                        vm.getSPlans();
                    });
                },
                data: []
            };


            vm.getCountries();
            vm.getCodes();
            vm.getAllBanks();
            vm.getTables();
            vm.getSPlans();
        }]);
})();