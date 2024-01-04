
$(function () {
    $('#show-password-button').click(function () {
        var passwordInput = $('input[name="Password"]');
        if (passwordInput.attr('type') === 'password') {
            passwordInput.attr('type', 'text');
            $(this).text('Hide Password');
        } else {
            passwordInput.attr('type', 'password');
            $(this).text('Show Password');
        }
    });
});