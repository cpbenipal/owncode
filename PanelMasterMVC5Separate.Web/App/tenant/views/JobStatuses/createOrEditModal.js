(function () {
    appModule.controller('host.views.JobStatuses.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.systemDefaults', 'jobStatusId',
        function ($scope, $uibModalInstance, userService, jobStatusId) {
            var vm = this;
            vm.saving = false;
            vm.job = null;            
            vm.save = function () {
                vm.saving = true;           
                userService.createOrUpdateJobStatus(vm.job).then(function () {
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
                userService.getJobStatus({
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