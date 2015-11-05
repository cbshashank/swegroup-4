define(function () {

    function QuestionController(view, communication) {
        view.setModel(communication.getQuestions());
        view.onAnswer(function () {
            console.log(view.getAnswers());
            communication.sendCharacteristics(view.getAnswers());
        });
    }

    return QuestionController;
});

