(function () {

    appModule.controller('host.views.Banks.index', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.systemDefaults',
        function ($scope, $uibModal, $stateParams, uiGridConstants, userService) {

            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
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
                        '  <div class="btn-group dropdown" uib-dropdown="" dropdown-append-to-body>' +
                        '    <button class="btn btn-xs btn-primary blue" uib-dropdown-toggle="" aria-haspopup="true" aria-expanded="false"><i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span></button>' +
                        '    <ul uib-dropdown-menu>' +                         
                        '      <li><a ng-if="grid.appScope.permissions.edit" ng-click="grid.appScope.editBank(row.entity)">' + app.localize('Edit') + '</a></li>' +
                        '    </ul>' +
                        '  </div>' +
                        '</div>'
                    },                   
                    { 
                        name: app.localize('BankName'),
                        enableSorting: true,
                        field: 'bankName',
                        minWidth: 140
                    },
                    {
                        name: app.localize('CountryCode'),
                        enableSorting: true,
                        field: 'countryCode',
                        minWidth: 100
                    },
                    {
                        name: app.localize('CreationTime'),
                        field: 'creationTime',
                        cellFilter: 'momentFormat: \'L\'',
                        minWidth: 50
                    }
                    //,
                    //{
                    //    name: app.localize('Status'),
                    //    field: 'isActive',
                    //    cellTemplate:
                    //    '<div class=\"ui-grid-cell-contents\">' +
                    //    '<div ng-show="row.entity.isActive" ng-click="grid.appScope.Status(row.entity)" class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-mini bootstrap-switch-id-test{{row.entity.makeID}} bootstrap-switch-animate bootstrap-switch-on" style="width: 66px;"><div class="bootstrap-switch-container" style="width: 96px; margin-left: 0px;">' +
                    //    '<span class="bootstrap-switch-handle-on bootstrap-switch-primary" style="width: 32px;"> ON</span>' +
                    //    '<span class="bootstrap-switch-label" style="width: 32px;">&nbsp;</span>' +
                    //    '<span class="bootstrap-switch-handle-off bootstrap-switch-default" style="width: 32px;">OFF</span>' +
                    //    '<input ng-checked="row.entity.isActive" ng-model="row.entity.isActive"  class="make-switch" data-size="mini" type="checkbox"></div></div>' +
                    //    '<div ng-show="!row.entity.isActive" ng-click="grid.appScope.Status(row.entity)" class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-mini bootstrap-switch-id-test{{row.entity.makeID}} bootstrap-switch-animate bootstrap-switch-off" style="width: 66px;"><div class="bootstrap-switch-container" style="width: 96px; margin-left: -32px;"><span class="bootstrap-switch-handle-on bootstrap-switch-primary" style="width: 32px;">ON</span><span class="bootstrap-switch-label" style="width: 32px;">&nbsp;</span><span class="bootstrap-switch-handle-off bootstrap-switch-default" style="width: 32px;">OFF</span>' +
                    //    '</div></div>' +
                    //    '</div>',
                    //    minWidth: 50
                    //}
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

            vm.getAllBanks = function () {

                vm.loading = true;

                userService.getBanks($.extend({ filter: vm.filterText }, vm.requestParams))
                    .then(function (result) {
                        vm.userGridOptions.totalItems = result.data.totalCount;
                        vm.userGridOptions.data = addRoleNamesField(result.data.items);
                    }).finally(function () {
                        vm.loading = false;
                    });

            };

            vm.editBank = function (bank) {
                openCreateOrEditBankModal(bank.id);
            };

            vm.createBank = function () {
                openCreateOrEditBankModal(null);
            };
            if ($stateParams.so =="addnew") {
                openCreateOrEditBankModal(null);
            }
            function openCreateOrEditBankModal(bankId) {
                
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/host/views/Banks/createOrEditModal.cshtml',
                    controller: 'host.views.Banks.createOrEditModal as vm',
                    backdrop: 'static',
                    resolve: {
                        bankId: function () {
                            return bankId;
                        }
                    }
                });
                modalInstance.result.then(function (result) {
                    vm.getAllBanks();
                });
            }
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
            };             
            vm.exportToExcel = function () {
                userService.getBanksExcel({})
                    .then(function (result) {
                        app.downloadTempFile(result.data);
                    });
            };
            vm.getAllBanks();

        }]);
})();