define(function () {

    function ResultView(resultDisplay) {
        this.resultDisplay = resultDisplay || document.getElementById("resultDisplay");
    }

    ResultView.prototype.setModel = function (model) {
        this.model = model;
        this.clearUI();
        this.className = "row";
        for (var i = 0; i < model.length; i++) {
            var item = model[i];
            var itemDisplay = document.createElement("div");
            itemDisplay.className = "col-xs-4 col-md-3";
            itemDisplay.appendChild(this.displayName(item.Name));
            itemDisplay.appendChild(this.displayImage(item.ImageURL));
            itemDisplay.appendChild(this.displayMoreResults(item.GoogleURL, item.GoogleImageURL));
            this.resultDisplay.appendChild(itemDisplay);
        }
    };

    ResultView.prototype.clearUI = function () {
        this.resultDisplay.innerHTML = "";
    };

    ResultView.prototype.displayName = function (name) {
        var header = document.createElement("h4");
        header.className = "text-left";
        header.innerHTML = name;
        return header;
    };

    ResultView.prototype.displayImage = function (imageURL) {
        var img = document.createElement("img");
        img.src = imageURL;
        img.width = "160";
        img.height = "160";
        var paragraph = document.createElement("p");
        paragraph.appendChild(img);

        return paragraph;
    };

    ResultView.prototype.displayMoreResults = function (googleURL, googleImageURL) {
        var btn = document.createElement("button");
        btn.type = "button";
        btn.innerHTML = "More Results";
        btn.onclick = function () {
            window.open(googleURL);
            window.open(googleImageURL);
        };
        btn.className = "span5";
        return btn;
    };

    ResultView.prototype.getModel = function () {
        return this.model;
    };

    return ResultView;
});

