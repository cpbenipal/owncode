(function () {
    appModule.controller('host.views.VehicleMakes.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.manufacture', 'FileUploader', 'makeId',
        function ($scope, $uibModalInstance, userService, fileUploader, makeId) {
            var vm = this;
            vm.saving = false;
            vm.job = {};
            vm.loading = false;
            vm.uploadedFileName = null;
            vm.currentUserId = abp.session.userId;
            vm.makeId = makeId;
            vm.save = function () {
                if (makeId == null) {
                    vm.create();
                }
                else {
                    vm.update();
                }
            };           

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };
            vm.create = function () {
                vm.saving = true;
                userService.createMake({
                    logoPicture: vm.uploadedFileName,
                    description: vm.job.description                    
                }).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $uibModalInstance.close()
                }).finally(function () {
                    vm.saving = false;
                });
            };
            vm.update = function () {
                vm.saving = true;
                userService.updateMake({
                    Id: makeId,
                    logoPicture: vm.job.logoPicture,
                    description: vm.job.description,
                    newFileName: vm.uploadedFileName
                }).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $uibModalInstance.close()
                }).finally(function () {
                    vm.saving = false;
                });
            }; 

            function init() {
                vm.loading = true;
                userService.getThisMake($.extend({ filter: makeId }, makeId))
                    .then(function (result) {
                        vm.job = result.data;
                    }).finally(function () {
                        vm.loading = false;
                    });
            }

            init();

            vm.uploader = new fileUploader({
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

            vm.uploader.onSuccessItem = function (fileItem, response, status, headers) {
                if (response.success) {
                    vm.uploadedFileName = response.result.fileName;
                } else {
                    abp.message.error(response.error.message);
                }
            };
             
        }
    ]);
})();