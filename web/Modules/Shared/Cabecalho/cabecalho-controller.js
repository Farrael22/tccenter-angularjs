var refresh_without_clear_local_storage = false;

angular.module("balcao.cabecalho").controller("cabecalhoController", function ($scope, $rootScope, $timeout, $filter, $location, CabecalhoService, EventosFactory, BalcaoStorage, config, $window) {

    var vm = this;
    var ipEstacao = GLOBAL_SERVER.IP_ESTACAO;
    var at = Atalho().init();
    vm.nofiticacaoDisponibilidade = [];
    
    var timeOutStatusFilialOnline = null;

    var eventoLimparCampos = $rootScope.$on('limparCampos', function (event) {
    });

    var eventoTesteInterceptorEmissorDeEvento = $rootScope.$on('testeInterceptorEmissorDeEvento', function (event) {
        console.log('Banco Indisponível');
    });

    

    $scope.$on('$destroy', function (event, param) {
        eventoLimparCampos();
        eventoBancoIndisponivel();
        eventoTesteInterceptorEmissorDeEvento();
    });

    vm.iniciarCabecalhoController = function () {
        vm.tela = $location.path();
        obterStatusFilialOnline();
    }

    vm.home = function () {
        $location.path("/");
    }

    window.onbeforeunload = function (e) {

        if (!refresh_without_clear_local_storage) {
            BalcaoStorage.removerOrcamento();
        }

        return undefined;
    };

    function obterStatusFilialOnline() {
        CabecalhoService.obterFilialOnline(ipEstacao, obterStatusFilialOnlineSucessoCallback, obterStatusFilialOnlineErroCallback);
    }

    vm.mostrarNotificacao = function () {
        vm.notificacao = true;

        if (!vm.isOnline && !vm.atualizandoBanco) {
            if (vm.notificacao && vm.nofiticacaoDisponibilidade.length == 0) {
                vm.nofiticacaoDisponibilidade.push({
                    texto: "O sistema está OFFLINE... CONTINUE NA VERSÃO OFFLINE.",
                    tempo: 5000,
                    tipo: "erro",
                    callback: vm.callbackFechar,
                    callbackFechar: vm.callbackFechar
                });
            }
        }
        else if (vm.isOnline && !vm.atualizandoBanco) {
            if (vm.notificacao && vm.nofiticacaoDisponibilidade.length == 0) {
                vm.nofiticacaoDisponibilidade.push({
                    texto: "O sistema está ONLINE novamente! CLIQUE AQUI!",
                    tempo: -1,
                    tipo: "sucesso",
                    callback: irParaSistemaOnline,
                    callbackFechar: vm.callbackFechar
                });
            }
        }
        else {
            if (vm.notificacao && vm.nofiticacaoDisponibilidade.length == 0) {
                vm.nofiticacaoDisponibilidade.push({
                    texto: "O sistema está sendo ATUALIZADO. Aguarde um momento...",
                    tempo: 5000,
                    tipo: "alerta",
                    callback: vm.callbackFechar,
                    callbackFechar: vm.callbackFechar
                });
            }
        }
    }

    vm.callbackFechar = function () {
        vm.notificacao = false;
        vm.nofiticacaoDisponibilidade = [];
    }

    var eventoBancoIndisponivel = $rootScope.$on('bancoIndisponivel', function (event, disponivel) {
        vm.atualizandoBanco = disponivel;
        vm.mostrarNotificacao();
    })

    function irParaSistemaOnline() {
        $window.location.href = config.urlBalcaoOnline;
    }

    function obterStatusFilialOnlineSucessoCallback(dados) {
        vm.isOnline = true;
        vm.mostrarNotificacao();
        if (timeOutStatusFilialOnline != null) {
            clearTimeout(timeOutStatusFilialOnline);
        }

        timeOutStatusFilialOnline = setTimeout(function () {
            obterStatusFilialOnline();
        }, 60000);
    }

    function obterStatusFilialOnlineErroCallback(mensagem) {
        vm.isOnline = false;     
        vm.callbackFechar();

        if (timeOutStatusFilialOnline != null) {
            clearTimeout(timeOutStatusFilialOnline);
        }

        timeOutStatusFilialOnline = setTimeout(function () {
            obterStatusFilialOnline();
        }, 60000);
    }

    at.criarAtalho(['F5'], function () {
        refresh_without_clear_local_storage = true;
    }, null, false);

    at.criarAtalho(['ctrl', 'F5'], function () {
        refresh_without_clear_local_storage = true;
    }, null, false);
});