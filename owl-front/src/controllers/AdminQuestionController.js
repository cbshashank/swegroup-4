define(function () {

    function AdminQuestionController(view, communication) {
        communication.getQuestions(view.setModel.bind(view));
        view.insertData(function () {
            console.log(view.getAnswers());
            communication.insertData(view.getAnswers());
        });
    }

    return AdminQuestionController;
});

