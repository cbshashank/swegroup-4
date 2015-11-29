define(function () {

    function AdminQuestionController(view, communication) {
        communication.getQuestions(view.setModel.bind(view));
        view.insertData(function () {
            console.log(view.getAnswers());
            communication.insertData(view.getAnswers());
        });
        view.deleteData(function () {
            console.log(view.getAnswers());
            communication.deleteData(view.getAnswers());
        });
    }

    return AdminQuestionController;
});

