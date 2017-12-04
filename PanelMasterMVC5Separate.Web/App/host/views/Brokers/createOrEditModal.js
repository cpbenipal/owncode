(function () {
    appModule.controller('host.views.Brokers.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.broker', 'FileUploader', 'brokerId',
        function ($scope, $uibModalInstance, userService, fileUploader, brokerId) {
            var vm = this;
            vm.saving = false;
            vm.job = {};
            vm.loading = false;
            vm.uploadedFileName = null;
            vm.currentUserId = abp.session.userId;
            vm.brokerId = brokerId;
            vm.save = function () {
                if (brokerId == null) {
                    vm.create();
                }
                else {
                    vm.update();
                }
            };
            vm.update = function () {
                vm.saving = true;
                userService.updateBrokerMaster({
                    Id: brokerId,
                    logoPicture: vm.job.logoPicture,
                    BrokerName: vm.job.brokerName,
                    newFileName: vm.uploadedFileName,
                    Mask: vm.job.mask,
                    countryid: vm.job.countryID
                }).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $uibModalInstance.close()
                }).finally(function () {
                    vm.saving = false;
                });
            };
            vm.create = function () {
                vm.saving = true;

                userService.createBrokerMaster({
                    LogoPicture: vm.uploadedFileName,
                    BrokerName: vm.job.brokerName,
                    Mask: vm.job.mask,
                    countryid: vm.job.countryID
                }).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $uibModalInstance.close()
                }).finally(function () {
                    vm.saving = false;
                });
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };
            $scope.countryList = []; //list of Countries
            vm.getCountries = function () {
                userService.getCountry()
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
            function init() {
                userService.getBrokerMasterDetail({
                    filter: brokerId
                }).then(function (result) {
                    vm.job = result.data;
                });
            }

            init();

            vm.uploader = new fileUploader({
                url: abp.appPath + 'Broker/UploadProfilePicture',
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


            vm.getCountries();
        }
    ]);
})();