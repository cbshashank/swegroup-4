/**
 * Created by Scott on 10/7/2015.
 */

define(['jquery'], function($){

    function AdminResultDisplay(){}

    AdminResultDisplay.prototype.updateResultDisplay = function(input){
        var output = "";
//        alert("size=" + input.length);
//        for (var i = 0; i < input.length; i++) {
        output = output + "<p><b>Added element: </b><br//>";
        output = output + "Name: " + input.Name + "<br//>";
        output = output + "ImageURL: " + input.ImageUrl + "<br//>";
        output = output + "USState: " + input.USState + "<br//>";
        output = output + "Type: " + input.Type + "<br//>";
        output = output + "ColorFlower: " + input.ColorFlower + "<br//>";
        output = output + "ColorFoliage: " + input.ColorFoliage + "<br//>";
        output = output + "ColorFruitSeed: " + input.ColorFruitSeed + "<br//>";
        output = output + "Shape: " + input.Shape + "<br//>";
        output = output + "TextureFoliage: " + input.TextureFoliage + "<br//>";
        output = output + "Pattern: " + input.Pattern + "<br//>";
        output = output + "<//p>";
//            output = output + "<p>" + "img url = " + input[i].ImageURL + " description = " + input[i].Description+ "<//p>";
//        }
        $("#output_display").html(output);
    };

    return AdminResultDisplay;
});