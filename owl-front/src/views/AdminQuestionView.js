define(function () {
    var answerButton = document.createElement('button');

    function AdminQuestionView() {
        var questionContainer = document.getElementById('system_inputs');
        var widgets = [];

        this.setModel = function (questions) {
            for (var i = 0; i < questions.length; i++) {
                if (questions[i].options.length > 0) {
                    var widget = new AdminSelectionQuestionWidget(questions[i]);
                    questionContainer.appendChild(widget.asHTML());
                    widgets.push(widget);
                } else {
                    var textWidget = new AdminTextQuestionWidget(questions[i].term);
                    questionContainer.appendChild(textWidget.asHTML());
                    widgets.push(textWidget);
                }
            }

            answerButton.setAttribute('class', "btn btn-default");
            answerButton.innerHTML = "Submit Data";
            //var buttonContainer = document.createElement('p');
            //buttonContainer.appendChild(answerButton);
            //questionContainer.appendChild(buttonContainer);
            questionContainer.appendChild(answerButton);
        };

        this.getAnswers = function () {
            var answers = {};
            for(var i = 0; i < widgets.length; i++){
                var answer = widgets[i].getAnswer();
                if(answer) {
                    answers[widgets[i].getTerm()] = answer;
                }
            }
            answers["UserName"] = "Admin";
            answers["Password"] = "Admin";
            return answers;
        };

        this.insertData = function (action) {
            answerButton.addEventListener('click', action);
        };
    }

    /***
     * Widget for a single question with a defined set of possible options. Contains question text and a set of options for each item.
     * When an option is selected the answer value is updated.
     * @param question model
     */
    function AdminSelectionQuestionWidget(question) {

        var questionOptions;

        this.getAnswer = function () {
            return questionOptions.value;
        };

        this.getTerm = function(){
            return question.term;
        };

        this.asHTML = function () {
            var container = document.createElement('p');
            container.innerHTML = questionLabelHTML();
            questionOptions = document.createElement('select');
            var init_option = optionHTML("");
            questionOptions.appendChild(init_option);
            for (var i = 0; i < question.options.length; i++) {
                var option = optionHTML(question.options[i]);
                questionOptions.appendChild(option);
            }
            container.appendChild(questionOptions);
            return container;
        };

        function questionLabelHTML() {
            return question.term + ": &nbsp";
        }

        function optionHTML(option) {
            var container = document.createElement('option');
            container.value = option;
            container.innerHTML = option;
            return container;
        }
    }

    /***
     * Widget for a text-based question interface.
     * @param question model
     */
    function AdminTextQuestionWidget(term) {

        var input;

        this.getAnswer = function () {
            return input.value;
        };

        this.getTerm = function(){
            return term;
        };

        this.asHTML = function () {
            var container = document.createElement('p');
            container.innerHTML = questionLabelHTML();
            input = document.createElement('input');
            input.setAttribute('type', 'text');
            input.setAttribute('name', term);
            input.setAttribute('maxlength','100');
            input.onkeyup = limitText(input,100);
            input.onkeydown = limitText(input,100);
            container.appendChild(input);
            return container;
        };

        function questionLabelHTML() {
            return term + ": &nbsp";
        }

        function limitText(limitField, limitNum) {
            if (limitField.value.length > limitNum) {
                limitField.value = limitField.value.substring(0, limitNum);
            }
        }
    }

    return AdminQuestionView;
});