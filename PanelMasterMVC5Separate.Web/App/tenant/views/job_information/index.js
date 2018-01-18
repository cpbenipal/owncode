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
            
            vm.getJobs = function () {

                vm.loading = true;
                jobService.getJobDetails($.extend({ filter: $stateParams.id }, $stateParams.id))
                    .then(function (result) {

                        vm.job.id = $stateParams.id;
                        vm.job.RegNo = result.data.regNo;
                        vm.job.VinNumber = result.data.vinNumber;
                        vm.job.Year = result.data.year;
                        vm.job.Colour = result.data.colour;
                        vm.job.new_Comeback = result.data.new_Comeback;
                        vm.job.underWaranty = result.data.underWaranty; 
                        vm.job.isUnrelatedDamangeReason = result.data.isUnrelatedDamageReason;
                        vm.job.shopAllocation = result.data.shopAllocation;
                        vm.job.highPriority = result.data.highPriority;
                        vm.job.contents = result.data.contents;
                        vm.job.jobNotProceeding = result.data.jobNotProceeding;
                        vm.job.currentKMs = result.data.currentKMs;
                        vm.job.otherInformation = result.data.otherInformation;
                        vm.job.damageReason = result.data.damageReason;
                        vm.job.csaID = result.data.csaID;
                        vm.job.claimHandlerID = result.data.claimHandlerID;
                        vm.job.estimatorID = result.data.estimatorID;
                        vm.job.partsBuyerID = result.data.partsBuyerID;
                        vm.job.shopAllocationID = result.data.shopAllocationID;                        
                        

                        $scope.jobStatusList.push({
                            name: result.data.jobStatusDesc,
                            id: result.data.jobStatusID
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
                        
                        $scope.selectedJobStatus = $scope.jobStatusList[0];
                        
                        if (result.data.branchEntryMethod === "D") {                           
                            $scope.branchEntryMethodList = [{ name: "Drive", id: "D" }, { name: "Tow", id: "T" }];
                        } else {
                            $scope.branchEntryMethodList = [{ name: "Tow", id: "T" }, { name: "Drive", id: "D" }];
                        }                        
                        $scope.selectedBranchEntry = $scope.branchEntryMethodList[0];

                   
                        if (result.data.shopAllocation === 0) {
                            $scope.shopAllocationMethodList = [{ name: "1", id: "1" }, { name: "2", id: "2" },
                                { name: "3", id: "3" }, { name: "4", id: "4" }, { name: "5", id: "5" }];
                        }
                       
                    }).finally(function () {
                        //vm.loading = false;
                    });

                $scope.claimHandlerList = [];
                $scope.CSAList = [];
                $scope.EstimatorList = [];
                $scope.PartsBuyerList = [];
                
                jobService.getRoles()
                    .then(function (result) {                        

                        angular.forEach(result.data.items, function (roles_value, key) {                        
                            
                            if (roles_value.rolesCategoryID === 3) {
                                $scope.claimHandlerList.push({
                                    name: roles_value.description,
                                    id: roles_value.id
                                });
                            }
                            
                            if (roles_value.rolesCategoryID === 4) {                               
                                $scope.CSAList.push({
                                    name: roles_value.description,
                                    id: roles_value.id
                                });
                            }

                            if (roles_value.rolesCategoryID === 5) {
                                $scope.PartsBuyerList.push({
                                    name: roles_value.description,
                                    id: roles_value.id
                                });
                            }

                            if (roles_value.rolesCategoryID === 6) {                               
                                $scope.EstimatorList.push({
                                    name: roles_value.description,
                                    id: roles_value.id
                                });
                            }

                        });

                        //$scope.selectedClaimHandler = $scope.claimHandlerList[0];
                        //$scope.selectedCSA = $scope.CSAList[0];
                        //$scope.selectedPartsBuyer = $scope.PartsBuyerList[0];
                        //$scope.selectedEstimator = $scope.EstimatorList[0];
                        
                       
                    }).finally(function () {
                        vm.loading = false;
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

                jobService.updateVehicleInfo(
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