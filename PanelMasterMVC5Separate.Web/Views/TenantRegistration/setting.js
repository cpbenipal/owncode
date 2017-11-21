﻿var CurrentPage = function () {
     
    var handleRegister = function () {
         
        $('.form-horizontal').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "",
            rules: {
                TenancyName: {
                    minlength: 4,
                    required: true
                },
                LoginName: {
                    minlength: 4,
                    required: true
                },
                FullName: {
                    required: true
                },
                CellNumber: {
                    required: true
                }, 
                FaximileeNumber: {
                    required: true
                },
                CellNumber: {
                    required: true
                },                            
                Address: {
                    required: true
                },
                City: {
                    required: true
                },
                Country: {
                    required: true
                },
                Timezone: {
                    required: true
                },  
                Currency: {
                    required: true
                },  
                TenanyPlan: {
                    required: true
                }, 
                //payment
                CardHoldersName: {
                    required: true
                },
                CardNumber: {
                    minlength: 16,
                    maxlength: 16,
                    required: true
                },
                CVV: {
                    digits: true,
                    required: true,
                    minlength: 3,
                    maxlength: 4
                },
                CardExpiration: {
                    required: true
                },
                'payment[]': {
                    required: true,
                    minlength: 1
                },
                BillingCountryCode: {
                    required: true
                },
                CompanyRegistrationNo: {
                    required: true
                },
                CompanyVatNo: {
                    required: true
                },
            },
            messages: {},

            invalidHandler: function (event, validator) {

            },

            highlight: function (element) { // hightlight error inputs

                $(element)
                    .closest('.form-group').addClass('has-error'); // set error class to the control group
            },

            unhighlight: function (element) { // revert the change done by hightlight
                $(element)
                    .closest('.form-group').removeClass('has-error'); // set error class to the control group
            },

            success: function (label) {
                label
                    .closest('.form-group').removeClass('has-error'); // set success class to the control group
            },             

            errorPlacement: function (error, element) {
                if (element.closest('.input-icon').size() === 1) {
                    error.insertAfter(element.closest('.input-icon'));
                } else {
                    error.insertAfter(element);
                }
            },

            submitHandler: function (form) {
                form.submit();
            }
        });

        $('.form-horizontal input').keypress(function (e) {
            if (e.which == 13) {
                if ($('.form-horizontal').valid()) {
                    $('.form-horizontal').submit();
                }
                return false;
            }
        });        
    }

    return {
        init: function () { 
            handleRegister();
        }
    };

}();

jQuery(document).ready(function () {
    CurrentPage.init();
});