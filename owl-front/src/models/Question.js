define(function(){
    function Question(text, options){
        if(arguments.length == 0){
            var text = '';
            var options = [];
        }

        this.getText = function(){
            return text;
        };

        this.getOptions = function(){
            return options;
        };
    }

    return Question;
});