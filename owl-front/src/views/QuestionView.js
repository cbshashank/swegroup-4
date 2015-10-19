define(function () {

    function QuestionView() {
        var questionContainer = document.getElementById('system_inputs');
        var answerButton = document.getElementById('answerButton');
        var widgets = [];

        this.setModel = function (questions) {
            for (var i = 0; i < questions.length; i++) {
                var widget = new QuestionWidget(questions[i]);
                questionContainer.appendChild(widget.asHTML());
                widgets.push(widget);
            }
        };

        this.getAnswers = function () {
            var answers = [];
            for(var i = 0; i < widgets.length; i++){
                var answer = widgets[i].getAnswer();
                if(Object.keys(answer).length > 0)
                    answers.push(answer);
            }
            return answers;
        };

        this.onAnswer = function (action) {
            answerButton.addEventListener('click', action);
        };
    }

    /***
     * Widget for a single question. Contains a question text and a set of radio buttons for each option.
     * When a radio button is selected the answer value is updated.
     * @param question model
     */
    function QuestionWidget(question) {

        var answer = {};

        this.getAnswer = function () {
            return answer;
        };

        this.asHTML = function () {
            var container = document.createElement('form');
            container.appendChild(questionLabelHTML());
            for (var i = 0; i < question.options.length; i++) {
                var radio = optionRadioHTML(question.options[i]);
                container.appendChild(radio);
            }
            return container;
        };

        function questionLabelHTML() {
            var label = document.createElement('p');
            label.innerHTML = question.text;
            return label;
        }

        function optionRadioHTML(option) {
            var container = document.createElement('div');
            var radio = document.createElement('input');
            var label = document.createElement('label');
            radio.type = 'radio';
            radio.name = 'option';
            radio.onclick = function () {
                answer[question.term] = option;
            };
            label.innerHTML = option;
            container.appendChild(radio);
            container.appendChild(label);
            return container;
        }
    }

    return QuestionView;
});