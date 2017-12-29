(function () {
    appModule.controller('tenant.views.quoteheaders.index', [
        '$scope', '$uibModal', '$stateParams', '$window' , 'uiGridConstants', 'abp.services.app.quote',
        function ($scope, $uibModal, $stateParams, $window, uiGridConstants, jobService) {
            
            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });
            vm.job = {};
            vm.quote = {};
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

            editableGrid = new EditableGrid("DemoGridAttach");
            vm.getHeaders = function () {
                vm.loading = true;
                jobService.getHeaders()
                    .then(function (result) {
                        var metadata = [];
                        var metadata = eval(result.data);
                        editableGrid.load({ "metadata": metadata });
                        editableGrid.attachToHTMLTable('htmlgrid');

                        editableGrid.setCellRenderer("copydelete", new CellRenderer({
                            render: function (cell, value) {
                                cell.innerHTML = "<a onclick=\"if (confirm('Are you sure you want to delete this quote ? ')) deleteRow(" + cell.rowIndex + ");\" style=\"cursor:pointer\">" +
                                    "<i class=\"fa fa-remove\" title=\"del\"></i></a>|<a onclick=\"if (confirm('Are you sure you want to clone this quote ? ')) editableGrid.duplicate(" + cell.rowIndex + ");\" style=\"cursor:pointer\">" +
                                    "<i class=\"fa fa-plus\" title=\"add\"></i></a>";
                            }
                        }));
                        editableGrid.renderGrid();

                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            vm.getQuotes = function () {
                vm.loading = true;
                jobService.getQuotes($.extend({ filter: $stateParams.id }, $stateParams.id))
                    .then(function (result) {
                        vm.quote = result.data.items;
                        $scope.rowcount = result.data.items.length;
                        if ($scope.rowcount == 0){
                            $scope.isShow = true;     
                        }
                        //else
                        //    $scope.showNew = false;     
                        //$scope.rowcount = $scope.rowcount + 1;
                        vm.estimatedRepairDays();
                    }).finally(function () {
                        vm.loading = false;
                    });
            };


            vm.estimatedRepairDays = function () {
                var total = 0;
                var totalvalue = 0;
                angular.forEach(vm.quote, function (q) {
                    total = total + (q.panelHrs + q.paintHrs + q.saHrs);
                    totalvalue = totalvalue + (q.partQty * q.partPrice) + (q.panelHrs * q.panelRate) + (q.paintHrs * q.paintRate) + (q.saHrs * q.saRate);
                })
                total = (total / 9).toFixed(2);
                $("#tdestimatedRepairDays").html(total);
                $("#tdtotal").html(totalvalue.toFixed(2))
            };


            vm.save = function () {
                vm.saving = true;                 
                var quote = [];
                $('#htmlgrid tbody tr').each(function (i, tr) {
                    var Id = $("#hdnId" + i + "").val();                    
                    if (Id == undefined) {
                        Id = 0;                         
                    } 
                    quote.push({
                        "actionid": "" + editableGrid.getValueAt(i, 1) + "",
                        "locationid": "" + editableGrid.getValueAt(i, 2) + "",
                        "description": "" + editableGrid.getValueAt(i, 3) + "",
                        "toOrder": $("#toOrder" + i + "").is(":checked"),
                        "outwork": $("#outwork" + i + "").is(":checked"),
                        "partQty": "" + editableGrid.getValueAt(i, 6) + "",
                        "partPrice": "" + editableGrid.getValueAt(i, 7) + "",
                        "part": "" + editableGrid.getValueAt(i, 9) + "",
                        "panelHrs": "" + editableGrid.getValueAt(i, 10) + "",
                        "panelRate": "" + editableGrid.getValueAt(i, 11) + "",
                        "paintHrs": "" + editableGrid.getValueAt(i, 13) + "",
                        "paintRate": "" + editableGrid.getValueAt(i, 14) + "",
                        "saHrs": "" + editableGrid.getValueAt(i, 16) + "",
                        "saRate": "" + editableGrid.getValueAt(i, 17) + "",
                        "notaxvat": $("#notaxvat" + i + "").is(":checked"),
                        "quoteId": $stateParams.id,
                        "id": Id
                    });

                });
                // $scope.display = quote;
                jobService.saveQuote(JSON.stringify({ 'quote': quote })).then(function () { 
                    abp.notify.info(app.localize('SavedSuccessfully'));                     
                    getQuotes();
                }).finally(function () {                    
                    vm.saving = false;                    
                });
            };
            deleteRow = function (c) {
                var quoteId = $("#hdnId" + c).val();
                if (quoteId == null) {
                    editableGrid.remove(c);
                }
                else {
                    jobService.deleteQuote($.extend({ filter: quoteId }, quoteId)).then(function () {
                        abp.notify.info(app.localize('QuoteRemovedSuccessfully'));                         
                        $window.location.href = "#!/tenant/quoteheaders/" + $stateParams.id;
                    }).finally(function () {
                        vm.saving = false;
                    });
                }
            };
            vm.getJobSummary();
            vm.getHeaders();
            vm.getQuotes();
        }]);
})();