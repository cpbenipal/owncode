(function () {
    appModule.controller('tenant.views.towoperators.addedittow', [
        '$scope', '$uibModalInstance', 'abp.services.app.branchClaim', 'towId',
        function ($scope, $uibModalInstance, userService, towId) {
            var vm = this;
            vm.saving = false;
            vm.job = {};

            vm.save = function () {
                vm.saving = true;
                if (towId == null) {
                    vm.job.id = 0;
                }
                userService.addUpdateTowOperator(vm.job).then(function () {
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
                userService.getMainTowOperator($.extend({ filter: towId }, towId)).then(function (result) {
                    vm.job = result.data;
                });
            } 
            if (towId != null) {
                init();
            }            
        }
    ]);
})();