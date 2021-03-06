/**
 * Created by Scott on 10/7/2015.
 */
define(['jquery', 'views/AdminResultDisplay'], function ($, AdminResultDisplay) {

    function ClientCommunicationModule() {
    }

    var admin_display = new AdminResultDisplay();
    var serverAddress = "https://localhost:32297";

    ClientCommunicationModule.prototype.onResultReceived = function (result) {
    };

    ClientCommunicationModule.prototype.sendAnswers = function (answers) {
        console.log(answers);
        this.onResultReceived([{
            Name: "A plant",
            ImageURL: "http://companionplants.com/images/small-plant2.jpg",
            GoogleURL: "https://en.wikipedia.org/wiki/Carnivorous_plant",
            GoogleImagesURL: "https://en.wikipedia.org/wiki/Carnivorous_plant"
        }, {
            Name: "Other plant",
            ImageURL: "http://companionplants.com/images/small-plant2.jpg",
            GoogleURL: "https://en.wikipedia.org/wiki/Carnivorous_plant",
            GoogleImagesURL: "https://en.wikipedia.org/wiki/Carnivorous_plant"
        }]);
    };

    ClientCommunicationModule.prototype.getQuestions = function (onReceive) {
        onReceive([
            {
                term: 'PlantId',
                text: 'What is the plant ID?',
                options: []
            },
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
                options: ["AK", "AL", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY", "AS", "DC", "FM", "GU", "MH", "MP", "PW", "PR", "VI"]
            },
            {
                term: 'Type',
                text: 'What type of plant is it?',
                options: ["Forb/herb", "Graminoid", "Lichenous", "Nonvascular", "Shrub", "Subshrub", "Tree", "Vine"],
                urls: ["http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg"]
            },
            {
                term: 'ColorFlower',
                text: 'What color are the flowers?',
                options: ["Blue", "Brown", "Green", "Orange", "Purple", "Red", "White", "Yellow"],
                urls: ["http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg"]
            },
            {
                term: 'ColorFoliage',
                text: 'What color are the leaves?',
                options: ["Dark Green", "Green", "Grey-Green", "Red", "White-Gray", "Yellow-Green"],
                urls: ["http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg"]
            },
            {
                term: 'ColorFruitSeed',
                text: 'What color are the fruit or seeds?',
                options: ["Black", "Blue", "Brown", "Green", "Orange", "Purple", "Red", "White", "Yellow"],
                urls: ["http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg"]
            },
            {
                term: 'Shape',
                text: 'What shape does the plant have?',
                options: ["Climbing", "Columnar", "Conical", "Decumbent", "Erect", "Irregular", "Oval", "Prostrate", "Rounded", "Semi-Erect", "Vase"],
                urls: ["http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg"]

            },
            {
                term: 'TextureFoliage',
                text: 'What kind of texture do the leaves have?',
                options: ["Fine", "Medium", "Coarse"],
                urls: ["http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg"]
            },
            {
                term: 'Pattern',
                text: 'What kind of pattern does the plant have?',
                options: ["Dicot", "Fern", "Green Alga", "Gymnosperm", "Hornwort", "Horsetail", "Lichen", "Liverwort", "Lycopod", "Monocot", "Moss", "Quillwort", "Red Algae", "Whisk-fern"],
                urls: ["http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg", "http://companionplants.com/images/small-plant2.jpg"]
            }]);
    };

    ClientCommunicationModule.prototype.insertData = function (data) {
        var out = JSON.stringify(data);
        var in_data = JSON.parse(out);
        admin_display.updateResultDisplay(in_data);
    };

    return ClientCommunicationModule;
});


