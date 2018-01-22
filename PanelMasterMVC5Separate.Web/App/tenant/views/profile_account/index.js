(function () {

    appModule.controller('tenant.views.profile_account.index', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.profile', 'abp.services.app.user', 'abp.services.app.userLink',

        function ($scope, $uibModal, $stateParams, uiGridConstants, profileService, userService, userLinkService) {
            var vm = this;
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.saving = false;
            vm.advancedFiltersAreShown = false;
            vm.filterText = $stateParams.filterText || '';
            vm.currentUserId = abp.session.userId;
            vm.setRandomPassword = (vm.currentUserId == null);
            vm.sendActivationEmail = (vm.currentUserId == null);
            vm.userinfo = [];
            vm.user = [];

            var requestParams = {
                skipCount: 0,
                maxResultCount: app.consts.grid.defaultPageSize,
                sorting: null
            };

            vm.getProfile = function () {
                profileService.myPersonalInfo()
                    .then(function (result) {
                        vm.userinfo = result.data;
                        vm.userinfo.fullNametitle = vm.userinfo.fullName;

                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            vm.saveprofile = function () {
                profileService.saveprofileInfo(vm.userinfo)
                    .then(function () {
                        abp.notify.info(app.localize('SavedSuccessfully'));
                        vm.getProfile();
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            vm.getAssignedRoleCount = function () {
                return _.where(vm.roles, { isAssigned: true }).length;
            };

            vm.getRoles = function () {
                userService.getUserForEdit({
                    id: vm.currentUserId
                }).then(function (result) {
                    vm.roles = result.data.roles;
                    vm.user = result.data.user;
                });
            };
            vm.deleteLinkedUser = function (linkedUser) {
                abp.message.confirm(
                    app.localize('LinkedUserDeleteWarningMessage', linkedUser.username),
                    function (isConfirmed) {
                        if (isConfirmed) {
                            userLinkService.unlinkUser({
                                userId: linkedUser.id,
                                tenantId: linkedUser.tenantId
                            }).then(function () {
                                vm.getLinkedUsers();
                                abp.notify.success(app.localize('SuccessfullyUnlinked'));
                            });
                        }
                    }
                );
            };
            vm.updateroles = function () {
                var assignedRoleNames = _.map(
                    _.where(vm.roles, { isAssigned: true }), //Filter assigned roles
                    function (role) {
                        return role.roleName; //Get names
                    });

                vm.saving = true;
                userService.createOrUpdateUser({
                    user: vm.user,
                    assignedRoleNames: assignedRoleNames,
                    sendActivationEmail: vm.sendActivationEmail,
                    setRandomPassword: vm.setRandomPassword
                }).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    getRoles();
                }).finally(function () {
                    vm.saving = false;
                });
            };
            vm.getLinkedUsers = function () {
                vm.loading = true;
                userLinkService.getLinkedUsers({
                    skipCount: requestParams.skipCount,
                    maxResultCount: requestParams.maxResultCount,
                    sorting: requestParams.sorting
                }).then(function (result) {
                    vm.linkedUsersGridOptions.totalItems = result.data.totalCount;
                    vm.linkedUsersGridOptions.data = result.data.items;
                }).finally(function () {
                    vm.loading = false;
                });
            };


            vm.linkedUsersGridOptions = {
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
                        name: app.localize('Name'),
                        field: 'name',
                        minWidth: 140
                    },
                    {
                        name: app.localize('TenancyName'),
                        field: 'tenancyName',
                        minWidth: 140
                    },
                    {
                        name: 'Action',
                        enableSorting: false,
                        width: 100,
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '    <button class="btn btn-xs btn-primary blue" uib-dropdown-toggle="" aria-haspopup="true" aria-expanded="false">Send Message</button>' +
                        '</div>'
                    }
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    $scope.gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
                        if (!sortColumns.length || !sortColumns[0].field) {
                            requestParams.sorting = null;
                        } else {
                            requestParams.sorting = sortColumns[0].field + ' ' + sortColumns[0].sort.direction;
                        }

                        vm.getLinkedUsers();
                    });
                    gridApi.pagination.on.paginationChanged($scope, function (pageNumber, pageSize) {
                        requestParams.skipCount = (pageNumber - 1) * pageSize;
                        requestParams.maxResultCount = pageSize;

                        vm.getLinkedUsers();
                    });
                },
                data: []
            };
            vm.switchToUser = function (linkedUser) {
                abp.ajax({
                    url: abp.appPath + 'Account/SwitchToLinkedAccount',
                    data: JSON.stringify({
                        targetUserId: linkedUser.id,
                        targetTenantId: linkedUser.tenantId
                    }),
                    success: function () {
                        app.utils.removeCookie(abp.security.antiForgery.tokenCookieName);
                    }
                });
            };
            vm.getShownLinkedUserName = function (linkedUser) {
                return app.getShownLinkedUserName(linkedUser);
            };

            vm.getLinkedUsers();
            vm.getProfile();
            vm.getRoles();
            
        }]);
})();