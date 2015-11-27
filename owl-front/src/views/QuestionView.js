define(function () {

    /**
     * View for a  group of questions. Constructs the answer object from all the user answers
     * @constructor
     */
    function QuestionView() {
        var questionContainer = document.getElementById('system_inputs');
        questionContainer.className = 'tab-content';

        var resetButton = document.getElementById('resetButton');
        var widgets = [];
        var that = this;
        var lastQuestions;

        this.setModel = function (questions) {
            lastQuestions = questions;
            questionContainer.innerHTML = "";
            for (var i = 0; i < questions.length; i++) {
                var widget = widgetFactory(questions[i], that.onAnswer.bind(that));
                questionContainer.appendChild(widget.asHTML());
                widgets.push(widget);
            }
        };

        this.getAnswers = function () {
            var answers = {};
            for (var i = 0; i < widgets.length; i++) {
                var answer = widgets[i].getAnswer();
                if (answer)
                    answers[widgets[i].getTerm()] = answer;
            }
            return answers;
        };

        resetButton.addEventListener('click', function () {
            that.setModel(lastQuestions);
        });
    }

    /**
     * This function selects the proper OptionWidget for a question
     * (ex: radioButtons, listSelects, imageButtons, ...)
     * @param question
     */
    function widgetFactory(question, onAnswer) {
        switch (question.term) {
            case 'USState':
                return new SelectQuestionWidget(question, onAnswer);
            default:
                return new RadioQuestionWidget(question, onAnswer);
        }
    }

    /***
     * Widget for a single question. Should be extended for supporting different option widgets.
     * @param question
     * @constructor
     */
    function AbstractQuestionWidget(question, onAnswer) {

        var answer;

        this.getAnswer = function () {
            return answer;
        };

        this.setAnswer = function (value) {
            answer = value;
            onAnswer();
        };

        this.getTerm = function () {
            return question.term;
        };

        this.asHTML = function () {
            var tabpanel = document.createElement('div');
            tabpanel.role = "tabpanel";
            tabpanel.className = "tab-pane";
            tabpanel.id = this.getTerm();

            tabpanel.appendChild(questionLabelHTML());
            tabpanel.appendChild(this.optionsHTML(question.options, question.urls));
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
    function RadioQuestionWidget(questions, onAnswer) {

        AbstractQuestionWidget.call(this, questions, onAnswer);

        var that = this;

        this.optionsHTML = function (options, urls) {
            var container = document.createElement('div');
            //container.className = "row";
            
            var form = document.createElement('form');
            form.role = "form";
            container.appendChild(form);

            for (var i = 0; i < options.length; i++) {
                var radio = optionRadioHTML(options[i], urls[i]);
                form.appendChild(radio);
            }
            
            return container;
        };

        /* Create options as
    <form role="form">
        <label class="radio-inline">
              <input type="radio" name=this.getTerm value=option />
              <div>
                <img src="./pictures/type/shrub.jpg" alt="..." style="width:150px;height:150px"></img>
                <p>...</p>
              </div>
        </label>
    </form>
        */

        function optionRadioHTML(option, url) {
            var label = document.createElement('label');
            label.className = "radio-inline";

            var input = document.createElement('input');
            input.type = "radio";
            input.name = this.getTerm;
            input.value = option;
            
            var thumbnail = document.createElement('div');

            var img = document.createElement('img');
            img.src = url;
            img.alt = option;
            img.width=  160;
	    img.height=160;

            var caption = document.createElement('p');
            caption.innerHTML = option;

            thumbnail.onclick = that.setAnswer.bind(that, option);

            label.appendChild(input);
            label.appendChild(thumbnail);
            thumbnail.appendChild(img);
            thumbnail.appendChild(caption);

            return label;
            /*
            var container = document.createElement('div');
            container.className = "col-md-2";

            var thumbnail = document.createElement('a');
            thumbnail.className = "thumbnail";

            // populate the options
            thumbnail.onclick = function () {
                RadioQuestionWidget.setMyAnswer(option);
            };

            var thumbnailImg = document.createElement('img');
            thumbnailImg.src = url;
            thumbnailImg.alt = option;

            var thumbnailCaption = document.createElement('div');
            thumbnailCaption.className = "caption";

            var thumbnailCaptionText = document.createElement('p');
            thumbnailCaptionText.innerHTML = option;

            container.appendChild(thumbnail);
            thumbnail.appendChild(thumbnailImg);
            thumbnail.appendChild(thumbnailCaption);
            thumbnailCaption.appendChild(thumbnailCaptionText);

            return container; */
        }
    }

    /**
     * Displays options as a select list
     * @constructor
     */
    function SelectQuestionWidget(questions, onAnswer) {

        AbstractQuestionWidget.call(this, questions, onAnswer);

        this.optionsHTML = function (options, urls) {
            var select = document.createElement('select');
            select.id = "mySelect";
            select.className = 'form-control';
            select.appendChild(document.createElement('option'));
            for (var i = 0; i < options.length; i++) {
                var option = document.createElement('option');
                option.value = options[i];
                option.innerHTML = options[i];
                select.appendChild(option);
            }

            var that = this;
            select.onchange = function(){
                var selectedOption = select.options[select.selectedIndex].value;
                that.setAnswer(selectedOption);
            };

            return select;
        };
    }

    return QuestionView;
});