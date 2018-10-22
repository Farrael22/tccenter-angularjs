angular.module("tccenter.home").controller("HomeController", function ($scope, $rootScope, $timeout, $location, HomeService, ElementoAtivoFactory, EventosFactory, TccenterStorage, Util, AtalhosFactory) {

    if (!TccenterStorage.obterUsuario()) {
        $location.path("login");
    }

        var vm = this;

        
});