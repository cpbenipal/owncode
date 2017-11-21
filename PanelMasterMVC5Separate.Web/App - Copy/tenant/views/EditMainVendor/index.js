(function () {
 
    appModule.controller('tenant.views.EditMainVendor.index', [
        '$scope', '$uibModal', '$stateParams', 'abp.services.app.vendorClaim',
        
        function ($scope, $uibModal, $stateParams, VendorService) {
           
            var vm = this;
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.saving = false;
           
            vm.mainVendor = {};
            
            vm.advancedFiltersAreShown = false;
            vm.filterText = $stateParams.filterText || '';
            vm.currentUserId = abp.session.userId;
            vm.TenantId = abp.session.tenantId;

            vm.getVendorDetails = function () {
               
                vm.loading = true;
                VendorService.getMainVendor($.extend({ filter: $stateParams.id }, $stateParams.id))
                    .then(function (result) {
                        vm.mainVendor = result.data.items[0];                       
                       
                    }).finally(function () {
                        vm.loading = false;
                    });               
            };

           
            $('#submit_form .button-submit').click(function () {
               
                vm.saving = true;
                
                VendorService.updateMainVendor(vm.mainVendor).then(function () {                  
                    
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    window.location.href = "#!/tenant/VendorList";
                   
                }).finally(function () {
                    vm.saving = false;
                }, function errorCallback(response) {                   
                    alert("Error : " + response.data.ExceptionMessage);
                });              
                                
            });

            $('#submit_form .btn-cancel').click(function () {
                window.location.href = "#!/tenant/VendorList";
            });

            vm.getVendorDetails();
            
        }]);
})();