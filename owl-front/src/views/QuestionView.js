define(function(){
    function QuestionView(){
        var questionText = document.getElementById('questionText');
        var answerButton = document.getElementById('answerButton');

        function questnView(question){
            var container = document.createElement('div');
            var paragraph = document.createElement('p');
            paragraph.innerHTML = question;
            container.appendChild(paragraph);
            return container;
        }

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
            for(var i = 0; i < model.questions.length; i++){
                // state question
                var questnText = questnView(model.questions[i].text);
                questionText.appendChild(questnText);

                // present option choices
                for(var j = 0; j < model.questions[i].options.length; j++){
                    var widget = optionView(model.questions[i].options[j]);
                    questionText.appendChild(widget);
                }
            }
        };

        this.onAnswer = function(action){
            answerButton.addEventListener('click', action);
        };
    }

    return QuestionView;
});