(function() {
    appModule.controller('tenant.views.costing.index', [
        '$scope', 'abp.services.app.person',
        function($scope, personService) {
            var vm = this;

            vm.persons = [];
             
        }
    ]);
})();