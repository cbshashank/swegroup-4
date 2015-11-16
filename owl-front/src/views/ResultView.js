define(function () {

    function ResultView(imageContainer, googleResults, googleImages) {
        this.imageContainer = imageContainer || document.getElementById("imageContainer") ;
        this.googleResults = googleResults || document.getElementById("googleResults");
        this.googleImages =  googleImages || document.getElementById("googleImages");
    }

    ResultView.prototype.setModel = function(model){
        this.model = model;
        this.displayImage(model.ImageURL);
        this.displayGoogleResults(model.GoogleURL);
        this.displayGoogleImages(model.GoogleImagesURL);
    };

    ResultView.prototype.displayImage = function(imageURL){
        var img = document.createElement("img");
        img.src = imageURL;
        this.imageContainer.innerHTML = "";
        this.imageContainer.appendChild(img);
    };

    ResultView.prototype.displayGoogleResults = function(googleURL){
        var frame = document.createElement("iframe");
        frame.src = googleURL;
        this.googleResults.innerHTML = "";
        this.googleResults.appendChild(frame);
    };

    ResultView.prototype.displayGoogleImages = function(googleImageURL){
        var frame = document.createElement("iframe");
        frame.src = googleImageURL;
        this.googleImages.innerHTML = "";
        this.googleImages.appendChild(frame);
    };

    ResultView.prototype.getModel = function(){
        return this.model;
    };

    return ResultView;
});

