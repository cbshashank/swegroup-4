define(['controllers/ResultController', 'views/ResultView'], function (ResultController, ResultView) {

    var COMM = {
        onResultReceive: null
    };

    var RESULTS = {

    };

    function mockElement(){
        return {
          appendChild: function(){}
        };
    }

    describe('Result UI', function () {
        describe('ResultController', function () {
            it('Should set onResultReceive callback at ClientCommunicationModule', function () {
                var view = new ResultView(mockElement(), mockElement(), mockElement());
                new ResultController(view, COMM);
                expect(COMM.onResultReceived).not.toBeNull();
            });
            it('onResultReceive should set results to view', function () {
                var view = new ResultView(mockElement(), mockElement(), mockElement());
                var controller = new ResultController(view, COMM);
                COMM.onResultReceived(RESULTS);
                expect(view.model).not.toBeNull();
            });
        });
    });
});