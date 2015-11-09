define(function () {

    function QuestionController(view, communication) {
        communication.getQuestions(view.setModel.bind(view));
        view.onAnswer(function () {
            console.log(view.getAnswers());
            communication.sendAnswers(view.getAnswers());
        });
    }

    return QuestionController;
});

