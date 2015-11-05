define(function () {

    /**
     * View for a  group of questions. Constructs the answer object from all the user answers
     * @constructor
     */
    function QuestionView() {
        var questionContainer = document.getElementById('system_inputs');
        var answerButton = document.getElementById('answerButton');
        var widgets = [];

        this.setModel = function (questions) {
            for (var i = 0; i < questions.length; i++) {
                var widget = widgetFactory(questions[i]);
                questionContainer.appendChild(widget.asHTML());
                widgets.push(widget);
            }
        };

        this.getAnswers = function () {
            var answers = {};
            for(var i = 0; i < widgets.length; i++){
                var answer = widgets[i].getAnswer();
                if(answer)
                    answers[widgets[i].getTerm()] = answer;
            }
            return answers;
        };

        this.onAnswer = function (action) {
            answerButton.addEventListener('click', action);
        };
    }

    /**
     * This function selects the proper OptionWidget for a question
     * (ex: radioButtons, listSelects, imageButtons, ...)
     * @param question
     */
    function widgetFactory(question){
        switch (question.term){
            case 'USState':
                return new SelectQuestionWidget(question);
            default:
                return new RadioQuestionWidget(question);
        }
    }

    /***
     * Widget for a single question. Should be extended for supporting different option widgets.
     * @param question
     * @constructor
     */
    function AbstractQuestionWidget(question){

        var answer;

        this.getAnswer = function(){
            return answer;
        };

        function setAnswer(value){
            answer = value;
        }

        this.getTerm = function(){
            return question.term;
        };

        this.asHTML = function(){
            var container = document.createElement('form');
            container.appendChild(questionLabelHTML());
            container.appendChild(this.optionsHTML(question.options));
            return container;
        };

        function questionLabelHTML() {
            var label = document.createElement('h3');
            label.innerHTML = question.text;
            return label;
        }
    }

    /**
     * Display options as a group of radio buttons
     * @constructor
     */
    function RadioQuestionWidget(questions){
        AbstractQuestionWidget.call(this, questions);

        this.optionsHTML = function(options){
            var container = document.createElement('form');
            for (var i = 0; i < options.length; i++) {
                var radio = optionRadioHTML(options[i]);
                container.appendChild(radio);
            }
            return container;
        };

        function optionRadioHTML(option) {
            var container = document.createElement('div');
            var radio = document.createElement('input');
            var label = document.createElement('label');
            radio.type = 'radio';
            radio.name = 'option';
            radio.onclick = function () {
                answer = option;
            };
            label.innerHTML = option;
            container.appendChild(radio);
            container.appendChild(label);
            return container;
        }
    }

    /**
     * Displays options as a select list
     * @constructor
     */
    function SelectQuestionWidget(questions){
        AbstractQuestionWidget.call(this, questions);

        this.optionsHTML = function(options){
            var select = document.createElement('select');
            select.appendChild(document.createElement('option'));
            for(var i = 0; i < options.length; i++){
                var option = document.createElement('option');
                option.value = options[i];
                option.innerHTML = options[i];
                select.appendChild(option);
            }
            return select;
        };
    }

    return QuestionView;
});