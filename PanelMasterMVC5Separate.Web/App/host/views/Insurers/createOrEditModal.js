(function () {
    appModule.controller('host.views.Insurers.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.insurer', 'FileUploader', 'insurerId',
        function ($scope, $uibModalInstance, jobService, fileUploader, insurerId) {
            var vm = this;
            vm.saving = false;
            vm.job = {};
            vm.loading = false;
            vm.uploadedFileName = null;
            vm.currentUserId = abp.session.userId;
            vm.insurerId = insurerId;
          

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };
            $scope.countryList = []; //list of Countries
            vm.getCountries = function () {
                jobService.getCountry()
                    .then(function (ins_obj) {

                        angular.forEach(ins_obj.data.items, function (insvalue, key1) {
                            $scope.countryList.push({
                                name: insvalue.country + " (" + insvalue.code + ")",
                                id: insvalue.id
                            });
                        });

                    }).finally(function () {
                        vm.loading = false;
                    });
            };            

            vm.uploader = new fileUploader({
                url: abp.appPath + 'Insurer/UploadProfilePicture',
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


            vm.save = function () {
                if (insurerId == null) {
                    vm.create();
                }
                else {
                    vm.update();
                }
            };

            vm.create = function () {
                vm.saving = true;

                jobService.createInsurerMaster({
                    logoPicture: vm.uploadedFileName,
                    insurerName: vm.job.insurerName,
                    mask: vm.job.mask,
                    countryid: vm.job.countryID
                }).then(function () {

                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $uibModalInstance.close();
                }).finally(function () {
                    vm.saving = false;
                });
            };

            vm.update = function () {
                vm.saving = true;
                jobService.updateInsurerMaster({
                    Id: insurerId,
                    logoPicture: vm.job.logoPicture,
                    insurerName: vm.job.insurerName,
                    newFileName: vm.uploadedFileName,
                    mask: vm.job.mask,
                    countryid: vm.job.countryID
                }).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $uibModalInstance.close();
                }).finally(function () {
                    vm.saving = false;
                });
            };

            vm.getInsurerMaster = function () {

                vm.loading = true;
                jobService.getInsurerMasterDetail($.extend({ filter: insurerId }, insurerId))
                    .then(function (result) {
                        vm.job = result.data;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };
            if (insurerId != null) {
                vm.getInsurerMaster();
            }

            vm.getCountries();
        }
    ]);
})();