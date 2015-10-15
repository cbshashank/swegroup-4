define(function(){
    function QuestionController(view, clientCommunicationModule){
        var testInput = {"Color":"Red","Shape":"Vine","Texture":"Rough"};

        view.setModel({
            text: 'Is it a plant?',
            options: ['yes', 'no']
        });

        view.onAnswer(function(){
            clientCommunicationModule.sendCharacteristics(testInput);
           //alert('send to');
        });
    }

    return QuestionController;
});