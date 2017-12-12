(function () {

    appModule.controller('tenant.views.createjob.index', [
        '$scope', '$uibModal', '$stateParams', 'abp.services.app.job',
        function ($scope, $uibModal, $stateParams, jobService) {
            var vm = this;
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.saving = false;
             
            vm.client = {};
            vm.filter = {};
            vm.advancedFiltersAreShown = false;
            vm.filterText = $stateParams.filterText || '';
            vm.filterText1 = $stateParams.filterText1 || '';
            vm.currentUserId = abp.session.userId;
            vm.BranchID = abp.session.tenantId;

             vm.importClient = function () {
                vm.loading = true;                 
                jobService.importExistingData(vm.filter)
                    .then(function (result) {
                        vm.client = result.data;
                        vm.client.clientOtherInformation = result.data.otherInformation;   
                        vm.getModels(vm.client.makeId);
                        vm.client.modelId = result.data.modelId;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            $scope.manufactureList = [];
            vm.getVehicleMake = function () {
                vm.loading = true;
                $scope.manufactureList.pop();
                jobService.getManufacture()
                    .then(function (ins_obj) {
                        angular.forEach(ins_obj.data.items, function (insvalue, key) {
                            $scope.manufactureList.push({
                                name: insvalue.description,
                                id: insvalue.id
                            });
                        });

                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            $scope.paintList = [];
            vm.getPaints = function () {
                vm.loading = true;
                $scope.paintList.pop();
                jobService.getPaintType()
                    .then(function (ins_obj) {
                        angular.forEach(ins_obj.data.items, function (value, key) {
                            $scope.paintList.push({
                                name: value.paintType,
                                id: value.id
                            });
                        });

                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            $scope.modelList = []; //list of car models
            vm.getModels = function (makeId) {
               
                $scope.modelList.pop();
                jobService.getVehicleModel($.extend({ filter: makeId }, makeId))
                    .then(function (result1) {
                        angular.forEach(result1.data.items, function (value1, key1) {
                            $scope.modelList.push({
                                name: value1.model,
                                id: value1.madeID
                            });
                        });

                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            $scope.InsuranceList = []; //list of Insurances
            vm.getInsurance = function () {
                $scope.InsuranceList.pop();
                jobService.getInsurances()
                    .then(function (ins_obj) {
                        angular.forEach(ins_obj.data.items, function (insvalue, key1) {
                            $scope.InsuranceList.push({
                                name: insvalue.insurerName,
                                id: insvalue.id
                            });
                        });

                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            $scope.contactAfterServiceList = []; //list of preAuthList
            vm.contactAfterService = function () {
                $scope.contactAfterServiceList.pop();
                $scope.contactAfterServiceList.push({
                    name: "Yes",
                    id: true
                });
                $scope.contactAfterServiceList.push({
                    name: "No",
                    id: false
                });
            };

            $scope.BrokerList = []; //list of Brokers
            vm.getBroker = function () {
                $scope.BrokerList.pop();
                jobService.getBrokers()
                    .then(function (ins_obj) {
                        angular.forEach(ins_obj.data.items, function (insvalue, key1) {
                            $scope.BrokerList.push({
                                name: insvalue.brokerName,
                                id: insvalue.id
                            });
                        });

                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            $('#form_wizard_1 .button-submit').click(function () {
                vm.loading = true;
                vm.saving = true;
                abp.message.confirm(
                    app.localize('AreYouSure', "Submit"),
                    function (isConfirmed) {
                        if (isConfirmed) {
                            jobService.createNewJob($.extend({ filter: vm.client }, vm.client)).then(function () {
                                abp.notify.info(app.localize('SavedSuccessfully'));
                                window.location.href = "#!/tenant/jobdetails";
                            }).finally(function () {
                                vm.saving = false;
                                vm.loading = false;
                            });
                        }
                    })                     
            }).hide();
            
            $scope.communicationTypeList = []; //list of communicationTypeList
            vm.communicationType = function () {
                $scope.communicationTypeList.pop();
                $scope.communicationTypeList.push({
                    name: "All",
                    id: 'all'
                });
                $scope.communicationTypeList.push({
                    name: "SMS",
                    id: 'sms'
                });
                $scope.communicationTypeList.push({
                    name: "Email",
                    id: 'email'
                });
                $scope.communicationTypeList.push({
                    name: "Telephone",
                    id: 'telephone'
                });
            };

            $scope.titleList = []; //list of communicationTypeList
            vm.getTitles = function () {
                $scope.titleList.pop();
                $scope.titleList.push({
                    name: "Mr",
                    id: 'mr'
                });
                $scope.titleList.push({
                    name: "Mrs",
                    id: 'mrs'
                });
                $scope.titleList.push({
                    name: "Miss",
                    id: 'miss'
                });
            };

            vm.getVehicleMake();
            vm.getInsurance();
            vm.getBroker();
            vm.getPaints();
            vm.contactAfterService();
            vm.communicationType();
            vm.getTitles();
        }]);
})();