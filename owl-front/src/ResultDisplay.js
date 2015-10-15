/**
 * Created by Scott on 10/7/2015.
 */

define(['jquery'], function($){

    function ResultDisplay(){}

    ResultDisplay.prototype.updateResultDisplay = function(input){
        $("#result_display").html(input.Color + " " + input.Shape + " " + input.Texture);
    };

    return ResultDisplay;
});