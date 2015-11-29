/**
 * Created by Scott on 10/7/2015.
 */

define(['jquery'], function($){

    function AdminResultDisplay(){}

    AdminResultDisplay.prototype.updateResultDisplay = function(output){
        $("#output_display").html(output);
    };

    return AdminResultDisplay;
});