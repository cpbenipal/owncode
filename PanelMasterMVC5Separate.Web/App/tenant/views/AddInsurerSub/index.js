(function () {
 
    appModule.controller('tenant.views.AddInsurerSub.index', [
        '$scope', '$uibModal', '$stateParams', 'abp.services.app.insurer',


        function ($scope, $uibModal, $stateParams, jobService) {
            var vm = this;
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.saving = false;
            vm.job = {};
            vm.client = {};

             
            vm.advancedFiltersAreShown = false;
            vm.filterText = $stateParams.filterText || '';
            vm.currentUserId = abp.session.userId;
            vm.TenantId = abp.session.tenantId;
                       
            $scope.bankList = []; //list of Banks
            vm.getBank = function () {

                jobService.getBanks()
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

                jobService.getCurrencies()
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

            vm.getInsurerMaster = function () {

                vm.loading = true;
                jobService.getInsurerMasterDetail($.extend({ filter: $stateParams.id }, $stateParams.id))
                    .then(function (result) {

                        vm.client = result.data; 

                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            vm.save = function () {
                vm.job.TenantId = abp.session.tenantId;
                vm.job.InsurerId = $stateParams.id;
                vm.job.id = 0;
                vm.saving = true;
                jobService.createOrUpdateSubInsurer(vm.job).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    window.location.href = "#!/tenant/Insurers";
                }).finally(function () {
                    vm.saving = false;
                });
            };

            vm.getpaymenttype();
            vm.getBank();
            vm.getcurrency();
            vm.getInsurerMaster();
            
        }]);
})();