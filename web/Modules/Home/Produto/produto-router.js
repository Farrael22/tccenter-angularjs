angular.module('balcao.produto').config(function ($routeProvider) {
    $routeProvider.when("/produto", {
        templateUrl: "/Home/Produto",
        controller: "produtoController"
    });
});