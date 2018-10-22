angular.module("tccenter.cabecalho").controller("cabecalhoController", function ($scope, $rootScope, $timeout, $filter, $location, CabecalhoService, EventosFactory, TccenterStorage, config, $window) {

    var vm = this;
    
    vm.iniciarCabecalhoController = function () {
        verificarUsuarioLogado();
    };

    $scope.$on('UsuarioLogado', function () {
        verificarUsuarioLogado();
    });

    function verificarUsuarioLogado() {
        if (TccenterStorage.obterUsuario()) {
            vm.usuarioLogado = true;
        }
        else {
            vm.usuarioLogado = false;
        }
    }

    vm.redirecionarTelaHome = function () {
        $location.path("home");
    };

    vm.redirecionarTelaLogin = function () {
        $location.path("login");
    };

    vm.redirecionarTelaCadastro = function () {
        $location.path("cadastroCliente");
    };

    vm.realizarLogout = function () {
        TccenterStorage.removerUsuario();
        verificarUsuarioLogado();
        $location.path("login");
    };
   
});