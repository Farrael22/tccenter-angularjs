angular.module("tccenter.modalAlertaErro").controller("modalAlertaErroController", function ($scope, $rootScope, $timeout, Util, ElementoAtivoFactory, AtalhosFactory) {
    var vm = this;

    vm.exibirModal = false;
    vm.erro = false;
    vm.alerta = false;
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

    var eventoFecharModalAlertaErro = $rootScope.$on('fecharModalAlertaErro', function (event) {
        vm.fecharModal();
    });

    var eventoAbrirModalErro = $rootScope.$on('abrirModalErro', function (event, mensagem, config) {
        if (typeof (config) !== "undefined" && config != null) configurar(config);
        else {
            vm.botoes = [];
            vm.enter = vm.fecharModal;
            vm.esc = vm.fecharModal;
        }
        configurarModalErro();
        vm.mensagem = mensagem;
        vm.abrirModal();
    });

    var eventoAbrirModalAlerta = $rootScope.$on('abrirModalAlerta', function (event, mensagem, config) {
        if (typeof (config) !== "undefined" && config != null) configurar(config);
        else {
            vm.botoes = [];
            vm.enter = vm.fecharModal;
            vm.esc = vm.fecharModal;
        } 
        configurarModalAlerta();
        vm.mensagem = mensagem;
        vm.abrirModal();
    });

    $scope.$on('$destroy', function (event, param) {
        eventoAbrirModalAlerta();
        eventoAbrirModalErro();
        eventoFecharModalAlertaErro();
    });

    function configurarModalErro() {
        vm.erro = true;
        vm.alerta = false;
        vm.titulo = 'MENSAGEM DE ERRO';
        vm.icone = 'cancel';
    }

    function configurarModalAlerta() {
        vm.erro = false;
        vm.alerta = true;
        vm.titulo = 'ATENÇÃO';
        vm.icone = 'report';
    }

    vm.fecharModal = function () {
        vm.exibirModal = false;
        Util.permitirScroll();
        AtalhosFactory.fecharModal($scope, 'AlertaErro');
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
        $scope.$apply();
    });

    vm.esc = vm.fecharModal;
    AtalhosFactory.criarAtalho($scope, ['esc'], function () {
        vm.esc();
        $scope.$apply();
    });

    AtalhosFactory.criarAtalho($scope, ['tab'], function () {});


});