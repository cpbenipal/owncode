(function () {

    appModule.controller('tenant.views.viewQuotations.index', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.quote',
        function ($scope, $uibModal, $stateParams, uiGridConstants, jobService) {

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
                        '      <li><a ng-click="grid.appScope.newQuotation(row.entity)">' + app.localize('NewQuotation') + '</a></li>' +
                        '    </ul>' +
                        '  </div>' +
                        '</div>'
                    },                     
                    {
                        name: app.localize('VehicleRegistration'),
                        field: 'job', 
                        minWidth: 140
                    },
                    {
                        name: app.localize('QuoteCategory'),
                        field: 'quoteCat',
                        minWidth: 120
                    },
                    {
                        name: app.localize('QuoteStatus'),
                        field: 'quoteStatus',
                        minWidth: 120
                    },
                    {
                        name: app.localize('RepairType'),
                        field: 'repairType',
                        minWidth: 120
                    },
                    {
                        name: app.localize('CreationTime'),
                        field: 'creationTime',
                        cellFilter: 'momentFormat: \'L\'',
                        minWidth: 50
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

                        vm.getQuotes();
                    });
                    gridApi.pagination.on.paginationChanged($scope, function (pageNumber, pageSize) {
                        vm.requestParams.skipCount = (pageNumber - 1) * pageSize;
                        vm.requestParams.maxResultCount = pageSize;

                        vm.getQuotes();
                    });
                },
                data: []
            };

            vm.getQuotes = function () {

                vm.loading = true;

                jobService.viewQuotations($.extend({ filter: vm.filterText }, vm.requestParams))
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
            };
 
            vm.newQuotation = function (quote) {
                openCreateQuoteModal(quote.jobId);
            };
             
            function openCreateQuoteModal(jobId) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/tenant/views/viewQuotations/createOrEditModal.cshtml',
                    controller: 'tenant.views.quoting.createModal as vm',
                    backdrop: 'static',
                    resolve: {
                        jobId: function () {                         
                            return jobId;
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getQuotes();
                });
            }

            vm.getQuotes();
        }]);
})();