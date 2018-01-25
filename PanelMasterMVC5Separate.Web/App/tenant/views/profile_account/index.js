(function () {

    appModule.controller('tenant.views.profile_account.index', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.user', 'abp.services.app.userLink',

        function ($scope, $uibModal, $stateParams, uiGridConstants, userService, userLinkService) {
            var vm = this;
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.saving = false;
            vm.currentUserId = abp.session.userId;
            vm.setRandomPassword = (vm.currentUserId == null);
            vm.sendActivationEmail = (vm.currentUserId == null);
            vm.userinfo = [];
            vm.user = [];
            vm.linkedUsers = {};
             
            vm.getAssignedRoleCount = function () {
                return _.where(vm.roles, { isAssigned: true }).length;
            };

            vm.getRoles = function () {
                userService.getUserForEdit({
                    id: vm.currentUserId
                }).then(function (result) {
                    vm.roles = result.data.roles;
                    vm.userinfo = result.data.user;                                      
                });
            };
             
            vm.updateroles = function () {
                var assignedRoleNames = _.map(
                    _.where(vm.roles, { isAssigned: true }), //Filter assigned roles
                    function (role) {
                        return role.roleName; //Get names
                    });

                vm.saving = true;
                userService.createOrUpdateUser({
                    user: vm.userinfo,
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
                userLinkService.linkedUsersQueryForProfile().then(function (result) {                    
                    vm.linkedUsers = result.data.items;
                }).finally(function () {
                    vm.loading = false;
                });
            };
             

            vm.getLinkedUsers();
            
            vm.getRoles();
            
        }]);
})();