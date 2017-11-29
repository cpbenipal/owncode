(function () {
    appModule.controller('host.views.JobMaskStatuses.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.systemDefaults', 'jobStatusId',
        function ($scope, $uibModalInstance, userService, jobStatusId) {
            var vm = this;
            vm.saving = false;
            vm.job = null;            
            vm.save = function () {
                vm.saving = true;   
                userService.createOrUpdateJobMaskStatus(vm.job).then(function () {
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
                userService.getJobStatusMask({
                    id: jobStatusId
                }).then(function (result) {                         
                    vm.job = result.data; 
                });
            }  
            if (jobStatusId != null) {
                init();
            }
        }
    ]);
})();