(function () {
    appModule.controller('host.views.Vendors.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.vendorClaim', 'vendorId',
        function ($scope, $uibModalInstance, vendorService, vendorId) {
            var vm = this;
            vm.vendorId = vendorId;
            vm.saving = false;
            vm.bank = null;
            vm.loading = false;
            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            $scope.countryList = []; //list of Banks
            vm.getCountries = function () {

                vendorService.getCountry()
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
                if (vendorId == null)
                    vendorId = 0;
                vendorService.addUpdateVendor({
                    Id: vendorId,
                    SupplierName: vm.mainVendor.supplierName,
                    CountryID: vm.mainVendor.countryID
                }).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $uibModalInstance.close()
                }).finally(function () {
                    vm.saving = false;
                });
            };

            function init() {
                vm.loading = true;
                vendorService.getMainVendor($.extend({ filter: vendorId }, vendorId))
                    .then(function (result) {
                        vm.mainVendor = result.data;

                    }).finally(function () {
                        vm.loading = false;
                    });   
            }
             
            init();         

            vm.getCountries();
        }
    ]);
})();