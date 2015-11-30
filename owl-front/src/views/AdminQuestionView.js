define(function () {
    var submissionButton = document.createElement('button');
    var deletionButton = document.createElement('button');

    function AdminQuestionView() {
        var questionContainer = document.getElementById('system_inputs');
        var deletionContainer = document.getElementById('system_deletion');
        var widgets = [];
        var deletionWidgets = [];

        this.setModel = function (questions) {
            var questionTitle = document.createElement('h3');
            questionTitle.innerHTML = "New Plant Insertion";
            questionContainer.appendChild(questionTitle);
            questionContainer.appendChild(document.createElement('br'));
            this.setTextWidget("PlantId", questionContainer);
            this.setTextWidget("Name", questionContainer);
            this.setTextWidget("ImageURL", questionContainer);
            for (var i = 0; i < questions.length; i++) {
                if (questions[i].options.length > 1) {
                    var widget = new AdminSelectionQuestionWidget(questions[i]);
                    this.appendWidget(widget, questionContainer);
                }
            }

            submissionButton.setAttribute('class', "btn btn-default");
            submissionButton.innerHTML = "Submit Data";
            questionContainer.appendChild(submissionButton);

            var deletionTitle = document.createElement('h3');
            deletionTitle.innerHTML = "Plant Deletion";
            deletionContainer.appendChild(deletionTitle);
            deletionContainer.appendChild(document.createElement('br'));
            this.setTextWidget("PlantId", deletionContainer);

            deletionButton.setAttribute('class', "btn btn-default");
            deletionButton.innerHTML = "Submit Deletion";
            deletionContainer.appendChild(deletionButton);

        };

        this.setTextWidget = function(name, container) {
            var textWidget = new AdminTextQuestionWidget(name);
            this.appendWidget(textWidget, container);
        };

        this.appendWidget = function(widget, container) {
            container.appendChild(widget.asHTML());
            if (container == deletionContainer) {
                deletionWidgets.push(widget);
            } else {
                widgets.push(widget);
            }
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

        this.getDeletionSubject = function () {
            var subject = {};

            subject[deletionWidgets[0].getTerm()] = deletionWidgets[0].getAnswer();
            subject["UserName"] = "Admin";
            subject["Password"] = "Admin";
            return subject;
        };

        this.insertData = function (action) {
            submissionButton.addEventListener('click', action);
        };

        this.deleteData = function (action) {
            deletionButton.addEventListener('click', action);
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