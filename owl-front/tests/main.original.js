require.config({
  baseUrl: '../src', 
  paths: {
      'jasmine': ['../tests/lib/jasmine'],
      'jasmine-html': ['../tests/lib/jasmine-html'],
      'jasmine-boot': ['../tests/lib/boot'],
      'core/*': ['../src/core'],
      'engine': ['../src/engine']
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
  require([
      '../tests/specs/example-spec'
  ], function(){
    window.onload();
  })
});
