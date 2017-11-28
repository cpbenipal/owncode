(function () {
    appModule.controller('host.views.towoperators.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.branchClaim', 'towId',
        function ($scope, $uibModalInstance, userService, towId) {
            var vm = this;
            vm.saving = false;
            vm.job = null;           
           
            vm.save = function () {
                vm.saving = true;                 
                vm.job.tenantId = 1;                            
                vm.job.isActive = true; 
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
            $scope.countryList = []; //list of Countries
            vm.getCountries = function () {
                userService.getCountry()
                    .then(function (ins_obj) {

                        angular.forEach(ins_obj.data.items, function (insvalue, key1) {
                            $scope.countryList.push({
                                name: insvalue.country + " (" + insvalue.code + ")",
                                id: insvalue.id
                            });
                        });

                    }).finally(function () {
                        vm.loading = false;
                    });
            };
            function init() {
                userService.getHostTow({
                    id: towId
                }).then(function (result) {                         
                    vm.job = result.data; 
                });
            }
            init(); 
            vm.getCountries();
        }
    ]);
})();