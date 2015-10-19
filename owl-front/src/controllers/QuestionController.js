define(function(){
    var us_state = {term: 'USState',
                text: 'Where is the plant in the US?',
                options: ["AK", "AL", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY", "AS", "DC", "FM", "GU", "MH", "MP", "PW", "PR", "VI"
                ]};
    var type = {term: 'Type',
                text: 'What type of plant is it?',
                options: ["Forb/herb", "Graminoid", "Lichenous", "Nonvascular", "Shrub", "Subshrub", "Tree", "Vine"
                ]};
    var color_flower = {term: 'ColorFlower',
                text: 'What color are the flowers?',
                options: ["Blue", "Brown", "Green", "Orange", "Purple", "Red", "White", "Yellow"
                ]};
    var color_foliage = {term: 'ColorFoliage',
                text: 'What color are the leaves?',
                options: ["Dark Green", "Green", "Grey-Green", "Red", "White-Gray", "Yellow-Green"
                ]};
    var color_fruit_seed = {term: 'ColorFruitSeed',
                text: 'What color are the fruit or seeds?',
                options: ["Black", "Blue", "Brown", "Green", "Orange", "Purple", "Red", "White", "Yellow"
                ]};
    var shape = {term: 'Shape',
                text: 'What shape does the plant have?',
                options: ["Climbing", "Columnar", "Conical", "Decumbent", "Erect", "Irregular", "Oval", "Prostrate", "Rounded", "Semi-Erect", "Vase"
                ]};
    var texture_foliage = {term: 'TextureFoliage',
                text: 'What kind of texture do the leaves have?',
                options: ["Fine", "Medium", "Coarse"
                ]};
    var pattern = {term: 'Pattern',
                text: 'What kind of pattern does the plant have?',
                options: ["Dicot", "Fern", "Green Alga", "Gymnosperm", "Hornwort", "Horsetail", "Lichen", "Liverwort", "Lycopod", "Monocot", "Moss", "Quillwort", "Red Algae", "Whisk-fern"
                ]};
    var questions = [us_state, type, 
                    color_flower, color_foliage, color_fruit_seed, 
                    shape, texture_foliage, pattern];

    function QuestionController(view, clientCommunicationModule){
        //var testInput = {"Color":"Red","Shape":"Vine","Texture":"Rough"};
        var testInput2 = {"ColorFlower":"Yellow", "TextureFoliage":"Fine", "Shape":"Rounded"};

        view.setModel({
            questions
            // text: 'Is it a plant?',
            // options: ['yes', 'no']
        });

        view.onAnswer(function(){
            clientCommunicationModule.sendCharacteristics(testInput2);
           //alert('send to');
        });
    }

    return QuestionController;
});

