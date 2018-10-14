angular.module('tccenter.cadastroCliente').config(function ($routeProvider) {
    $routeProvider.when("/cadastroCliente", {
        templateUrl: "/CadastroCliente/CadastroCliente",
        controller: "CadastroClienteController"
    });
});