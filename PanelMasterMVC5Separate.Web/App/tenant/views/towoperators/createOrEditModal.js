(function () {
    appModule.controller('tenant.views.towoperators.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.branchClaim', 'towId',
        function ($scope, $uibModalInstance, userService, towId) {
            var vm = this;
            vm.saving = false;
            vm.job = null;           
           
            vm.save = function () {
                vm.saving = true;         
                vm.job.isActive = true; 
                vm.job.tenantId = abp.session.tenantId;
                vm.job.towOperatorId = towId;
                userService.createOrUpdateTowOperator(vm.job).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $uibModalInstance.close()
                }).finally(function () {
                    vm.saving = false;
                });
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };
             
            function init() {
                userService.getTow({
                    id: towId
                }).then(function (result) {                         
                    vm.job = result.data; 
                });
            }
            init(); 
        }
    ]);
})();