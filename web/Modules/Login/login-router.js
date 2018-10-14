angular.module('tccenter.login').config(function ($routeProvider) {
    $routeProvider.when("/login", {
        templateUrl: "/Login/Login",
        controller: "LoginController"
    });
});