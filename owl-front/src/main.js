require([
    'controllers/QuestionController',
    'views/QuestionView',
    'MockClientCommunicationModule'], function (Controller, View, ClientCommunicationModule) {
    new Controller(new View(), new ClientCommunicationModule());
});