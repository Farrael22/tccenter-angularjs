angular.module('tccenter.pesquisa').config(function ($routeProvider) {
    $routeProvider.when("/pesquisa", {
        templateUrl: "/Pesquisa/Pesquisa",
        controller: "PesquisaController"
    });
});