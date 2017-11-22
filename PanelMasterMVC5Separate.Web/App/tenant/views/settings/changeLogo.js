(function () {
    appModule.controller('tenant.views.settings.changeLogo', [
        '$scope', 'appSession', '$uibModalInstance', 'FileUploader', 'abp.services.app.tenantSettings',
        function ($scope, appSession, $uibModalInstance, fileUploader, profileService) {
            var vm = this;
            vm.logoUploader = null;
           
            vm.logoUploader = undefined;
            vm.uploadLogo = function () {
                vm.logoUploader.uploadAll();
            };

            

            function initUploaders() {
                vm.logoUploader = createUploader(
                    "Company/UploadLogo",
                    [{
                        name: 'imageFilter',
                        fn: function (item, options) {
                            var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
                            if ('|jpg|jpeg|png|gif|'.indexOf(type) === -1) {
                                abp.message.warn(app.localize('UploadLogo_Info'));
                                return false;
                            }

                            if (item.size > 30720) //30KB
                            {
                                abp.message.warn(app.localize('UploadLogo_Info'));
                                return false;
                            }

                            return true;
                        }
                    }],
                    function (result) {
                        appSession.tenant.logoFileType = result.fileType;
                        appSession.tenant.logoId = result.id;
                        $('#LogoFileInput').val(null);
                        $uibModalInstance.close();
                    }
                );
            }
             
            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            function createUploader(url, filters, success) {
                var uploader = new fileUploader({
                    url: abp.appPath + url,
                    headers: {
                        "X-XSRF-TOKEN": abp.security.antiForgery.getToken()
                    },
                    queueLimit: 1,
                    removeAfterUpload: true
                });

                if (filters) {
                    uploader.filters = filters;
                }

                uploader.onSuccessItem = function (item, ajaxResponse, status) {
                    if (ajaxResponse.success) {
                        abp.notify.info(app.localize('SavedSuccessfully'));
                        success && success(ajaxResponse.result);
                    } else {
                        abp.message.error(ajaxResponse.error.message);
                    }
                };

                return uploader;
            }
            initUploaders();
        }
    ]);
})();