var CurrentPage = function () {

    var _passwordComplexityHelper = new app.PasswordComplexityHelper();

    var handleRegister = function () {       

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