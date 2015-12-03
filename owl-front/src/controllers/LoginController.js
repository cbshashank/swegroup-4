define(function () {

    function LoginController(view, communication) {

        view.submitCredentials(function () {
            console.log(view.getCredentials());
            communication.login(view.getCredentials());
        });
    }

    return LoginController;
});

