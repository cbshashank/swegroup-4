/**
 * Created by Scott on 10/7/2015.
 */

define(['jquery', 'ResultDisplay', 'views/AdminResultDisplay'], function ($, ResultDisplay, AdminResultDisplay) {

    function ClientCommunicationModule() {
    }

    var output_display = new ResultDisplay();
    var admin_display = new AdminResultDisplay();
    var serverAddress = "https://localhost:32297";

    ClientCommunicationModule.prototype.sendAnswers = function (answers) {
        //var questionText = document.getElementById('form');

        $.ajax({
            url: serverAddress, 		// Location of the service
            type: "POST", 		//GET or POST or PUT or DELETE verb
            data: JSON.stringify(answers), //Data sent to server //"This is a test",
            dataType: "text",
            success: function (in_data) {//On Successful service call
                //alert("input = " + in_data);
                var result = JSON.parse(in_data); 
                //                alert("result=" + result);
                output_display.updateResultDisplay(result);
            },
            error: (function () {
                alert("Error");
            })	// When Service call fails
        });

    };

    ClientCommunicationModule.prototype.getQuestions = function (onReceive) {
        alert("GetQuestion");
                    onReceive([
                        {
                            term: 'Name',
                            text: 'What is the plant name?',
                            options: []
                        },
                        {
                            term: 'ImageUrl',
                            text: 'Where is the plant image located?',
                            options: []
                        },
                        {
                            term: 'USState',
                            text: 'Where is the plant in the US?',
                            options: ["AK", "AL", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY", "AS", "DC", "FM", "GU", "MH", "MP", "PW", "PR", "VI"
                            ]
                        },
                        {
                            term: 'Type',
                            text: 'What type of plant is it?',
                            options: ["Forb/herb", "Graminoid", "Lichenous", "Nonvascular", "Shrub", "Subshrub", "Tree", "Vine"
                            ]
                        },
                        {
                            term: 'ColorFlower',
                            text: 'What color are the flowers?',
                            options: ["Blue", "Brown", "Green", "Orange", "Purple", "Red", "White", "Yellow"
                            ]
                        },
                        {
                            term: 'ColorFoliage',
                            text: 'What color are the leaves?',
                            options: ["Dark Green", "Green", "Grey-Green", "Red", "White-Gray", "Yellow-Green"
                            ]
                        },
                        {
                            term: 'ColorFruitSeed',
                            text: 'What color are the fruit or seeds?',
                            options: ["Black", "Blue", "Brown", "Green", "Orange", "Purple", "Red", "White", "Yellow"
                            ]
                        },
                        {
                            term: 'Shape',
                            text: 'What shape does the plant have?',
                            options: ["Climbing", "Columnar", "Conical", "Decumbent", "Erect", "Irregular", "Oval", "Prostrate", "Rounded", "Semi-Erect", "Vase"
                            ]
                        },
                        {
                            term: 'TextureFoliage',
                            text: 'What kind of texture do the leaves have?',
                            options: ["Fine", "Medium", "Coarse"
                            ]
                        },
                        {
                            term: 'Pattern',
                            text: 'What kind of pattern does the plant have?',
                            options: ["Dicot", "Fern", "Green Alga", "Gymnosperm", "Hornwort", "Horsetail", "Lichen", "Liverwort", "Lycopod", "Monocot", "Moss", "Quillwort", "Red Algae", "Whisk-fern"
                            ]
                        }]);
                
    }

    ClientCommunicationModule.prototype.insertData = function (data) {
        var out = JSON.stringify(data);
        var in_data = JSON.parse(out);
        admin_display.updateResultDisplay(in_data);
        //$.ajax({
        //    url: serverAddress, 		// Location of the service
        //    type: "PUT", 		//GET or POST or PUT or DELETE verb
        //    data: JSON.stringify(data), //Data sent to server //"This is a test",
        //    dataType: "text",
        //    success: function (in_data) {//On Successful service call
        //        //alert("input = " + in_data);
        //        var result = JSON.parse(in_data);
        //        //                alert("result=" + result);
        //        admin_display.updateResultDisplay(result);
        //    },
        //    error: (function () {
        //        alert("Error");
        //    })	// When Service call fails
        //});

    };

    return ClientCommunicationModule;
});


