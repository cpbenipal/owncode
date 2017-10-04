(function () {

    appModule.controller('tenant.views.insurance_information.index', [
        '$scope', '$uibModal', '$stateParams', 'abp.services.app.branchClaim',


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
            vm.BranchID = abp.session.tenantId;
            vm.job.id = $stateParams.id;


            vm.getJobs = function () {

                vm.loading = true;
                jobService.getJobDetails($.extend({ filter: $stateParams.id }, $stateParams.id))
                    .then(function (result) {

                        vm.job.id = $stateParams.id;
                        vm.job.Insurance = result.data.insurance;
                        vm.job.Broker = result.data.broker;

                        vm.job.Name = result.data.name;
                        vm.job.Surname = result.data.surname;
                        vm.job.clientid = result.data.clientID;
                        vm.job.BrokerId = result.data.brokerID;
                        vm.job.InsuranceId = result.data.insuranceID;
                        $scope.drpdpwnvalue = $scope.InsuranceList
                        vm.job.RegNo = result.data.regNo;
                        vm.job.VinNumber = result.data.vinNumber;
                        vm.job.Year = result.data.year;
                        vm.job.Colour = result.data.colour;
                         
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            $scope.InsuranceList = []; //list of Insurances
            vm.getInsurance = function () {

                jobService.getInsurances()
                    .then(function (ins_obj) {                       

                        angular.forEach(ins_obj.data.items, function (insvalue, key1) {
                            $scope.InsuranceList.push({
                                name: insvalue.insurance_Desc,
                                id: insvalue.id
                            });
                        });
                         

                    }).finally(function () {
                        vm.loading = false;
                    });
            };
             
            $scope.BrokerList = []; //list of Brokers
            vm.getBroker = function () {

                jobService.getBrokers()
                    .then(function (ins_obj) {
                       

                        angular.forEach(ins_obj.data.items, function (insvalue, key1) {                             
                            $scope.BrokerList.push({
                                name: insvalue.broker_Desc,
                                id: insvalue.id
                            });
                        });
                        
                    }).finally(function () {
                        vm.loading = false;
                    });

            };
           

            $('#submit_form .button-submit').click(function () {
              
                vm.saving = true;
                vm.job.id = $stateParams.id;
                jobService.updateInsuranceInfo(
                    $.extend({ filter: vm.job }, vm.job)
                ).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    window.location.href = "#!/tenant/vehicle_information/" + vm.job.id + "#tab_1";
                }).finally(function () {
                    vm.saving = false;
                }, function errorCallback(response) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                    alert("Error : " + response.data.ExceptionMessage);
                });
            });
          
            vm.getInsurance();
            vm.getBroker();
            vm.getJobs();
             

        }]);
})();