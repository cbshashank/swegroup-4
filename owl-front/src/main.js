require([
        'ClientCommunicationModule',//'MockClientCommunicationModule',//
        'controllers/QuestionController', 'views/QuestionView',
        'controllers/ResultController', 'views/ResultView'
    ],
    function (ClientCommunicationModule,
              QuestionController, QuestionView,
              ResultController, ResultView) {
        var comm = new ClientCommunicationModule();
        new QuestionController(new QuestionView(), comm);
        new ResultController(new ResultView(), comm);
    });