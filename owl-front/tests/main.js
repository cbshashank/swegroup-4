require.config({
  baseUrl: '../src', 
  paths: {
      'jasmine': ['../tests/lib/jasmine'],
      'jasmine-html': ['../tests/lib/jasmine-html'],
      'jasmine-boot': ['../tests/lib/boot'],
      'specs': ['../tests/specs']
  },
  shim: {
    'jasmine-html': {
      deps : ['jasmine']
    },
    'jasmine-boot': {
      deps : ['jasmine', 'jasmine-html']
    }
  }
});

//Add the path to your specs bellow
require(['jasmine-boot'], function () {
    var specs = [
        'specs/OWL-front-spec',
        'specs/QuestionController-spec'
    ];
    require(specs, function(){
        window.onload();
    })
});
