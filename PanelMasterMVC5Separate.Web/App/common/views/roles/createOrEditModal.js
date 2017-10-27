﻿(function () {
    appModule.controller('common.views.roles.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.role', 'roleId',
        function ($scope, $uibModalInstance, roleService, roleId) {
            var vm = this;

            vm.saving = false;
            vm.role = null;
            vm.permissionEditData = null;

            vm.save = function () {
                vm.saving = true;
                //alert(vm.role.RolesCategoryID);
                roleService.createOrUpdateRole({
                    role: vm.role,
                    grantedPermissionNames: vm.permissionEditData.grantedPermissionNames
                }).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $uibModalInstance.close();
                }).finally(function() {
                    vm.saving = false;
                });
            }; 
            
            $scope.rolesCategoryList = []; //list of categories
            vm.getRolesCategories = function () {
               
                roleService.getRolesCategories({ id: roleId })
                    .then(function (roles_obj) {
                      
                        angular.forEach(roles_obj.data.items, function (rolesCategoriesvalue, key1) {                           
                            $scope.rolesCategoryList.push({
                                name: rolesCategoriesvalue.description,
                                id: rolesCategoriesvalue.id
                            });
                        });

                    }).finally(function () {
                        vm.loading = false;
                    });

            };

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            function init() {
                roleService.getRoleForEdit({
                    id: roleId
                }).then(function (result) {
                    vm.role = result.data.role;
                    vm.permissionEditData = {
                        permissions: result.data.permissions,
                        grantedPermissionNames: result.data.grantedPermissionNames
                    };
                });
            }
          
            init();
            vm.getRolesCategories();
        }
    ]);
})();