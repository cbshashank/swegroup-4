/**
 * Created by Scott on 10/7/2015.
 */

define(['jquery'], function($){

    function ResultDisplay(){}

    ResultDisplay.prototype.updateResultDisplay = function(input){
//        $("#output_img").html("<img src=" + input.ImageUrl + "><//img>");
//        $("#output_desc").html(input.Description);
        var output = "";
//        alert("size=" + input.length);
        for (var i = 0; i < input.length; i++) {
            output = output + "<p><img src=" + input[i].ImageUrl + "><//img><//p>";
            output = output + "<p>" + input[i].Name + "<//p>";
//            output = output + "<p>" + "img url = " + input[i].ImageURL + " description = " + input[i].Description+ "<//p>";
//            $("#output_img").html("img url = " + input.ImageURL);
//            $("#output_desc").html("description = " + input.Description);
        }
        $("#output_desc").html(output);
    };

    return ResultDisplay;
});