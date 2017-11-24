(function () {
    appModule.controller('host.views.Banks.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.systemDefaults', 'bankId',
        function ($scope, $uibModalInstance, userService, bankId) {
            var vm = this;
             
            vm.saving = false;
            vm.bank = null;
            vm.loading = false;
            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            $scope.countryList = []; //list of Banks
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

            vm.save = function () {
                vm.saving = true; 
                userService.createOrUpdateBank(vm.bank).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $uibModalInstance.close()
                }).finally(function () {
                    vm.saving = false;                     
                });
            };

            function init() {
                userService.getBank({ 
                    id: bankId
                }).then(function (result) {
                    vm.bank = result.data;                     
                });  
            }
            if (bankId != null) {
                init();
            }

            vm.getCountries();
        }
    ]);
})();