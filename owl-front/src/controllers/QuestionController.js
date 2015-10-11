define(function(){
    function QuestionController(view){
        view.setModel({
            text: 'Is it a plant?',
            options: ['yes', 'no']
        });

        view.onAnswer(function(){
           alert('send to');
        });
    }

    return QuestionController;
});