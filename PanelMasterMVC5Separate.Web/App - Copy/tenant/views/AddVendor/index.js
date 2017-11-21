(function () {
 
    appModule.controller('tenant.views.AddVendor.index', [
        '$scope', '$uibModal', '$stateParams', 'abp.services.app.vendorClaim',
        
        function ($scope, $uibModal, $stateParams, VendorService) {
            var vm = this;
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.saving = false;
            vm.subVendor = {};
            vm.mainVendor = {};
            
            vm.advancedFiltersAreShown = false;
            vm.filterText = $stateParams.filterText || '';
            vm.currentUserId = abp.session.userId;
            vm.TenantId = abp.session.tenantId;
           
                       
            $scope.bankList = []; //list of Banks
            vm.getBank = function () {

                VendorService.getBanks()
                    .then(function (ins_obj) {                       

                        angular.forEach(ins_obj.data.items, function (insvalue, key1) {                             
                            $scope.bankList.push({
                                name: insvalue.bankName,
                                id: insvalue.id
                            });
                        });
                        
                    }).finally(function () {
                        vm.loading = false;
                    });

            };

            $scope.currencyList = []; //list of Currencies
            vm.getcurrency = function () {

                VendorService.getCurrencies()
                    .then(function (ins_obj) {

                        angular.forEach(ins_obj.data.items, function (insvalue, key1) {
                            $scope.currencyList.push({
                                name: insvalue.countryAndCurrency,
                                id: insvalue.id
                            });
                        });

                    }).finally(function () {
                        vm.loading = false;
                    });

            };

            $scope.SimpleData = []; //list of Currencies
            vm.getpaymenttype = function () {
               
                for (var i = 1; i <= 100; i++) {
                    $scope.SimpleData.push({
                        name: i+' DAYS',
                        id: i+' DAYS'
                    });
                }                
            }; 


            $('#submit_form .button-submit').click(function () {
               
                vm.saving = true;
                
                VendorService.addMainVendor(vm.mainVendor).then(function (vendor_obj) {                  
                    
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    window.location.href = "#!/tenant/VendorList";
                   
                }).finally(function () {
                    vm.saving = false;
                }, function errorCallback(response) {                   
                    alert("Error : " + response.data.ExceptionMessage);
                });              
                                
            });

            //vm.getpaymenttype();
            //vm.getBank();
            //vm.getcurrency();
            
        }]);
})();