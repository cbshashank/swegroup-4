define(function () {
    var username = document.getElementById('username');
    var password = document.getElementById('password');
    var loginButton = document.getElementById('loginButton');

    function LoginView() {

        this.getCredentials = function () {
            var credentials = {};
            credentials["UserName"] = username.value;
            credentials["Password"] = password.value;
            return credentials;
        };

        this.submitCredentials = function (action) {
            loginButton.addEventListener('click', action);
        };
    }

    return LoginView;
});