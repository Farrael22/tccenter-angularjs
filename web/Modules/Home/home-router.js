angular.module('balcao.home').config(function ($routeProvider) {
    $routeProvider.when("/home", {
        templateUrl: "/Home/Home",
        controller: "homeController"
    });
});