var CurrentPage = function () {

    jQuery.validator.addMethod("customUsername", function (value, element) {
        if (value === $('input[name="AdminEmailAddress"]').val()) {
            return true;
        }

        return !$.validator.methods.email.apply(this, arguments);
    }, abp.localization.localize("RegisterFormUserNameInvalidMessage"));

    var _passwordComplexityHelper = new app.PasswordComplexityHelper();

    var handleRegister = function () {

        $.validator.addMethod(
            "regex",
            function (value, element, regexp) {
                var re = new RegExp(regexp);
                return this.optional(element) || re.test(value);
            },
            app.localize('TenantName_Regex_Description'));

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
                AdminEmailAddress: {
                    required: true,
                    email: true
                },
                AdminPassword: {
                    required: true
                },
                AdminPasswordRepeat: {
                    required: true
                },
                AdminPasswordRepeat: {
                    equalTo: "#admin_password"
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

        _passwordComplexityHelper.setPasswordComplexityRules($("input[name=AdminPassword]"), window.passwordComplexitySetting);
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