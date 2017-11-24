(function () {

    appModule.controller('host.views.AddVehicleMade.index', [
        '$scope', 'appSession', '$uibModal', '$stateParams', 'FileUploader', 'abp.services.app.manufacture',


        function ($scope, appSession, $uibModal, $stateParams, fileUploader, jobService) {
            var vm = this;
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.saving = false;
            vm.job = {};

            vm.advancedFiltersAreShown = false;
            vm.filterText = $stateParams.filterText || '';
            vm.currentUserId = abp.session.userId;
            vm.TenantId = abp.session.tenantId;          
             
            vm.save = function () {
                vm.saving = true;
                jobService.createOrUpdateMade(vm.job).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    window.location.href = "#!/host/VehicleMades";
                }).finally(function () {
                    vm.saving = false;
                });
            };
            $scope.makeList = []; //list of makes
            vm.getMakes = function () {

                jobService.getMakes()
                    .then(function (ins_obj) {

                        angular.forEach(ins_obj.data.items, function (insvalue, key1) {
                            $scope.makeList.push({
                                name: insvalue.description,
                                id: insvalue.id
                            });
                        });

                    }).finally(function () {
                        vm.loading = false;
                    });

            };
            vm.getMakes();
        }]);
})();