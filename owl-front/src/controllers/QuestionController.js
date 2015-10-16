define(function(){
    function QuestionController(view, clientCommunicationModule){
        //var testInput = {"Color":"Red","Shape":"Vine","Texture":"Rough"};
        var testInput2 = {"us_state":"MA", "type":"vine", "shape":"Rounded"};
    view.setModel({
            text: 'Is it a plant?',
            options: ['yes', 'no']
        });

        view.onAnswer(function(){
            clientCommunicationModule.sendCharacteristics(testInput2);
           //alert('send to');
        });
    }

    return QuestionController;
});