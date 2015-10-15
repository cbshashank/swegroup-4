require(['controllers/QuestionController', 'views/QuestionView', 'ClientCommunicationModule'], function (Controller, View, ClientCommunicationModule) {
    new Controller(new View(), new ClientCommunicationModule());
});