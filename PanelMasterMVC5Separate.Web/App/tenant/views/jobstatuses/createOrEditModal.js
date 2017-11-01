(function () {
    appModule.controller('tenant.views.jobstatuses.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.branchClaim', 'jobStatusId',
        function ($scope, $uibModalInstance, userService, jobStatusId) {
            var vm = this;
            vm.saving = false;
            vm.job = null;           
            vm.TenantId = abp.session.tenantId;
            vm.save = function () {
                vm.saving = true;                 
                vm.job.tenant = vm.TenantId; 
                vm.job.jobStatusId = jobStatusId;                 
                vm.job.isActive = true; 
                userService.createOrUpdateJobStatus(vm.job).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $uibModalInstance.close()
                }).finally(function () {
                    vm.saving = false;
                });
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            $scope.ddlList = []; //list of dropdowns
            vm.filldds = function () {
                $scope.ddlList.push({
                    name: "Yes",
                    id: true
                });
                $scope.ddlList.push({
                    name: "No",
                    id: false
                });
            };

            $scope.sortOrderList1 = []; //list of dropdowns
            vm.sortOrder1 = function () {
                for (var i = 1; i <= 100; i++) {
                    $scope.sortOrderList1.push({
                        name: i,
                        id: i
                    });
                }
            };

            $scope.sortOrderList = []; //list of masks
            vm.sortOrder = function () {
                userService.getSortOrders(jobStatusId)
                    .then(function (ins_obj) {
                        angular.forEach(ins_obj.data.items, function (insvalue, key1) {
                            $scope.sortOrderList.push({
                                name: insvalue,
                                id: insvalue
                            });
                        });

                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            $scope.masksList = []; //list of masks
            vm.getMarks = function () {
                userService.getJobStatusMasks()
                    .then(function (ins_obj) {

                        angular.forEach(ins_obj.data.items, function (insvalue, key1) {
                            $scope.masksList.push({
                                name: insvalue.description1,
                                id: insvalue.id
                            });
                        });

                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            function init() {
                userService.getJobStatusForEdit({
                    id: jobStatusId
                }).then(function (result) {                         
                    vm.job = result.data;
                    
                    if (vm.job.id === 0) {
                        vm.job.showAwaiting = '';
                        vm.job.showSpeedbump = '';
                    }
                });
            }
            init();
            vm.getMarks();
            vm.filldds();
            vm.sortOrder();
        }
    ]);
})();