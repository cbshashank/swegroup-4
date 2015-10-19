/**
 * Created by Scott on 10/7/2015.
 */

define(['jquery','ResultDisplay'], function($,ResultDisplay){

    function ClientCommunicationModule(){}

    var output_display = new ResultDisplay();
    var serverAddress = "http://localhost:32297"
//    var serverAddress = "http://localhost:3000"

    ClientCommunicationModule.prototype.sendCharacteristics = function(output){
        var questionText = document.getElementById('form');

        $.ajax({
            url: serverAddress, 		// Location of the service
            type: "POST", 		//GET or POST or PUT or DELETE verb
            data: JSON.stringify(output), //Data sent to server //"This is a test",
            dataType: "text",
            success: function (in_data) {//On Successful service call
                alert("input = " + in_data);
                var result = JSON.parse(in_data);
//                alert("result=" + result);
                output_display.updateResultDisplay(result);
            },
            error: (function() { alert("Error"); })	// When Service call fails
        });

    };

    return ClientCommunicationModule;
});


