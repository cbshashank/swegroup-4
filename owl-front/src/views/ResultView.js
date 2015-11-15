define(function () {

    function ResultView(imageContainer, descriptionContainer) {
        this.imageContainer = document.getElementById("imageContainer") || imageContainer;
        this.descriptionContainer = document.getElementById("descriptionContainer") || descriptionContainer;
    }

    ResultView.prototype.setModel = function(model){
        this.model = model;
        this.displayImage(model.ImageURL);
        this.displayDescription(model.Description);
    };

    ResultView.prototype.displayImage = function(imageURL){
        var img = document.createElement("img");
        img.src = imageURL;
        this.imageContainer.appendChild(img);
    };

    ResultView.prototype.displayDescription = function(description){
        this.descriptionContainer.innerHTML = description;
    };

    ResultView.prototype.getModel = function(){
        return this.model;
    };

    return ResultView;
});

