define(function () {

    function ResultView(resultDisplay) {
        this.resultDisplay = resultDisplay || document.getElementById("resultDisplay");
    }

    ResultView.prototype.setModel = function (model) {
        this.model = model;
        this.clearUI();
        for (var i = 0; i < model.length; i++) {
            var item = model[i];
            var itemDisplay = document.createElement("div");
            itemDisplay.appendChild(this.displayImage(item.ImageURL));
            itemDisplay.appendChild(this.displayGoogleResults(item.GoogleURL));
            itemDisplay.appendChild(this.displayGoogleImages(item.GoogleImagesURL));
            this.resultDisplay.appendChild(itemDisplay);
        }
    };

    ResultView.prototype.clearUI = function () {
        this.resultDisplay.innerHTML = "";
    };

    ResultView.prototype.displayImage = function (imageURL) {
        var img = document.createElement("img");
        img.src = imageURL;
        return img;
    };

    ResultView.prototype.displayGoogleResults = function (googleURL) {
        var frame = document.createElement("iframe");
        frame.src = googleURL;
        return frame;
    };

    ResultView.prototype.displayGoogleImages = function (googleImageURL) {
        var frame = document.createElement("iframe");
        frame.src = googleImageURL;
        return frame;
    };

    ResultView.prototype.getModel = function () {
        return this.model;
    };

    return ResultView;
});

