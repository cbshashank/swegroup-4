define(function () {

    function QuestionController(view, communication) {
        communication.getQuestions(view.setModel.bind(view));
        view.onAnswer(function () {
            communication.sendAnswers(view.getAnswers());
        });
    }

    return QuestionController;
});

