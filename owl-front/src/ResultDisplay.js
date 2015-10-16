/**
 * Created by Scott on 10/7/2015.
 */

define(['jquery'], function($){

    function ResultDisplay(){}

    ResultDisplay.prototype.updateResultDisplay = function(input){
//        $("#output_img").html("<img src=" + input.ImageUrl + "><//img>");
//        $("#output_desc").html(input.Description);
        $("#output_img").html("img url = " + input.ImageUrl);
        $("#output_desc").html("description = " + input.Description);
    };

    return ResultDisplay;
});