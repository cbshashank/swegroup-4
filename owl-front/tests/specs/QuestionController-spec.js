define(['controllers/QuestionController'], function (Controller) {

    function QuestionViewMock() {
        this.model = null;
        this.answerCallback = null;
        this.setModel = function (model) {
            this.model = model;
        };
        this.onAnswer = function (callback) {
            this.answerCallback = callback;
        };
    }

    describe('QuestionController', function () {
        it('assigns a model to a view', function () {
            var view = new QuestionViewMock();
            var controller = new Controller(view);
            expect(view.model).not.toBeNull();
        });

        it('assigns a callback to answer button', function () {
            var view = new QuestionViewMock();
            var controller = new Controller(view);
            expect(view.answerCallback).not.toBeNull();
        });
    });
});