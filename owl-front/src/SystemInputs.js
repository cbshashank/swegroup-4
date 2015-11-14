/**
 * Created by Scott on 10/7/2015.
 */
define(function () {
    // Need to tie this (or something like this) to whatever interface widgets are used. Actual structure may vary from this extremely simple placeholder
    function SystemInputs() { }

    SystemInputs.prototype.getInput = function (input_id) {
        var result = "";
        if (input_id == "Color")
            result = "Color:Red";
        else if (input_id == "Location")
            result = "Location:Western_US";
        else if (input_id == "Setting")
            result = "Setting:Rural";
        else if (input_id == "Shape")
            result = "Shape:Vine";
        else if (input_id == "Texture")
            result = "Texture:Rough";
        else if (input_id == "Smell")
            result = "Smell:Sweet";
        else if (input_id == "Pattern")
            result = "Pattern:Clusters";
        return result;
    };

    return SystemInputs;
});