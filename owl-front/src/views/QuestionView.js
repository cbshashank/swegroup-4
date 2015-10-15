define(function(){
    function QuestionView(){
        var questionText = document.getElementById('questionText');
        var optionsForm = document.getElementById('optionsForm');
        var answerButton = document.getElementById('answerButton');

        function optionView(optionText){
            var container = document.createElement('div');
            var radio = document.createElement('input');
            var label = document.createElement('label');
            radio.type = 'radio';
            radio.name = 'option';
            label.innerHTML = optionText;
            container.appendChild(radio);
            container.appendChild(label);
            return container;
        }

        this.setModel = function(model){
            questionText.innerHTML = model.text;
            for(var i = 0; i < model.options.length; i++){
                var widget = optionView(model.options[i]);
                optionsForm.appendChild(widget);
            }
        };

        this.onAnswer = function(action){
            answerButton.addEventListener('click', action);
        };
    }

    return QuestionView;
});