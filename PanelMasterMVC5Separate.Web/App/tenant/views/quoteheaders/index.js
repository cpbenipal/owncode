﻿(function () {
    appModule.controller('tenant.views.quoteheaders.index', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.quote',
        function ($scope, $uibModal, $stateParams, uiGridConstants, jobService) {

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

            $scope.lstactions = [{ id: 1, name: 'Air-conditioners regas' }, { id: 2, name: 'Alternate/Generic parts' }, { id: 3, name: 'Assemble/Disassemble' }, { id: 4, name: 'Blend' }, { id: 5, name: 'Check' }, { id: 6, name: 'Diagnostics - dealer warranty checks / aqr /electronic measuring systems' }, { id: 7, name: 'Fit' }, { id: 8, name: 'Frame' }, { id: 9, name: 'Glass replacement' }, { id: 10, name: 'jig hire (celette & other):' }, { id: 11, name: 'mag repair' }, { id: 12, name: 'Mechanical' }, { id: 13, name: 'Mechanical other' }, { id: 14, name: 'Mechanical: upholstery / trimming ' }, { id: 15, name: 'Miscellaneous' }, { id: 16, name: 'New Parts' }, { id: 17, name: 'on/off jigs / bench, setup, floor anchorage' }, { id: 18, name: 'Paintles Dent Repair' }, { id: 19, name: 'Polish' }, { id: 20, name: 'Repair' }, { id: 21, name: 'Rustproofing, body filler and underbody sealant, seam sealer' }, { id: 22, name: 'Salvageable Parts' }, { id: 23, name: 'Settlement discount' }, { id: 24, name: 'Spray' }, { id: 25, name: 'Storage' }, { id: 26, name: 'Sundry Parts' }, { id: 27, name: 'Supplies - Consumables' }, { id: 28, name: 'Supplies - Paint' }, { id: 29, name: 'Towing' }, { id: 30, name: 'Transportation cost parts - freight/courier/railage charges' }, { id: 31, name: 'Transportation costs for parts - Road' }, { id: 32, name: 'Tyres' }, { id: 33, name: 'Used/2cnd hand parts' }, { id: 34, name: 'Valet' }, { id: 35, name: 'Waste Collection' }, { id: 36, name: 'Wheel alignment : 4 x 4 / all wheel drive / specialised' }, { id: 37, name: 'Wheel alignment : electrical repairs' }, { id: 38, name: 'Wheel alignment : Normal' }];                       

            $scope.lstlocations = [{ id: 1, name: 'Back Boot' }, { id: 2, name: 'Back Bumper' }, { id: 3, name: 'Back Left Door' }, { id: 4, name: 'Back Left Fender' }, { id: 5, name: 'Back Left Wheel' }, { id: 6, name: 'Back Right Door' }, { id: 7, name: 'Back Right Fender' }, { id: 8, name: 'Back Right Wheel' }, { id: 9, name: 'Back Window' }, { id: 10, name: 'Chassis' }, { id: 11, name: 'Electrical' }, { id: 12, name: 'Front Bonnet' }, { id: 13, name: 'Front Bumper' }, { id: 14, name: 'Front Grill' }, { id: 15, name: 'Front Left Door' }, { id: 16, name: 'Front Left Fender' }, { id: 17, name: 'Front Left Headlights' }, { id: 18, name: 'Front Left Wheel' }, { id: 19, name: 'Front Right Door' }, { id: 20, name: 'Front Right Fender' }, { id: 21, name: 'Front Right Headlights' }, { id: 22, name: 'Front Right Wheel' }, { id: 23, name: 'Left Side Mirror' }, { id: 24, name: 'Mechanical' }, { id: 25, name: 'Other' }, { id: 26, name: 'Right Side Mirror' }, { id: 27, name: 'Roof' }, { id: 28, name: 'Slam Tray' }, { id: 29, name: 'Spare Wheel' }, { id: 30, name: 'Windscreen' }];

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
               
                        var metadata = [];
                    
                                              
                        metadata.push({ name: "Ref#", datatype: "integer", editable: false});
                        metadata.push({ name: "action", datatype: "string", editable: true});
                        metadata.push({ name: "location", datatype: "string", editable: true});
                        metadata.push({ name: "description", datatype: "string", editable: true});
                        metadata.push({ name: "towork", datatype: "boolean", editable: true});
                        metadata.push({ name: "outwork", datatype: "boolean", editable: true});
                        metadata.push({ name: "quantity", datatype: "integer", editable: true});
                        metadata.push({ name: "price", datatype: "double(2)", editable: true});
                        metadata.push({ name: "total", datatype: "double(2)", editable: false});
                        metadata.push({ name: "part", datatype: "string", editable: true});
                        metadata.push({ name: "pbhrs", datatype: "double(2)", editable: true});
                        metadata.push({ name: "pbrate", datatype: "double(2)", editable: true});
                        metadata.push({ name: "pbvalue", datatype: "double(2)", editable: false});
                        metadata.push({ name: "phrs", datatype: "double(2)", editable: true});
                        metadata.push({ name: "prate", datatype: "double(2)", editable: true});
                        metadata.push({ name: "pvalue", datatype: "double(2)", editable: false});
                        metadata.push({ name: "sahrs", datatype: "double(2)", editable: true});
                        metadata.push({ name: "sarate", datatype: "double(2)", editable: true});
                        metadata.push({ name: "savalue", datatype: "double(2)", editable: false});
                        metadata.push({ name: "notaxvat", datatype: "html", editable: false});
                        metadata.push({ name: "gtotal", datatype: "double(2)", editable: false}); 
                        metadata.push({ name: "copydelete", datatype: "html", editable: false});  
                        editableGrid.load({ "metadata": metadata });
                        
                        
                        editableGrid.setCellRenderer("copydelete", new CellRenderer({
                            render: function (cell, value) {
                                
                                cell.innerHTML = "<span><a onclick=\"deleteRow(" + cell.rowIndex + ");\" style=\"cursor:pointer\">" +
                                    "<i class=\"fa fa-remove\" title=\"Delete Line\"></i></a>  <a onclick=\"duplicate(" + cell.rowIndex + ");\" style=\"cursor:pointer\">" +
                                    "<i class=\"fa fa-copy\" title=\"Clone Line\"></i></a>  <a onclick=\"addnewline(" + cell.rowIndex + ");\" style=\"cursor:pointer\">" +
                                    "<i class=\"fa fa-plus\" title=\"Add Blank Line\"></i></a></span>";
                            }
                        }));
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
           

            vm.RepairDays = function () {
                var totalhours = 0;
                var totalMoney = 0;                    
                $('#htmlgrid tbody tr').each(function (i, tr) {
                totalhours = totalhours + (editableGrid.getValueAt(i, 10) + editableGrid.getValueAt(i, 13) + editableGrid.getValueAt(i, 16));
                totalMoney = totalMoney + editableGrid.getValueAt(i, 20);
                });
                totalhours = (totalhours / 9).toFixed(2);
                $("#tdestimatedRepairDays").html(totalhours);
                $("#tdtotal").html(totalMoney.toFixed(2));
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
                        "qaction": "" + editableGrid.getValueAt(i, 1) + "",
                        "qlocation": "" + editableGrid.getValueAt(i, 2) + "",
                        "description": "" + editableGrid.getValueAt(i, 3) + "",
                        "toOrder": "" + editableGrid.getValueAt(i, 4) + "",
                        "outwork": "" + editableGrid.getValueAt(i, 5) + "",
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
                        "repairerEstimatedDays" : vm.repairerEstimatedDays,
                        "estimatedRepairDays" : $("#tdestimatedRepairDays").html(),
                        "totalQuotedValue" : $("#tdtotal").html(),
                        "id": Id    
                    });
                    // }
                });
                
  
                jobService.saveQuote(JSON.stringify({ 'quote': quote })).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    vm.getQuotes();
                }).finally(function () {
                    vm.saving = false;                  
                });
                 
            };

            deleteRow = function (c) {
               abp.message.confirm(app.localize('Are you sure to delete this line?', "DeleteQuoteLine"),function (isConfirmed) {
                    if (isConfirmed) {    
                            var quoteId = $("#hdnId" + c).val();
                                if (quoteId == null) {
                                    editableGrid.remove(c);
                                    abp.notify.info(app.localize('QuoteRemovedSuccessfully')); 
                                    vm.estimatedRepairDays();                         
                                }
                                else {
                                    jobService.deleteQuote($.extend({ filter: quoteId }, quoteId)).then(function () {
                                    abp.notify.info(app.localize('QuoteRemovedSuccessfully'));
                                     vm.getQuotes();
                                    }).finally(function () {
                                    vm.saving = false;
                                    });
                               }
                        }
                });           
            };
             duplicate = function (c) {
                abp.message.confirm(app.localize('Are you sure to clone this line?', "duplicateQuoteLine"),function (isConfirmed) {
                    if (isConfirmed) { 
                        editableGrid.duplicate(c);
                        vm.RepairDays();
                        }
                });       
            };
            addnewline = function (c) {
                abp.message.confirm(app.localize('Are you sure to add blank line?', "Addnewline"),function (isConfirmed) {
                if (isConfirmed) { 
                editableGrid.addnewline(c);
                vm.RepairDays();
                }
                }); 
            };
                
             vm.getQuotes = function () {
                    vm.loading = true;
                    jobService.getQuotes($.extend({ filter: $stateParams.id }, $stateParams.id)).then(function (result) {
                                    vm.quote = result.data.items;                        
                        
                            $('#htmlgrid tbody').html("");
                            angular.forEach(vm.quote, function (q,c) {                        
                                    var r = c+1;
                                    var chk = q.noTaxVat?"checked":"";
                              
                                    $('#htmlgrid tbody').append("<tr id='R"+r+"'><td>"+r+"</td><td>"+q.qAction+"</td><td>"+q.qLocation+"</td><td>"+q.description+"</td><td>"+q.toOrder+"</td><td>"+q.outwork+"</td><td>"+q.partQty+"</td><td>"+q.partPrice+"</td><td>"+q.partQty*q.partPrice+"</td><td>"+q.part+"</td><td>"+q.panelHrs+"</td><td>"+q.panelRate+"</td><td>"+q.panelHrs*q.panelRate+"</td><td>"+q.paintHrs+"</td><td>"+q.paintRate+"</td><td>"+q.paintHrs*q.paintRate+"</td><td>"+q.saHrs+"</td><td>"+q.saRate+"</td><td>"+q.saHrs*q.saRate+"</td><td><input type=\"checkbox\" name=\"notaxvat"+c+"\" id=\"notaxvat"+c+"\" "+chk+" /><input type=\"hidden\" name=\"hdnId"+c+"\" id=\"hdnId"+c+"\" value=\""+q.id+"\" /></td><td>"+((q.partQty*q.partPrice)+(q.panelHrs*q.panelRate)+(q.paintHrs*q.paintRate)+(q.saHrs*q.saRate))+"</td><td></td>");  
                            })
                            editableGrid.attachToHTMLTable('htmlgrid'); 
                            editableGrid.renderGrid(); 
                            vm.repairerEstimatedDays = vm.quote[0].repairerEstimatedDays;
                            vm.estimatedRepairDays();
                        }).finally(function () {
                            vm.loading = false;
                        });  
                };                  

 
            vm.getJobSummary();
            vm.getHeaders();
            vm.getQuotes();                        
        }]);
})();