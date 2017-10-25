(function () {

    appModule.controller('tenant.views.landingPage.index', [
        '$scope', '$uibModal', '$stateParams', 'abp.services.app.branchClaim',


        function ($scope, $uibModal, $stateParams, jobService) {
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
            vm.BranchID = abp.session.tenantId;
           
            vm.getJobs = function () {

                vm.loading = true;
                jobService.getJobDetails($.extend({ filter: $stateParams.id }, $stateParams.id))
                    .then(function (result) {

                        vm.job.id = $stateParams.id;
                        vm.job.RegNo = result.data.regNo;
                        vm.job.VinNumber = result.data.vinNumber;
                        vm.job.Year = result.data.year;
                        vm.job.Colour = result.data.colour;
                        vm.job.Make = result.data.manufacture;
                        vm.job.Model = result.data.model;
                        vm.job.Cell = result.data.tel;
                        vm.job.Email = result.data.email;
                        vm.job.ClaimStatusDesc = result.data.claimStatusDescription;
                        vm.job.Surname = result.data.surname;
                        vm.job.Name = result.data.name;
                        
                       
                    }).finally(function () {
                        vm.loading = false;
                    });

                $scope.tab_or_modal = 'tab';
            };

            vm.getJobs();

        }]);
})();