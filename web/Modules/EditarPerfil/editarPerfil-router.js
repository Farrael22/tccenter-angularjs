angular.module('tccenter.editarPerfil').config(function ($routeProvider) {
    $routeProvider.when("/editarPerfil", {
        templateUrl: "/EditarPerfil/EditarPerfil",
        controller: "EditarPerfilController"
    });
});