(function () {

    appModule.controller('tenant.views.landingPage.index', [
        '$scope', '$uibModal', '$stateParams', 'abp.services.app.branchClaim',


        function ($scope, $uibModal, $stateParams, jobService) {
            var vm = this;
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });
            
        }]);
})();