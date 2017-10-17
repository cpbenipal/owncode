(function () {

    appModule.controller('tenant.views.vehicle_information.index', [
        '$scope', '$uibModal', '$stateParams', 'abp.services.app.branchClaim',


        function ($scope, $uibModal, $stateParams, jobService) {
            var vm = this;
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.saving = false;
            vm.job = {};
            vm.existing_job = {};
            vm.client = {};

            vm.advancedFiltersAreShown = false;
            vm.filterText = $stateParams.filterText || '';
            vm.currentUserId = abp.session.userId;
            vm.BranchID = abp.session.tenantId;
            vm.job.id = $stateParams.id;


            vm.getJobs = function () {

                vm.loading = true;
                jobService.getJobDetails($.extend({ filter: $stateParams.id }, $stateParams.id))
                    .then(function (result) {

                        vm.job.id = $stateParams.id;                       
                        vm.job.RegNo = result.data.regNo;
                        vm.job.VinNumber = result.data.vinNumber;
                        vm.job.Year = result.data.year;
                        vm.job.Colour = result.data.colour;

                        vm.existing_job.RegNo = result.data.regNo;
                        vm.existing_job.VinNumber = result.data.vinNumber;
                        vm.existing_job.Year = result.data.year;
                        vm.existing_job.Colour = result.data.colour;
                         
                    }).finally(function () {
                        vm.loading = false;
                    });
               
                $scope.tab_or_modal = 'tab';
            };           

           

        }]);
})();