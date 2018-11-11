angular.module('tccenter.perfil').config(function ($routeProvider) {
    $routeProvider.when("/perfil", {
        templateUrl: "/Perfil/Perfil",
        controller: "PerfilController"
    });
});