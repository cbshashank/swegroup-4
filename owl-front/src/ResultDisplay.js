/**
 * Created by Scott on 10/7/2015.
 */

define(['jquery'], function($){

    function ResultDisplay(){}

    ResultDisplay.prototype.updateResultDisplay = function(input){
        var output = "";
        var length = input.length;
        if (length == 0)
        {   
            output = "<p><font size=15>No matching plants found.</font></p>";   
        }
        else
        {
            for (var i = 0; i < input.length; i++) {
                output = output + "<p><img src=" + input[i].ImageURL + " height=200 width=200></img></p>";
                output = output + "<p>" + input[i].Name + "</p>";
            }
        }
        $("#output_desc").html(output);
    };

    return ResultDisplay;
});