(function () {

    appModule.controller('host.views.AddEditBroker.index', [
        '$scope', 'appSession', '$uibModal', '$stateParams', 'FileUploader', 'abp.services.app.broker',


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
            vm.uploadedFileName = null;

            vm.job.uploader = new fileUploader({
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

            vm.job.uploader.onSuccessItem = function (fileItem, response, status, headers) {
                if (response.success) {

                    var profileFilePath = abp.appPath + 'Temp/Downloads/' + response.result.fileName + '?v=' + new Date().valueOf();
                    vm.uploadedFileName = response.result.fileName;


                } else {
                    abp.message.error(response.error.message);
                }
            };
            vm.save = function () {
                if ($stateParams.id == null) {
                    vm.create();
                }
                else {
                    vm.update();
                }
            };
            vm.update = function () {                
                vm.saving = true;                 
                jobService.updateBrokerMaster({
                    Id: $stateParams.id,
                    logoPicture: vm.job.logoPicture,                  
                    BrokerName: vm.job.brokerName,
                    newFileName: vm.uploadedFileName, 
                    Mask: vm.job.mask,
                    countryid : vm.job.countryID
                }).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    window.location.href = "#!/host/Brokers";
                }).finally(function () {
                    vm.saving = false;
                });
            };
            vm.create = function () {
                vm.saving = true;

                jobService.createBrokerMaster({
                    LogoPicture: vm.uploadedFileName,
                    BrokerName: vm.job.brokerName,
                    Mask: vm.job.mask,
                    countryid : vm.job.countryID
                }).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    window.location.href = "#!/host/Brokers";
                }).finally(function () {
                    vm.saving = false;
                });
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
            vm.getBrokerMaster = function () {

                vm.loading = true;
                jobService.getBrokerMasterDetail($.extend({ filter: $stateParams.id }, $stateParams.id))
                    .then(function (result) {
                        vm.job = result.data;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };
            if ($stateParams.id != null) {
                vm.getBrokerMaster();
            }
            vm.getCountries();
        }]);
})();