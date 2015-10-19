define(function () {
    var us_state = {
        id: "us_state",
        text: 'Where is the plant in the US?',
        options: ["AK", "AL", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY", "AS", "DC", "FM", "GU", "MH", "MP", "PW", "PR", "VI"]
    };
    var type = {
        id: "type",
        text: 'What type of plant is it?',
        options: ["Forb/herb", "Graminoid", "Lichenous", "Nonvascular", "Shrub", "Subshrub", "Tree", "Vine"
        ]
    };
    var color_flower = {
        id: "color_flower",
        text: 'What color are the flowers?',
        options: ["Blue", "Brown", "Green", "Orange", "Purple", "Red", "White", "Yellow"
        ]
    };
    var color_foliage = {
        id: "color_foliage",
        text: 'What color are the leaves?',
        options: ["Dark Green", "Green", "Grey-Green", "Red", "White-Gray", "Yellow-Green"
        ]
    };
    var color_fruit_seed = {
        id: "color_fruit_seed",
        text: 'What color are the fruit or seeds?',
        options: ["Black", "Blue", "Brown", "Green", "Orange", "Purple", "Red", "White", "Yellow"
        ]
    };
    var shape = {
        id: "shape",
        text: 'What shape does the plant have?',
        options: ["Climbing", "Columnar", "Conical", "Decsumbent", "Erect", "Irregular", "Oval", "Prostrate", "Rounded", "Semi-Erect", "Vase"
        ]
    };
    var texture_foliage = {
        id: "texture_foliage",
        text: 'What kind of texture do the leaves have?',
        options: ["Fine", "Medium", "Coarse"
        ]
    };
    var pattern = {
        id: "pattern",
        text: 'What kind of pattern does the plant have?',
        options: ["Dicot", "Fern", "Green Alga", "Gymnosperm", "Hornwort", "Horsetail", "Lichen", "Liverwort", "Lycopod", "Monocot", "Moss", "Quillwort", "Red Algae", "Whisk-fern"
        ]
    };
    var questions = [us_state, type,
        color_flower, color_foliage, color_fruit_seed,
        shape, texture_foliage, pattern];

    function QuestionController(view, clientCommunicationModule) {
        view.setModel(questions);
        view.onAnswer(function () {
            alert(JSON.stringify(view.getAnswers()));
            clientCommunicationModule.sendCharacteristics(view.getAnswers());
        });
    }

    return QuestionController;
});

