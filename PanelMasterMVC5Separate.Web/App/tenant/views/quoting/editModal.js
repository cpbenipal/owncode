(function () {
    appModule.controller('tenant.views.quoting.editModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.quote', 'Id',
        function ($scope, $uibModalInstance, jobService, Id) {
            var vm = this;
 
            vm.saving = false;
            vm.currentUserId = abp.session.userId;
            vm.TenantId = abp.session.tenantId;
             
            vm.save = function () {                
                vm.saving = true;
                vm.vehicle.id = Id;                            
                vm.vehicle.TenantId = vm.TenantId;       
                jobService.createOrUpdateQuotation(vm.vehicle).then(function (result) {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    window.location.href = "#!/tenant/quoteheaders/"+result.data;
                    $uibModalInstance.close();
                }).finally(function () {
                    vm.saving = false;
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
             
            $scope.preAuthList = []; //list of preAuthList
            vm.getPreAuth = function () {
                $scope.preAuthList.push({
                    name: "Yes",
                    id: true
                });
                $scope.preAuthList.push({
                    name: "No",
                    id: false
                });
            };

            function init() {
                jobService.getQuoteForNewQuotation({
                    jobid: 0,
                    id: Id                    
                }).then(function (result) {                    
                    vm.vehicle = result.data;                     
                    });
            }           

            vm.getCategories();
            vm.getRepairs(); 
            vm.getPreAuth();
            init();
        }
    ]);
})();