/**
 * Created by Scott on 10/7/2015.
 */
define(function () {
    // Need to tie this (or something like this) to whatever interface widgets are used. Actual structure may vary from this extremely simple placeholder
    function SystemInputs() {
    }

    SystemInputs.prototype.getInput = function (input_id) {
        var resultMap = {
            'Color': 'Color:Red',
            'Location': 'Location:Western_US',
            'Setting': 'Setting:Rural',
            'Shape': 'Shape:Vine',
            'Texture': 'Texture:Rough',
            'Smell': 'Smell:Sweet',
            'Pattern': 'Pattern:Clusters'
        };
        return resultMap[input_id] || '';
    };

    return SystemInputs;
});