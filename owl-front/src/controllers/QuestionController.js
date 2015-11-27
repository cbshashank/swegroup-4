define(function () {

    function QuestionController(view, communication) {
        view.onAnswer = function () {
            communication.sendAnswers(view.getAnswers());
        };
        communication.getQuestions(view.setModel.bind(view));
    }

    return QuestionController;
});

