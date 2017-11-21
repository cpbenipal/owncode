(function () {
    appModule.controller('tenant.views.settings.editRegister', [
        '$scope', '$uibModalInstance', 'abp.services.app.tenantSettings', 'tenantId',
        function ($scope, $uibModalInstance, tenantSettingsService, tenantId) {
            var vm = this;           
            vm.saving = false;                 
            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            vm.save = function () { 

                bootbox.prompt(app.localize('EnterOTP'), function (result) {
                    if (result === "00-00") {
                        vm.saving = true;
                        tenantSettingsService.updateTenantProfile(vm.register).then(function () {
                            abp.notify.info(app.localize('SavedSuccessfully'));
                            $uibModalInstance.close();
                        }).finally(function () {
                            vm.saving = false;
                        });

                    }
                    else {
                        abp.notify.info(app.localize('OTPMismatched'));
                    }
                });
            };
            $scope.plansList = []; //list of timezones
                    vm.getPlans = function () {
                        tenantSettingsService.getSignOnPlans()
                            .then(function (ins_obj) {
                                angular.forEach(ins_obj.data, function (insvalue, key1) {
                                    $scope.plansList.push({
                                        name: insvalue.planName,
                                        id: insvalue.id
                                    });
                                });

                            }).finally(function () {
                                vm.loading = false;
                            });
                    };
            function init() { 
                tenantSettingsService.getRegisteredInfo().then(function (result) {
                    vm.register = result.data;                     
                });
            }

            init();
            vm.getPlans();  
        }
    ]);
})();