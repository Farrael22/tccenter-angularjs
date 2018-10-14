angular.module("tccenter.cabecalho").controller("cabecalhoController", function ($scope, $rootScope, $timeout, $filter, $location, CabecalhoService, EventosFactory, BalcaoStorage, config, $window) {

    var vm = this;
    
    vm.iniciarCabecalhoController = function () {
        vm.usuarioLogado = $rootScope.Usuario ? true : false;
    };

    vm.redirecionarTelaHome = function () {

    };

    vm.redirecionarTelaLogin = function () {
        $location.path("login");
    };

    vm.redirecionarTelaCadastro = function () {
        $location.path("cadastroCliente");
    };
});