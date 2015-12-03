require(['controllers/LoginController', 'views/LoginView', 'ClientCommunicationModule'], function (Controller, View, ClientCommunicationModule) {
    new Controller(new View(), new ClientCommunicationModule());
});