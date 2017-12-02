(function () {

    appModule.controller('host.views.AddEditVendor.index', [
        '$scope', 'appSession', '$uibModal', '$stateParams', 'abp.services.app.vendorClaim',
        
        function ($scope, appSession, $uibModal, $stateParams, vendorService) {
            var vm = this;
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.saving = false;
            vm.mainVendor = {};

            vm.advancedFiltersAreShown = false;
            vm.filterText = $stateParams.filterText || '';
            vm.currentUserId = abp.session.userId;
         
            $scope.countryList = []; //list of Countries
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
 
            
              vm.getVendorDetails = function () {               
                vm.loading = true;
                vendorService.getMainVendor($.extend({ filter: $stateParams.id }, $stateParams.id))
                    .then(function (result) {
                        vm.mainVendor = result.data;                       
                       
                    }).finally(function () {
                        vm.loading = false;
                    });               
            };
             
             vm.save = function () {
                vm.saving = true; 
                vendorService.addUpdateVendor({
                    Id: $stateParams.id,                     
                    SupplierName: vm.mainVendor.supplierName, 
                    CountryID: vm.mainVendor.countryID
                    }).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    window.location.href = "#!/host/Vendors";
                }).finally(function () {
                    vm.saving = false;
                });
            };

            if ($stateParams.id != null) {
                vm.getVendorDetails();
            }
            vm.getCountries();
        }]);
})();