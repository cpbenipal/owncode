(function () {

    appModule.controller('tenant.views.vehicle_information.index', [
        '$scope', '$uibModal', '$stateParams', 'abp.services.app.branchClaim',


        function ($scope, $uibModal, $stateParams, jobService) {
            var vm = this;
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.saving = false;
            vm.vehicle = {}; 
            vm.job = {}; 
            
            vm.advancedFiltersAreShown = false;
            vm.filterText = $stateParams.filterText || '';
            vm.currentUserId = abp.session.userId;
            vm.BranchID = abp.session.tenantId;
            vm.job.id = $stateParams.id;

            $scope.MakesList = [];
            $scope.ModelList = [];
            $scope.PaintTypeList = [];
            $scope.VehicleCodeList = [];

            vm.getVehicleInfo = function () {

                vm.loading = true;
                jobService.getJobDetails($.extend({ filter: $stateParams.id }, $stateParams.id))
                    .then(function (result) {

                        vm.vehicle.vehicleId = result.data.id;
                        vm.vehicle.regNo = result.data.regNo;
                        vm.vehicle.vinNumber = result.data.vinNumber;
                        vm.vehicle.colour = result.data.colour;
                        vm.vehicle.year = result.data.year;
                        vm.vehicle.paintTypeId = result.data.paintTypeId;
                        vm.vehicle.paintTypeDesc = result.data.paintTypeDesc;
                        vm.vehicle.vehicleCode = result.data.vehicleCode;
                        vm.vehicle.mmCode = result.data.mmCode;
                        vm.vehicle.vehicleOtherInfo = result.data.vehicleOtherInfo;
                        vm.vehicle.isLuxury = result.data.isLuxury;
                        vm.vehicle.isSpecialisedType = result.data.isSpecialisedType;
                        
                        if (result.data.manufactureID !== 0) {
                            $scope.MakesList.push({
                                name: result.data.manufacture,
                                id: result.data.manufactureID
                            });
                        }

                        jobService.getVehicleMakes(result.data.manufactureID).then(function (makes_results){

                            angular.forEach(makes_results.data.items, function (make, key) {
                                
                                $scope.MakesList.push({
                                    name: make.description,
                                    id: make.id
                                });                               

                            });

                        });

                        vm.vehicle.selectedMake = $scope.MakesList[0];

                        if (result.data.modelID !== 0) {
                            $scope.ModelList.push({
                                name: result.data.model,
                                id: result.data.modelID
                            });
                        }

                        jobService.getVehicleModels(result.data.modelID).then(function (model_results) {

                            angular.forEach(model_results.data.items, function (model, key) {

                                $scope.ModelList.push({
                                    name: model.model,
                                    id: model.id
                                });

                            });

                        });                        
                        vm.vehicle.selectedModel = $scope.ModelList[0];

                        if (result.data.paintTypeId !== 0) {
                            $scope.PaintTypeList.push({
                                name: result.data.paintTypeDesc,
                                id: result.data.paintTypeId
                            });
                        }
                       
                        jobService.getPaintType(result.data.paintTypeId).then(function (paintType_results) {

                            angular.forEach(paintType_results.data.items, function (paint_type, key) {

                                $scope.PaintTypeList.push({
                                    name: paint_type.paintType,
                                    id: paint_type.id
                                });

                            });

                        });      

                        vm.vehicle.selectedPaintType = $scope.PaintTypeList[0];

                        if (result.data.vehicleCode !== 0) {
                            $scope.VehicleCodeList.push({
                                name: result.data.vehicleCode,
                                id: result.data.vehicleCode
                            });
                        }

                        vm.vehicle.selectedVehicleCode = $scope.VehicleCodeList[0];

                        $scope.VehicleCodeList = [{ name: "1", id: "1" }, { name: "2", id: "2" },
                        { name: "3", id: "3" }, { name: "4", id: "4" }];

                        if (result.data.vehicleCode !== 0) {
                            vm.vehicle.selectedVehicleCode = $scope.VehicleCodeList[result.data.vehicleCode - 1];
                        } 

                    }).finally(function () {
                        vm.loading = false;
                    });
               
                $scope.tab_or_modal = 'tab';
            };           

            vm.getVehicleInfo();

            $('#submit_form .button-submit').click(function () {                
              
                UpdateVehicleINformation();
            });

            function UpdateVehicleINformation() {

                vm.saving = true;
               
                vm.vehicle.manufactureID = vm.vehicle.selectedMake.id;
                vm.vehicle.modelID = vm.vehicle.selectedModel.id;
                vm.vehicle.paintTypeId = vm.vehicle.selectedPaintType.id;
                vm.vehicle.vehicleCode = vm.vehicle.selectedVehicleCode.id;

                
                jobService.updateVehicleInfo(
                    $.extend({ filter: vm.vehicle }, vm.vehicle)
                ).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                }).finally(function () {
                    vm.saving = false;
                }, function errorCallback(response) {
                    alert("Error : " + response.data.ExceptionMessage);
                });

            }

        }]);
})();