(function () {

    appModule.controller('tenant.views.EditVehicleMake.index', [
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

            vm.uploadedFileName = null;

            vm.job.uploader = new fileUploader({
                url: abp.appPath + 'Manufacture/UploadProfilePicture',
                headers: {
                    "X-XSRF-TOKEN": abp.security.antiForgery.getToken()
                },
                queueLimit: 1,
                autoUpload: true,
                removeAfterUpload: true,
                filters: [{
                    name: 'imageFilter',
                    fn: function (item, options) {
                        //File type check
                        var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
                        if ('|jpg|jpeg|png|gif|'.indexOf(type) === -1) {
                            abp.message.warn(app.localize('ProfilePicture_Warn_FileType'));
                            return false;
                        }

                        //File size check
                        if (item.size > 1048576) //1MB
                        {
                            abp.message.warn(app.localize('ProfilePicture_Warn_SizeLimit'));
                            return false;
                        }

                        return true;
                    }
                }]
            });

            vm.job.uploader.onSuccessItem = function (fileItem, response, status, headers) {
                if (response.success) {

                    var profileFilePath = abp.appPath + 'Temp/Downloads/' + response.result.fileName + '?v=' + new Date().valueOf();
                    vm.uploadedFileName = response.result.fileName;


                } else {
                    abp.message.error(response.error.message);
                }
            };
            vm.save = function () {                
                vm.saving = true;                 
                jobService.updateMake({
                    Id: $stateParams.id,
                    logoPicture: vm.job.logoPicture,                  
                    description: vm.job.description,
                    newFileName: vm.uploadedFileName 
                }).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    window.location.href = "#!/tenant/VehicleMakesModels";
                }).finally(function () {
                    vm.saving = false;
                });
            }; 

          

            vm.ThisMake = function () {

                vm.loading = true;
                jobService.getThisMake($.extend({ filter: $stateParams.id }, $stateParams.id))
                    .then(function (result) {
                        vm.job = result.data;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };
          

            vm.ThisMake();
        }]);
})();