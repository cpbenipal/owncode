﻿(function () {

    appModule.controller('tenant.views.jobdetails.index', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.branchClaim',
        function ($scope, $uibModal, $stateParams, uiGridConstants, userService) {
            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.advancedFiltersAreShown = false;
            vm.filterText = $stateParams.filterText || '';
            vm.currentUserId = abp.session.userId;
            vm.TenantId = abp.session.tenantId;

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
                        '      <li><a ng-if="grid.appScope.permissions.edit" ng-href="#!/tenant/job_landing/job_information/{{row.entity.id}}">' + app.localize('Open') + '</a></li>' +
                        '      <li><a ng-click="grid.appScope.newQuotation(row.entity)">' + app.localize('AddnewQuote') + '</a></li>' +
                        '    </ul>' +
                        '  </div>' +
                        '</div>'
                    },
                    {
                        name: app.localize('JobNumber'),
                         enableSorting: true,
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">R0'+vm.TenantId+'-000{{row.entity.id}}</div>' 
                    },
                    {
                        name: app.localize('RegNo'),
                        field: 'regNo' 
                    },
                    {
                        name: app.localize('JobStatus'),
                        field: 'jobStatusDesc' 
                    }, 
                    {
                        name: app.localize('BranchEntryMethod '),
                        field: 'branchEntryMethod' 
                    },                                     
                    {
                        name: app.localize('insurance'),
                        field: 'insurance' 
                    },
                    {
                        name: app.localize('Broker'),
                        field: 'broker' 
                    },
                    {
                        name: app.localize('ShopAllocation'),
                        field: 'shopAllocation' 
                    },
                    {
                        name: app.localize('CreationTime'),
                        field: 'creationTime',
                        cellFilter: 'momentFormat: \'L\'' 
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
                       
                        vm.getUsers();
                    });
                    gridApi.pagination.on.paginationChanged($scope, function (pageNumber, pageSize) {
                        vm.requestParams.skipCount = (pageNumber - 1) * pageSize;
                        vm.requestParams.maxResultCount = pageSize;
                       
                        vm.getUsers();
                    });
                },
                data: []
            };

            vm.newQuotation = function (quote) {
                openCreateQuoteModal(quote.id);
            };

            function openCreateQuoteModal(jobId) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/tenant/views/viewQuotations/createOrEditModal.cshtml',
                    controller: 'tenant.views.viewQuotations.createModal as vm',
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
            
            vm.getUsers = function () {
               
                vm.loading = true;
                userService.getClaims($.extend({ filter: vm.filterText }, vm.requestParams))
                    .then(function (result) {                       
                        vm.userGridOptions.totalItems = result.data.totalCount;
                        vm.userGridOptions.data = addRoleNamesField(result.data.items);
                    }).finally(function () {                       
                        vm.loading = false;
                    }, function errorCallback(response) {
                        // called asynchronously if an error occurs
                        // or server returns response with an error status.                      
                        alert("Error : " + response.data.ExceptionMessage);
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

            vm.editUser = function (user) {
                openCreateOrEditUserModal(user.id);
            };

            vm.createUser = function () {
                openCreateOrEditUserModal(null);
            };

            vm.createnewjob = function () {
                window.location.href = "#!/tenant/createjob";
            };

            vm.editPermissions = function (user) {
                $uibModal.open({
                    templateUrl: '~/App/common/views/users/permissionsModal.cshtml',
                    controller: 'common.views.users.permissionsModal as vm',
                    backdrop: 'static',
                    resolve: {
                        user: function () {
                            return user;
                        }
                    }
                });
            };

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

            vm.exportToExcel = function () {
                userService.getClaimsToExcel({})
                    .then(function (result) {
                        app.downloadTempFile(result.data);
                    });
            };

            function openCreateOrEditUserModal(userId) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/common/views/users/createOrEditModal.cshtml',
                    controller: 'common.views.users.createOrEditModal as vm',
                    backdrop: 'static',
                    resolve: {
                        userId: function () {
                            return userId;
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getUsers();
                });
            }

            vm.getUsers();
            
        }]);
})();