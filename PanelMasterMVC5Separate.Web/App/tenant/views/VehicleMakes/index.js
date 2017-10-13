(function () {

    appModule.controller('tenant.views.VehicleMakes.index', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.manufacture',
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
                        '      <li><a ng-if="grid.appScope.permissions.edit" ng-href="#!/tenant/EditVehicleMake/{{row.entity.makeID}}">' + app.localize('Open') + '</a></li>' +                         
                        '    </ul>' +
                        '  </div>' +
                        '</div>'
                    },
                    {
                        name: app.localize('Description'),
                        field: 'description',
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '  <img ng-if="row.entity.makeID" ng-src="' + abp.appPath + 'Manufacture/GetProfilePictureById?id={{row.entity.makeID}}" width="22" height="22" class="img-rounded img-profile-picture-in-grid" />' +
                        '  <img ng-if="!row.entity.makeID" src="' + abp.appPath + 'Common/Images/default-profile-picture.png" width="22" height="22" class="img-rounded" />' +
                        '  {{COL_FIELD CUSTOM_FILTERS}} ' +
                        '</div>',
                        minWidth: 140
                    },                    
                    {
                        name: app.localize('CreationTime'),
                        field: 'creationTime',
                        cellFilter: 'momentFormat: \'L\'',
                        minWidth: 50
                    }
                    ,
                    {
                        name: app.localize('Status'),
                        field: 'isActive',
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '<div ng-show="row.entity.isActive" ng-click="grid.appScope.Status(row.entity)" class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-mini bootstrap-switch-id-test{{row.entity.makeID}} bootstrap-switch-animate bootstrap-switch-on" style="width: 66px;"><div class="bootstrap-switch-container" style="width: 96px; margin-left: 0px;">' +
                        '<span class="bootstrap-switch-handle-on bootstrap-switch-primary" style="width: 32px;"> ON</span>' +
                        '<span class="bootstrap-switch-label" style="width: 32px;">&nbsp;</span>' +
                        '<span class="bootstrap-switch-handle-off bootstrap-switch-default" style="width: 32px;">OFF</span>' +
                        '<input ng-checked="row.entity.isActive" ng-model="row.entity.isActive"  class="make-switch" data-size="mini" type="checkbox"></div></div>' +
                        '<div ng-show="!row.entity.isActive" ng-click="grid.appScope.Status(row.entity)" class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-mini bootstrap-switch-id-test{{row.entity.makeID}} bootstrap-switch-animate bootstrap-switch-off" style="width: 66px;"><div class="bootstrap-switch-container" style="width: 96px; margin-left: -32px;"><span class="bootstrap-switch-handle-on bootstrap-switch-primary" style="width: 32px;">ON</span><span class="bootstrap-switch-label" style="width: 32px;">&nbsp;</span><span class="bootstrap-switch-handle-off bootstrap-switch-default" style="width: 32px;">OFF</span>' +
                        '</div></div>' +
                        '</div>',
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

                        vm.getVehicleMakeModel();
                    });
                    gridApi.pagination.on.paginationChanged($scope, function (pageNumber, pageSize) {
                        vm.requestParams.skipCount = (pageNumber - 1) * pageSize;
                        vm.requestParams.maxResultCount = pageSize;

                        vm.getVehicleMakeModel();
                    });
                },
                data: []
            };

            vm.getVehicleMakeModel = function () {

                vm.loading = true;

                userService.getVehicleInformation($.extend({ filter: vm.filterText }, vm.requestParams))
                    .then(function (result) {
                        vm.userGridOptions.totalItems = result.data.totalCount;
                        vm.userGridOptions.data = addRoleNamesField(result.data.items);
                    }).finally(function () {
                        vm.loading = false;
                    });

            };

            vm.Status = function (i) {
                abp.message.confirm(
                    app.localize('AreYouSure', i.description),
                    function (isConfirmed) {
                        if (isConfirmed) {
                            userService.changeStatus({
                                id: i.makeID,
                                status: !i.isActive
                            }).then(function () {
                                vm.getVehicleMakeModel();
                                if (!i.isActive)
                                    abp.notify.success(app.localize('SuccessfullyEnabled'));
                                else
                                    abp.notify.warn(app.localize('SuccessfullyDisabled'));
                            });
                        }
                    }
                );
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

            vm.exportToExcel = function () {
                userService.getMakesToExcel({})
                    .then(function (result) {
                        app.downloadTempFile(result.data);
                    });
            };

            vm.getVehicleMakeModel();

        }]);
})();