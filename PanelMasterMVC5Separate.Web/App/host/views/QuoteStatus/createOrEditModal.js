(function () {
    appModule.controller('host.views.QuoteStatus.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.systemDefaults', 'quoteId',
        function ($scope, $uibModalInstance, userService, quoteId) {
            var vm = this;
            vm.saving = false;
            vm.job = null;            
            vm.save = function () {
                vm.saving = true;   
                userService.createOrUpdateQuoteStatus(vm.job).then(function () {
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
                userService.getQuoteStatus({
                    id: quoteId
                }).then(function (result) {                         
                    vm.job = result.data; 
                });
            }  
            if (quoteId != null) {
                init();
            }
        }
    ]);
})();