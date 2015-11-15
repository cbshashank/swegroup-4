define(function () {

    function ResultView() {
    }

    ResultView.prototype.setModel = function(model){
        this.model = model;
    };

    ResultView.prototype.getModel = function(){
        return this.model;
    };

    return ResultView;
});

