angular.module('tccenter.publicacaoCompleta').config(function ($routeProvider) {
    $routeProvider.when("/publicacaoCompelta", {
        templateUrl: "/PublicacaoCompleta/PublicacaoCompleta",
        controller: "PublicacaoCompletaController"
    });
});