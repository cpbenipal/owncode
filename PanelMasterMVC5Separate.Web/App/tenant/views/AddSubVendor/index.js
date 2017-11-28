(function () {

    appModule.controller('tenant.views.AddSubVendor.index', [
        '$scope', '$uibModal', '$stateParams', 'abp.services.app.vendorClaim',        

        function ($scope, $uibModal, $stateParams, jobService) {
            
            var vm = this;
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.saving = false;
            vm.mainVendor = {};
            vm.subVendor = {};
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

            vm.getVendorDetails = function () {
                
                vm.loading = true;
                jobService.getMainVendor($.extend({ filter: $stateParams.id }, $stateParams.id))
                    .then(function (result) {

                        vm.mainVendor = result.data;

                    }).finally(function () {
                        vm.loading = false;
                    });

                jobService.getSubVendor($.extend({ filter: $stateParams.id }, $stateParams.id), vm.TenantId)
                    .then(function (result) {

                        vm.subVendor = result.data.items[0];

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
                        name: i + ' DAYS',
                        id: i + ' DAYS'
                    });
                }
            };


            $('#submit_form .button-submit').click(function () {

                vm.subVendor.TenantId = abp.session.tenantId;
                vm.subVendor.VendorID = $stateParams.id;
                
                vm.saving = true;

                jobService.addSubVendor(vm.subVendor).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    window.location.href = "#!/tenant/VendorList";
                }).finally(function () {
                    vm.saving = false;
                }, function errorCallback(response) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                    alert("Error : " + response.data.ExceptionMessage);
                });
            });

            $('#submit_form .btn-cancel').click(function () {
                window.location.href = "#!/tenant/VendorList";
            });
                       

            vm.getpaymenttype();
            vm.getBank();
            vm.getcurrency();
            vm.getVendorDetails();
        }]);
})();