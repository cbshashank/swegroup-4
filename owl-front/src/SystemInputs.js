/**
 * Created by Scott on 10/7/2015.
 */
define(function(){
    // Need to tie this (or something like this) to whatever interface widgets are used. Actual structure may vary from this extremely simple placeholder
    function SystemInputs(){
    }

    SystemInputs.prototype.getInput = function(input_id){
        var result = "";
        if (input_id.equals("Color")) result = "Color:Red";
        return result;
    };

    return SystemInputs;
});