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
                    "TenantCustomization/UploadCompanyLogo",
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
                    }
                );
            initUploaders();
        }
    ]);
})();