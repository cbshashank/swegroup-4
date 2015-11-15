define(function () {

    function ResultController(view, communication) {
        communication.onResultReceive = view.setModel.bind(view);
    }

    return ResultController;
});

