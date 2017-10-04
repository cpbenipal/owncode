(function() {
    appModule.controller('tenant.views.quoting.index', [
        '$scope', 'abp.services.app.person',
        function($scope, personService) {
            var vm = this;

            vm.persons = [];

            personService.getPeople({}).then(function(result) {
                vm.persons = result.data.items;
            });
        }
    ]);
})();