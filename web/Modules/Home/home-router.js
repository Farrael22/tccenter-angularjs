angular.module('tccenter.home').config(function ($routeProvider) {
    $routeProvider.when("/home", {
        templateUrl: "/Home/Home",
        controller: "HomeController"
    });
});