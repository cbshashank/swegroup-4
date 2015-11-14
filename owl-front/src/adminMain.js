require(['controllers/AdminQuestionController', 'views/AdminQuestionView', 'ClientCommunicationModule'], function (Controller, View, ClientCommunicationModule) {
    new Controller(new View(), new ClientCommunicationModule());
});