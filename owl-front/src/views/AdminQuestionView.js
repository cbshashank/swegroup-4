define(function () {
    var submissionButton = document.createElement('button');

    function AdminQuestionView() {
        var questionContainer = document.getElementById('system_inputs');
        var widgets = [];

        this.setModel = function (questions) {
            this.setTextWidget("PlantId");
            this.setTextWidget("Name");
            this.setTextWidget("ImageURL");
            for (var i = 0; i < questions.length; i++) {
                if (questions[i].options.length > 1) {
                    var widget = new AdminSelectionQuestionWidget(questions[i]);
                    this.appendWidget(widget);
                }
            }

            submissionButton.setAttribute('class', "btn btn-default");
            submissionButton.innerHTML = "Submit Data";
            questionContainer.appendChild(submissionButton);
        };

        this.setTextWidget = function(name) {
            var textWidget = new AdminTextQuestionWidget(name);
            this.appendWidget(textWidget);
        };

        this.appendWidget = function(widget) {
            questionContainer.appendChild(widget.asHTML());
            widgets.push(widget);
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
            submissionButton.addEventListener('click', action);
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
            container.innerHTML = term + ": &nbsp";
            input = document.createElement('input');
            input.setAttribute('type', 'text');
            input.setAttribute('name', term);
            input.setAttribute('maxlength','100');
            input.onkeyup = this.limitText(input,100);
            input.onkeydown = this.limitText(input,100);
            container.appendChild(input);
            return container;
        };

        //this.questionLabelHTML = function() {
        //    return term + ": &nbsp";
        //};

        this.limitText = function(limitField, limitNum) {
            if (limitField.value.length > limitNum) {
                limitField.value = limitField.value.substring(0, limitNum);
            }
        }
    }

    return AdminQuestionView;
});