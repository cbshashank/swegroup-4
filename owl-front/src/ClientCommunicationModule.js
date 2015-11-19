/**
 * Created by Scott on 10/7/2015.
 */

define(['jquery', 'ResultDisplay', 'views/AdminResultDisplay'], function ($, ResultDisplay, AdminResultDisplay) {

    function ClientCommunicationModule() {
    }

    var output_display = new ResultDisplay();
    var admin_display = new AdminResultDisplay();
    var serverAddress = "https://localhost:32297";

    ClientCommunicationModule.prototype.onResultReceived = function(result){};

    ClientCommunicationModule.prototype.sendAnswers = function (answers) {
        $.ajax({
            url: serverAddress,            // Location of the service
            type: "POST",                  //GET or POST or PUT or DELETE verb
            data: JSON.stringify(answers), //Data sent to server
            dataType: "text",
            success: function (in_data) {  //On Successful service call
                var result = JSON.parse(in_data);
                this.onResultReceived(result);
                output_display.updateResultDisplay(result);
            },
            error: (function () {          //When Service call fails
                alert("Error");
            })
        });
    };

    ClientCommunicationModule.prototype.getQuestions = function (onReceive) {
        $.ajax({
            url: serverAddress, 		// Location of the service
            type: "GET", 		//GET or POST or PUT or DELETE verb
            dataType: "text",
            success: function (in_data) {//On Successful service call
                alert(in_data);
                var result = JSON.parse(in_data);
                onReceive(result);
            },
            error: (function () {
                alert("Error");
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
                admin_display.updateResultDisplay(result);
            },
            error: (function () {
                alert("Error");
            })	// When Service call fails
        });

    };

    return ClientCommunicationModule;
});


