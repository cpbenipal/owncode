var CurrentPage = function () {
     
    var handleRegister = function () {
         
        $('.form-horizontal').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "",
            rules: {                 
                OTP: {
                    minlength: 5,
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