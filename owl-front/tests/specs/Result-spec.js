define(['controllers/ResultController', 'views/ResultView'], function (ResultController, ResultView) {

    var COMM = {
        onResultReceive : null
    };

    var RESULTS = {};

    describe('Result UI', function () {
        describe('ResultController', function(){
            it('Should set onResultReceive callback at ClientCommunicationModule', function(){
                var view = new ResultView();
                new ResultController(view, COMM);
                expect(COMM.onResultReceive).not.toBeNull();
            });
            it('onResultReceive should set results to view', function(){
                var view = new ResultView();
                var controller = new ResultController(view, COMM);
                COMM.onResultReceive(RESULTS);
                expect(view.model).not.toBeNull();
            });
        });
    });
});