﻿(function () {

    appModule.controller('tenant.views.quoting.index', [
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
                        enableSorting: false ,
                        width: 120,
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '  <div class="btn-group dropdown" uib-dropdown="" dropdown-append-to-body>' +
                        '    <button class="btn btn-xs btn-primary blue" uib-dropdown-toggle="" aria-haspopup="true" aria-expanded="false"><i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span></button>' +
                        '    <ul uib-dropdown-menu>' +                        
                       // '      <li><a ng-click="grid.appScope.newQuotation(row.entity)">' + app.localize('NewQuotation') + '</a></li>' +
                        '      <li><a ng-show="row.entity.id!=0" ng-href="#!/tenant/quoteheaders/{{row.entity.id}}">' + app.localize('EditQuotation') + '</a></li>' + 
                        '      <li><a ng-show="row.entity.id!=0" ng-click="grid.appScope.editQuotation(row.entity.id)">' + app.localize('EditQuoteType') + '</a></li>' + 
                        '    </ul>' +
                        '  </div>' +
                        '</div>'
                    }, 
                    {
                        name: app.localize('QuoteNumber'),
                        enableSorting: true,
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">Q0' + vm.TenantId +'-0000{{row.entity.id}}</div>'
                    },
                    {
                        name: app.localize('JobNumber'),
                        enableSorting: true,
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">R0' + vm.TenantId +'-0000{{row.entity.jobId}}</div>'
                    },
                    {
                        name: app.localize('RegNo'),
                        field: 'job' 
                    },
                    {
                        name: app.localize('QuoteStatus'),
                        field: 'quoteStatus'   
                    },
                    {
                        name: app.localize('QuoteCategory'),
                        field: 'quoteCat' 
                    },                    
                    {
                        name: app.localize('RepairType'),
                        field: 'repairType' 
                    },
                    {
                        name: app.localize('PreparedBy'),
                        field: 'lastModifierUser'
                    },
                    {
                        name: app.localize('PreAuth'),
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '<div ng-show="row.entity.pre_Auth" class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-mini bootstrap-switch-id-test{{row.entity.id}} bootstrap-switch-animate bootstrap-switch-on" style="width: 66px;"><div class="bootstrap-switch-container" style="width: 96px; margin-left: 0px;">' +
                        '<span class="bootstrap-switch-handle-on bootstrap-switch-primary" style="width: 32px;"> ON</span>' +
                        '<span class="bootstrap-switch-label" style="width: 32px;">&nbsp;</span>' +
                        '<span class="bootstrap-switch-handle-off bootstrap-switch-default" style="width: 32px;">OFF</span>' +
                        '<input ng-checked="row.entity.pre_Auth" ng-model="row.entity.pre_Auth"  class="make-switch" data-size="mini" type="checkbox"></div></div>' +
                        '<div ng-show="!row.entity.pre_Auth" class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-mini bootstrap-switch-id-test{{row.entity.id}} bootstrap-switch-animate bootstrap-switch-off" style="width: 66px;"><div class="bootstrap-switch-container" style="width: 96px; margin-left: -32px;"><span class="bootstrap-switch-handle-on bootstrap-switch-primary" style="width: 32px;">ON</span><span class="bootstrap-switch-label" style="width: 32px;">&nbsp;</span><span class="bootstrap-switch-handle-off bootstrap-switch-default" style="width: 32px;">OFF</span>' +
                        '</div></div>' +
                        '</div>',
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
 
            vm.addQuote = function (quote) {
                window.location.href = "#!/tenant/viewQuotations";
            };
            vm.editQuotation = function (quote) {
                openCreateQuoteModal(quote);
            };    

            function openCreateQuoteModal(jobId) {                
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/tenant/views/quoting/createOrEditModal.cshtml',
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