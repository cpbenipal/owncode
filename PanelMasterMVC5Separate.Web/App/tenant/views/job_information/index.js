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
            
            vm.getJobs = function () {

                vm.loading = true;
                jobService.getJobDetails($.extend({ filter: $stateParams.id }, $stateParams.id))
                    .then(function (result) {

                        vm.job.id = $stateParams.id;
                        vm.job.RegNo = result.data.regNo;
                        vm.job.VinNumber = result.data.vinNumber;
                        vm.job.Year = result.data.year;
                        vm.job.Colour = result.data.colour;
                       
                        $scope.jobStatusList.push({
                            name: result.data.jobStatusDesc,
                            id: result.data.jobStatusID
                        });
                        
                        $scope.selectedJobStatus = $scope.jobStatusList[0];

                        if (result.data.branchEntryMethod == "D") {                           
                            $scope.branchEntryMethodList = [{ name: "Drive", id: "D" }, { name: "Tow", id: "T" }];
                        } else {
                            $scope.branchEntryMethodList = [{ name: "Tow", id: "T" }, { name: "Drive", id: "D" }];
                        }
                        
                        $scope.selectedBranchEntry = $scope.branchEntryMethodList[0];

                        if (result.data.new_Comeback == "N") {                          
                            $scope.new_ComebackList = [{ name: "New", id: "N" }, { name: "Waranty/Comeback", id: "C" }];
                        } else {
                            $scope.new_ComebackList = [{ name: "Warranty/Comeback", id: "C" }, { name: "New", id: "N" }];
                        }
                        $scope.selectedNew_Comeback = $scope.new_ComebackList[0];     
                       
                    }).finally(function () {
                        //vm.loading = false;
                    });
               
                jobService.getRoles(3)
                    .then(function (result) {                        

                        angular.forEach(result.data.items, function (roles_value, key) {                            
                            $scope.claimHandlerList.push({
                                name: roles_value.description,
                                id: roles_value.iD
                            });
                        });

                        $scope.selectedClaimHandler = $scope.claimHandlerList[0];
                       
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