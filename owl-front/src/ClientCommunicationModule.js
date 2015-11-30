/**
 * Created by Scott on 10/7/2015.
 */

define(['jquery', 'views/AdminResultDisplay'], function ($, AdminResultDisplay) {

    function ClientCommunicationModule() {
    }

    //var output_display = new ResultDisplay();
    var admin_display = new AdminResultDisplay();
    var serverAddress = "http://localhost:32296";

    ClientCommunicationModule.prototype.onResultReceived = function(result){};

    ClientCommunicationModule.prototype.sendAnswers = function (answers) {
        var that = this;
        $.ajax({
            url: serverAddress,            // Location of the service
            type: "POST",                  //GET or POST or PUT or DELETE verb
            data: JSON.stringify(answers), //Data sent to server
            dataType: "text",
            success: function (in_data) {  //On Successful service call
                var result = JSON.parse(in_data);
                that.onResultReceived(result);
                //output_display.updateResultDisplay(result);
            },
            error: (function () {          //When Service call fails
                alert("Error: Question posting unavailable");
            })
        });
    };

    ClientCommunicationModule.prototype.getQuestions = function (onReceive) {
        $.ajax({
            url: serverAddress, 		// Location of the service
            type: "GET", 		//GET or POST or PUT or DELETE verb
            dataType: "text",
            success: function (in_data) {//On Successful service call
                // **** FOR DEBUGGING --- alert(in_data);
                var result = JSON.parse(in_data);
                onReceive(result);
            },
            error: (function () {
                alert("Error: Questions unretrievable");
            })
        })
    };

    ClientCommunicationModule.prototype.insertData = function (data) {
        //var out = JSON.stringify(data);
        //var in_data = JSON.parse(out);
        //admin_display.updateResultDisplay(in_data);
        $.ajax({
            url: serverAddress, 		// Location of the service
            type: "PUT", 		//GET or POST or PUT or DELETE verb
            data: JSON.stringify(data), //Data sent to server //"This is a test",
            dataType: "text",
            success: function (in_data) {//On Successful service call
                //alert("input = " + in_data);
                var result = JSON.parse(in_data);
                //                alert("result=" + result);

                var output = "";
                output = output + "<p><b>Added Element: </b><br//>";
                output = output + "PlantId: " + result.PlantId + "<br//>";
                output = output + "Name: " + result.Name + "<br//>";
                output = output + "ImageURL: " + result.ImageURL + "<br//>";
                output = output + "USState: " + result.USState + "<br//>";
                output = output + "Type: " + result.Type + "<br//>";
                output = output + "ColorFlower: " + result.ColorFlower + "<br//>";
                output = output + "ColorFoliage: " + result.ColorFoliage + "<br//>";
                output = output + "ColorFruitSeed: " + result.ColorFruitSeed + "<br//>";
                output = output + "Shape: " + result.Shape + "<br//>";
                output = output + "TextureFoliage: " + result.TextureFoliage + "<br//>";
                output = output + "Pattern: " + result.Pattern + "<br//>";
                output = output + "<//p>";

                admin_display.updateResultDisplay(output);
            },
            error: (function () {
                alert("Error: data insertion failed");
            })	// When Service call fails
        });

    };

    ClientCommunicationModule.prototype.deleteData = function (data) {
        var check = window.confirm("Are you sure you want to delete " + data.PlantId + "?");
        if (check) {
            $.ajax({
                url: serverAddress, 		// Location of the service
                type: "DELETE", 		//GET or POST or PUT or DELETE verb
                data: JSON.stringify(data), //Data sent to server //"This is a test",
                dataType: "text",
                success: function (in_data) {//On Successful service call
                    //alert("input = " + in_data);
                    var result = JSON.parse(in_data);
                    //                alert("result=" + result);

                    var output = "";
                    output = output + "<p><b>Deleted Element: </b><br//>";
                    output = output + "PlantId: " + result.PlantId + "<br//>";
                    output = output + "<//p>";

                    admin_display.updateResultDisplay(output);
                },
                error: (function () {
                    alert("Error: data deletion failed");
                })	// When Service call fails
            });
        }
    };

    return ClientCommunicationModule;
});


