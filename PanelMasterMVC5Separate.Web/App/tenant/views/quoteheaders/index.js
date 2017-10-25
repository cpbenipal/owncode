(function () {

    appModule.controller('tenant.views.quoteheaders', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.quote',
        function ($scope, $uibModal, $stateParams, uiGridConstants, jobService) {

            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });
            vm.job = {};
            vm.saving = false;
            vm.loading = false;
            vm.advancedFiltersAreShown = false;
            vm.filterText = $stateParams.filterText || '';
            vm.currentUserId = abp.session.userId;
            vm.TenantId = abp.session.tenantId;

            vm.getJobSummary = function () {
                vm.loading = true;
                jobService.getQuoteJobSummary($.extend({ filter: $stateParams.id }, $stateParams.id))
                    .then(function (result) {
                        vm.job = result.data;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            vm.getJobSummary();

        }]);
})();