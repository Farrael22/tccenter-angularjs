angular.module("tccenter.modalSucesso").controller("modalSucessoController", function ($scope, $rootScope, $timeout, Util, ElementoAtivoFactory, AtalhosFactory) {
    var vm = this;

    vm.exibirModal = false;
    vm.sucesso = false;
    vm.mensagem = '';
    vm.botoes = [];

    vm.titulo;
    vm.icone;

    function configurar(config) {
        vm.botoes = config.botoes ? config.botoes : [];

        if (config.esc)
            vm.esc = config.esc;

        if (config.enter)
            vm.enter = config.enter;
    }

    var eventoFecharModalSucesso = $rootScope.$on('fecharModalSucesso', function (event) {
        vm.fecharModal();
    });

    var eventoAbrirModalSucesso = $rootScope.$on('abrirModalSucesso', function (event, mensagem, config) {
        if (typeof (config) !== "undefined" && config != null) configurar(config);
        else {
            vm.botoes = [];
            vm.enter = vm.fecharModal;
            vm.esc = vm.fecharModal;
        }
        configurarModalSucesso();
        vm.mensagem = mensagem;
        vm.abrirModal();
    });

    $scope.$on('$destroy', function (event, param) {
        eventoAbrirModalSucesso();
        eventoFecharModalSucesso();
    });

    function configurarModalSucesso() {
        vm.erro = true;
        vm.alerta = false;
        vm.titulo = 'SUCESSO';
        vm.icone = 'check_circle';
    }

    vm.fecharModal = function () {
        vm.exibirModal = false;
        Util.permitirScroll();
        AtalhosFactory.fecharModal($scope, 'Sucesso');
    }

    vm.abrirModal = function () {
        AtalhosFactory.iniciarAtalhosDaTela($scope, true);
        vm.exibirModal = true;
        Util.impedirScroll();
    }

    AtalhosFactory.criarTelaDeAtalhosGenericos($scope);
    
    vm.enter = vm.fecharModal;
    AtalhosFactory.criarAtalho($scope, ['enter'], function () {
        vm.enter();
    });

    vm.esc = vm.fecharModal;
    AtalhosFactory.criarAtalho($scope, ['esc'], function () {
        vm.esc();
    });

    AtalhosFactory.criarAtalho($scope, ['tab'], function () { });
});