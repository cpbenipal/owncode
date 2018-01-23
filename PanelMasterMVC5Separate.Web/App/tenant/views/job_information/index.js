(function () {

    appModule.controller('tenant.views.job_information.index', [
        '$scope', '$uibModal', '$stateParams', 'abp.services.app.branchClaim',


        function ($scope, $uibModal, $stateParams, jobService) {
            var vm = this;
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.saving = false;
            vm.job = {};
            vm.existing_job = {};
            vm.client = {};

            vm.advancedFiltersAreShown = false;
            vm.filterText = $stateParams.filterText || '';
            vm.currentUserId = abp.session.userId;
            vm.BranchID = abp.session.tenantId;
            vm.job.id = $stateParams.id;

            $scope.jobStatusList = []; 
            $scope.claimHandlerList = [];
            $scope.CSAList = [];
            $scope.EstimatorList = [];
            $scope.PartsBuyerList = [];
            $scope.InsuranceList = [];
            $scope.BrokerList = [];

            vm.getJobs = function () {

               

                vm.loading = true;
                jobService.getJobDetails($.extend({ filter: $stateParams.id }, $stateParams.id))
                    .then(function (result) {

                        vm.job.id = $stateParams.id;
                        //vm.job.RegNo = result.data.regNo;
                        //vm.job.VinNumber = result.data.vinNumber;
                        //vm.job.Year = result.data.year;
                        //vm.job.Colour = result.data.colour;
                        vm.job.new_Comeback = result.data.new_Comeback;
                        vm.job.underWaranty = result.data.underWaranty;                        
                        vm.job.isUnrelatedDamageReason = result.data.isUnrelatedDamageReason;
                        vm.job.shopAllocation = result.data.shopAllocation;
                        vm.job.highPriority = result.data.highPriority;
                        vm.job.contents = result.data.contents;
                        vm.job.jobNotProceeding = result.data.jobNotProceeding;
                        vm.job.currentKMs = result.data.currentKMs;
                        vm.job.otherInformation = result.data.otherInformation;
                        vm.job.damageReason = result.data.damageReason;

                        vm.job.csaID = result.data.csaID;
                        vm.job.csaDesc = result.data.csaDesc; 
                        
                        vm.job.claimHandlerID = result.data.claimHandlerID;
                        vm.job.claimHandlerDesc = result.data.claimHandlerDesc;
                        
                        vm.job.estimatorID = result.data.estimatorID;
                        vm.job.estimatorDesc = result.data.estimatorDesc;

                        vm.job.partsBuyerID = result.data.partsBuyerID;
                        vm.job.partsBuyerDesc = result.data.partsBuyerDesc;

                        vm.job.shopAllocationID = result.data.shopAllocationID; 

                        vm.job.claimAdministrator = result.data.claimAdministrator;
                        vm.job.claimNumber = result.data.claimNumber;
                        vm.job.insuranceOtherInfo = result.data.insuranceOtherInfo;
                        vm.job.policyNumber = result.data.policyNumber;

                        vm.job.insuranceID = result.data.insuranceID;
                        vm.job.insurance = result.data.insurance;
                        vm.job.brokerID = result.data.brokerID;
                        vm.job.broker = result.data.broker;

                        if (result.data.jobStatusID !== 0) {
                            $scope.jobStatusList.push({
                                name: result.data.jobStatusDesc,
                                id: result.data.jobStatusID
                            });
                        }

                        if (vm.job.insuranceID !== 0) {
                            $scope.InsuranceList.push({
                                name: vm.job.insurance,
                                id: vm.job.insuranceID
                            });
                        }

                        jobService.getInsurances()
                            .then(function (result) {
                                angular.forEach(result.data.items, function (ins, key) {

                                    if (vm.job.insuranceID !== ins.id) {
                                        $scope.InsuranceList.push({
                                            name: ins.insurerName,
                                            id: ins.id
                                        });
                                    }

                                });

                            });

                        vm.job.selectedInsurance = $scope.InsuranceList[0];
                            
                       

                        if (vm.job.brokerID !== 0) {
                            $scope.BrokerList.push({
                                name: vm.job.broker,
                                id: vm.job.brokerID
                            });                            
                        }
                       
                        jobService.getBrokers()
                            .then(function (result) {
                                angular.forEach(result.data.items, function (br, key) {
                                    if (vm.job.brokerID !== br.id) {
                                        $scope.BrokerList.push({
                                            name: br.brokerName,
                                            id: br.id
                                        });
                                    }
                                });

                            });

                        vm.job.selectedBroker = $scope.BrokerList[0];
                        
                        
                        if (vm.job.csaID !== 0) {
                            $scope.CSAList.push({
                                name: vm.job.csaDesc,
                                id: vm.job.csaID
                            });
                            vm.job.selectedCSA = $scope.CSAList[0];
                        }

                        if (vm.job.claimHandlerID !== 0) {
                            $scope.claimHandlerList.push({
                                name: vm.job.claimHandlerDesc,
                                id: vm.job.claimHandlerID
                            });
                            vm.job.selectedClaimHandler = $scope.claimHandlerList[0];
                        }

                        if (vm.job.partsBuyerID !== 0) {
                            $scope.PartsBuyerList.push({
                                name: vm.job.partsBuyerDesc,
                                id: vm.job.partsBuyerID
                            });
                            vm.job.selectedPartsBuyer = $scope.PartsBuyerList[0];
                        }

                        if (vm.job.estimatorID !== 0) {
                            $scope.EstimatorList.push({
                                name: vm.job.estimatorDesc,
                                id: vm.job.estimatorID
                            });
                            vm.job.selectedEstimator = $scope.EstimatorList[0];
                        }  

                        jobService.getRoles()
                            .then(function (result) {

                                angular.forEach(result.data.items, function (roles_value, key) {

                                    if (roles_value.rolesCategoryID === 3) {
                                        if (vm.job.claimHandlerID !== roles_value.id) {
                                            $scope.claimHandlerList.push({
                                                name: roles_value.description,
                                                id: roles_value.id
                                            });
                                        }
                                    }

                                    if (roles_value.rolesCategoryID === 4) {
                                        if (vm.job.csaID !== roles_value.id) {
                                            $scope.CSAList.push({
                                                name: roles_value.description,
                                                id: roles_value.id
                                            });
                                        }
                                    }

                                    if (roles_value.rolesCategoryID === 5) {
                                        if (vm.job.partsBuyerID !== roles_value.id) {
                                            $scope.PartsBuyerList.push({
                                                name: roles_value.description,
                                                id: roles_value.id
                                            });
                                        }
                                    }

                                    if (roles_value.rolesCategoryID === 6) {
                                        if (vm.job.estimatorID !== roles_value.id) {
                                            $scope.EstimatorList.push({
                                                name: roles_value.description,
                                                id: roles_value.id
                                            });
                                        }
                                    }

                                });
                                

                            }).finally(function () {
                                vm.loading = false;
                            });     
                      
                        

                        jobService.getJobStatuses_1(result.data.jobStatusID).then(function (result) {

                            angular.forEach(result.data.items, function (status_obj, key) {
                              
                                $scope.jobStatusList.push({
                                    name: status_obj.jobstatus,
                                    id: status_obj.id
                                });

                            });

                        }).finally(function () {
                            //vm.loading = false;
                        });
                        
                        vm.job.selectedJobStatus = $scope.jobStatusList[0];
                        
                        if (result.data.branchEntryMethod === "D") {                           
                            $scope.branchEntryMethodList = [{ name: "Drive", id: "D" }, { name: "Tow", id: "T" }];
                        } else {
                            $scope.branchEntryMethodList = [{ name: "Tow", id: "T" }, { name: "Drive", id: "D" }];
                        }                        
                        vm.job.selectedBranchEntry = $scope.branchEntryMethodList[0];

                        $scope.shopAllocationMethodList = [{ name: "1", id: "1" }, { name: "2", id: "2" },
                        { name: "3", id: "3" }, { name: "4", id: "4" }, { name: "5", id: "5" }];
                        
                        if (result.data.shopAllocation !== 0) {
                            vm.job.shopAllocationSelectedValue = $scope.shopAllocationMethodList[vm.job.shopAllocationID - 1];
                        } 
                       
                    }).finally(function () {
                        //vm.loading = false;
                    });    
                    

                    


                $scope.tab_or_modal = 'tab';
            };            
           

            /*$('#submit_form .vehicle_info_click').click(function () {
                alert("ok");
                $state.go('tenant.job_information.vehicle_information');

            });*/

            $('#submit_form .button-submit').click(function () {
                //$('#myModal_autocomplete').modal('hide');
                UpdateVehicleINformation();
            });

            function UpdateVehicleINformation() {

                vm.saving = true;
                vm.job.id = $stateParams.id;
                vm.job.jobStatusID = vm.job.selectedJobStatus.id;

                vm.job.branchEntryMethod = vm.job.selectedBranchEntry.id;
                vm.job.csaID = vm.job.selectedCSA.id;
                vm.job.claimHandlerID = vm.job.selectedClaimHandler.id;
                vm.job.estimatorID = vm.job.selectedEstimator.id;
                vm.job.partsBuyerID = vm.job.selectedPartsBuyer.id;
                vm.job.shopAllocationID = vm.job.shopAllocationSelectedValue.id;
                vm.job.insuranceID = vm.job.selectedInsurance.id;
                vm.job.brokerID = vm.job.selectedBroker.id;
               
                jobService.updateJobInfo(
                    $.extend({ filter: vm.job }, vm.job)
                ).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));

                    //window.location.href = "#!/tenant/insurance_information/" + vm.job.id + "#tab_1";

                }).finally(function () {
                    vm.saving = false;
                }, function errorCallback(response) {
                    alert("Error : " + response.data.ExceptionMessage);
                });

            }

            vm.getJobs();

            $('#submit_form .check_update_fields').click(function () {
                /*if (vm.existing_job.RegNo == vm.job.RegNo) {               
                    $scope.tab_or_modal = 'tab';
                    alert("regno didnt changed");                                   
                    window.location.href = "#!/tenant/insurance_information/" + vm.job.id + "#tab_1";
                } else {
                    $scope.tab_or_modal = 'modal';                 
                    alert("regno changed");                 
                    window.location.href = "#!/tenant/vehicle_information/" + vm.job.id + "#tab_1";
                }*/

            });


        }]);
})();