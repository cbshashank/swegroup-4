define(function () {

    function ResultController(view, communication) {
        communication.onResultReceived = view.setModel.bind(view);
    }

    return ResultController;
});

