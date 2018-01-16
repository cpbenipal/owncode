(function () {

    appModule.controller('tenant.views.NewJob.index', [
        '$scope', '$uibModal', '$stateParams', 'abp.services.app.job',
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
           
            vm.getManufacture = function () {

                vm.loading = true;
                jobService.getManufacture()
                    .then(function (result) {
                        $scope.manufactureList = [];

                        angular.forEach(result.data.items, function (value, key) {

                            $scope.manufactureList.push({
                                name: value.description,
                                id: value.id
                            });

                            vm.filterText = value.manufacture_Desc;                            
                        });

                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            $scope.getModels = function () {
                //alert($scope.selectedSource.id);               
                jobService.getVehicleModel($.extend({ filter: $scope.selectedSource.id }, $scope.selectedSource.id))
                    .then(function (result1) {
                        $scope.modelList = []; //list of car models

                        angular.forEach(result1.data.items, function (value1, key1) {
                            $scope.modelList.push({
                                name: value1.model,
                                id: value1.madeID
                            });
                        });

                    }).finally(function () {
                        vm.loading = false;
                    });
            }

            vm.getInsurance = function () {
                //alert($scope.selectedSource.id);               
                jobService.getInsurances()
                    .then(function (ins_obj) {
                        $scope.InsuranceList = []; //list of Insurances

                        angular.forEach(ins_obj.data.items, function (insvalue, key1) {                           
                            $scope.InsuranceList.push({
                                name: insvalue.insurerName,
                                id: insvalue.id
                            });                            
                        });

                    }).finally(function () {
                        vm.loading = false;
                    });
            }

            vm.getBroker = function () {
                             
                jobService.getBrokers()
                    .then(function (ins_obj) {
                        $scope.BrokerList = []; //list of Brokers

                        angular.forEach(ins_obj.data.items, function (insvalue, key1) {
                            $scope.BrokerList.push({
                                name: insvalue.brokerName,
                                id: insvalue.id
                            });
                        });

                    }).finally(function () {
                        vm.loading = false;
                    });
                
            }

            vm.getEstimators = function () {

                jobService.getEstimators()
                    .then(function (estimator_obj) {
                        $scope.EstimatorList = []; //list of Brokers                        

                        angular.forEach(estimator_obj.data.items, function (estimatorvalue, key1) {

                            //alert(estimatorvalue.id);

                            $scope.EstimatorList.push({
                                name: estimatorvalue.estimator_Desc,
                                id: estimatorvalue.id
                            });
                        });

                    }).finally(function () {
                        vm.loading = false;
                    });

            }

            $scope.getModelID = function () {
                //alert($scope.selectedItem.id);
            }

            $scope.drptitle_listChange = function () {
                //alert($scope.drptitle_list);
            }         
            
            
            $('#form_wizard_1 .button-submit').click(function () { 
                
                vm.saving = true;
                //alert(" Title: " + $scope.drptitle_list + " Comm Type: " + $scope.communicationTypeModel + " Contact After Service: " + $scope.ContactAfterServiceModel);
                vm.client.Title = $scope.drptitle_list;
                vm.client.CommunicationType = $scope.communicationTypeModel;
                vm.client.ContactAfterService = $scope.ContactAfterServiceModel;
                
                vm.job.ManufactureID = $scope.selectedSource.id;
                vm.job.ModelID = $scope.selectedItem.id;
                vm.job.InsuranceID = $scope.InsuranceModel.id;
                vm.job.BrokerID = $scope.BrokerModel.id;
                vm.job.CSAID = 0;
                vm.job.JobStatusID = 1;
                vm.job.ClaimHandlerID = 0;
                vm.job.PartsBuyerID = 0;
                vm.job.KeyAccountManagerID = 0;
                vm.job.EstimatorID = 0;
               
                vm.job.New_Comeback = $scope.NewComebackModel;       
                
                jobService.addClient(vm.client).then(function (client_results) {
                    
                    vm.job.ClientID = client_results.data.id;

                    alert(vm.job.ClientID);
                    jobService.createJob(vm.job).then(function () {
                        
                        abp.notify.info(app.localize('SavedSuccessfully'));
                        window.location.href = "#!/tenant/jobdetails";
                        
                    }).finally(function () {
                        vm.saving = false;
                    });


                }).finally(function () {
                    vm.saving = false;
                });

                

                

            }).hide();            

            vm.getManufacture(); 
            vm.getInsurance();
            vm.getBroker();
          //  vm.getEstimators(); 
            
        }]);
})();