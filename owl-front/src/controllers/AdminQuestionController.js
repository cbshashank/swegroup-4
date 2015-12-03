define(function () {
    function AdminQuestionController(view, communication) {
        window.onload = communication.checkCredentials();

        communication.getQuestions(view.setModel.bind(view));

        view.insertData(function () {
            console.log(view.getAnswers());
            communication.insertData(view.getAnswers());
        });

        view.deleteData(function () {
            console.log(view.getDeletionSubject());
            communication.deleteData(view.getDeletionSubject());
        });

        view.logout(function() {
            communication.logout();
        });
    }

    return AdminQuestionController;
});

