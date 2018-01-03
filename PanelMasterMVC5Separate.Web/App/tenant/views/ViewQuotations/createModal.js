(function () {
    appModule.controller('tenant.views.viewQuotations.createModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.quote', 'jobId',
        function ($scope, $uibModalInstance, jobService, jobId) {
            var vm = this;

            vm.saving = false;
            vm.currentUserId = abp.session.userId;
            vm.TenantId = abp.session.tenantId;

            vm.save = function () {
                
                vm.vehicle.jobId = jobId;
                vm.vehicle.quoteStatusID = 1;
                vm.vehicle.TenantId = vm.TenantId;
                jobService.createOrUpdateQuotation(vm.vehicle).then(function (result) {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    window.location.href = "#!/tenant/quoting";
                    $uibModalInstance.close();
                }).finally(function () {
                    
                });
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            $scope.categoryList = []; //list of categories
            vm.getCategories = function () {

                jobService.getQuoteCategories()
                    .then(function (ins_obj) {

                        angular.forEach(ins_obj.data.items, function (insvalue, key1) {
                            $scope.categoryList.push({
                                name: insvalue.description,
                                id: insvalue.id
                            });
                        });

                    }).finally(function () {
                        vm.loading = false;
                    });

            };

            $scope.rtypeList = []; //list of repair types
            vm.getRepairs = function () {

                jobService.getRepairTypes()
                    .then(function (ins_obj) {

                        angular.forEach(ins_obj.data.items, function (insvalue, key1) {
                            $scope.rtypeList.push({
                                name: insvalue.description,
                                id: insvalue.id
                            });
                        });

                    }).finally(function () {
                        vm.loading = false;
                    });

            };

            $scope.yesNoList = []; //list of yesNo
            vm.getYesNoList = function () {
                $scope.yesNoList.push({
                    name: "Yes",
                    id: true
                });
                $scope.yesNoList.push({
                    name: "No",
                    id: false
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

            function init() {                
                vm.loading = true;
                jobService.getQuoteForNewQuotation({                    
                    id: jobId
                }).then(function (result) {
                    vm.vehicle = result.data;
                    
                }).finally(function () {
                    vm.loading = false;
                });
            }
            
            vm.getCategories();
            vm.getRepairs();
            vm.getYesNoList();
            vm.getPaints();
            init();
        }
    ]);
})();