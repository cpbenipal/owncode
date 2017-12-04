(function () {
    appModule.controller('host.views.VehicleMades.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.manufacture', 'madeId',
        function ($scope, $uibModalInstance, userService, madeId) {
            var vm = this;
             
            vm.saving = false;
            vm.bank = null;
            vm.loading = false;
            vm.madeId = madeId;

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };
               

            vm.save = function () {
                vm.saving = true;
                if (madeId == null) {
                    vm.job.id = 0;
                }
                else
                    vm.job.id = madeId;
                userService.createOrUpdateMade(vm.job).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $uibModalInstance.close()
                }).finally(function () {
                    vm.saving = false;
                });
            };
             

            function init() {
                userService.getThisMade($.extend({ filter: madeId }, madeId)).then(function (result) {
                    vm.job = result.data;                     
                });  
            }             

            $scope.makeList = []; //list of makes
            vm.getMakes = function () {

                userService.getMakes()
                    .then(function (ins_obj) {

                        angular.forEach(ins_obj.data.items, function (insvalue, key1) {
                            $scope.makeList.push({
                                name: insvalue.description,
                                id: insvalue.id
                            });
                        });

                    }).finally(function () {
                        vm.loading = false;
                    });

            };
            init();
            vm.getMakes();
        }
    ]);
})();