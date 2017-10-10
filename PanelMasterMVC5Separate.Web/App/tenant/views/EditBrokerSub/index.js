(function () {

    appModule.controller('tenant.views.EditBrokerSub.index', [
        '$scope', '$uibModal', '$stateParams', 'abp.services.app.broker',


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
                                name: insvalue.currencyCode,
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
                        name: i + ' DAYS',
                        id: i + ' DAYS'
                    });
                }
            };

            vm.getBrokerMaster = function (BrokerId) {
                 
                vm.loading = true;
                jobService.getBrokerMasterDetail($.extend({ filter: BrokerId }, BrokerId))
                    .then(function (result) {
                        vm.client = result.data;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            vm.getBrokersub = function () {
                vm.loading = true;
                jobService.getBrokerSubMasterDetail($.extend({ filter: $stateParams.id }, $stateParams.id))
                    .then(function (result) {
                        vm.job = result.data;                         
                       // vm.getBrokerMaster(vm.job.brokerID);
                         
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            vm.save = function () {
                vm.job.TenantId = abp.session.tenantId;
               // vm.job.Id = $stateParams.id;
                vm.job.InsurerId = $stateParams.id;
                vm.saving = true;
                jobService.createOrUpdateSubBroker(vm.job).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    window.location.href = "#!/tenant/Brokers";
                }).finally(function () {
                    vm.saving = false;
                });
            };

            vm.getpaymenttype();
            vm.getBank();
            vm.getcurrency();            
            vm.getBrokersub();
        }]);
})();