(function () {

    appModule.controller('tenant.views.dates_information.index', [
        '$scope', '$uibModal', '$stateParams', 'abp.services.app.branchClaim',


        function ($scope, $uibModal, $stateParams, jobService) {
            var vm = this;
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.saving = false;
           
            vm.advancedFiltersAreShown = false;
            vm.filterText = $stateParams.filterText || '';
           
            


        }]);
})();