define(function () {

    /**
    * View for a  group of questions. Constructs the answer object from all the user answers
    * @constructor
    */
    function QuestionView() {
        var questionContainer = document.getElementById('system_inputs');
        questionContainer.className = 'tab-content';

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
            for (var i = 0; i < widgets.length; i++) {
                var answer = widgets[i].getAnswer();
                if (answer) {
                    answers[widgets[i].getTerm()] = answer;
                }
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
    function widgetFactory(question) {
        switch (question.term) {
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
    function AbstractQuestionWidget(question) {

        var answer;

        this.getAnswer = function () {
            return answer;
        };

        this.setAnswer = function (value) {
            answer = value;
        }

        this.getTerm = function () {
            return question.term;
        };

        this.asHTML = function () {
            var tabpanel = document.createElement('div');
            tabpanel.role = "tabpanel";
            tabpanel.className = "tab-pane";
            tabpanel.id = this.getTerm();

            tabpanel.appendChild(questionLabelHTML());
            tabpanel.appendChild(this.optionsHTML(question.options));
            return tabpanel;
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
    function RadioQuestionWidget(questions) {
        AbstractQuestionWidget.call(this, questions);


        this.setMyAnswer = function (value) {
            this.setAnswer(value);
        }

        this.optionsHTML = function (options) {
            var container = document.createElement('div');
            container.className = "row";

            for (var i = 0; i < options.length; i++) {
                var radio = optionRadioHTML(options[i], this);
                container.appendChild(radio);
            }
            return container;
        };

        /* Create options as
        // CODE for enclosing thumbnails in ROW in function RadioQuestionWidget(questions)
        <div class="row">
        // Code for creating THUMBNAILS is in function optionRadioHTML(option)
        <div class="col-sm-6 col-sm-1">
        <div class="thumbnail">
        <img src="..." alt="...">
        <div class="caption">
        <p>...</p>
        </div>
        </div>
        </div>
        </div>
        */

        function optionRadioHTML(option, RadioQuestionWidget) {
            var container = document.createElement('div');
            container.className = "col-md-2";

            var thumbnail = document.createElement('a');
            thumbnail.className = "thumbnail";

            // populate the options
            thumbnail.onclick = function () {
                RadioQuestionWidget.setMyAnswer(option);
            };

            var thumbnailImg = document.createElement('img');
            thumbnailImg.src = "...";
            thumbnailImg.alt = option;

            var thumbnailCaption = document.createElement('div');
            thumbnailCaption.className = "caption";

            var thumbnailCaptionText = document.createElement('p');
            thumbnailCaptionText.innerHTML = option;

            container.appendChild(thumbnail);
            thumbnail.appendChild(thumbnailImg);
            thumbnail.appendChild(thumbnailCaption);
            thumbnailCaption.appendChild(thumbnailCaptionText);

            return container;
        }
    }

    /**
    * Displays options as a select list
    * @constructor
    */
    function SelectQuestionWidget(questions) {
        AbstractQuestionWidget.call(this, questions);

        

        this.optionsHTML = function (options) {
            var select = document.createElement('select');
            select.className = 'form-control';

            select.appendChild(document.createElement('option'));
            for (var i = 0; i < options.length; i++) {
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