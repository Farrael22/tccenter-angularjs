angular.module("balcao.home").controller("homeController", function ($scope, $rootScope, $timeout, HomeService, ElementoAtivoFactory, EventosFactory, BalcaoStorage, Util, AtalhosFactory) {

    var vm = this;
    var ipEstacao = GLOBAL_SERVER.IP_ESTACAO;
    BalcaoStorage.salvarIpEstacao(ipEstacao);
    
    vm.elementoAtivo = '';

    vm.init = function (timeoutSistema) {
        AtalhosFactory.iniciarAtalhosDaTela($scope);
        var timeout = !Util.isNullOrUndefined(timeoutSistema) ? timeoutSistema : 1000;
        if (!BalcaoStorage.verificarSeDeveLimparBalcaoStorage(GLOBAL_SERVER.DATA, 10)) {
            limparLocalStorageERealizarBuscas(timeout);
        }
        else {
            var estacao = BalcaoStorage.obterDadosEstacao();

            if (estacao === null || typeof (estacao) !== "object" || estacao.Estacao === null || isNaN(estacao.Estacao) || estacao.Filial === null || isNaN(estacao.Filial)) {
                $timeout(function () {
                    EventosFactory.abrirModal($scope, 'Loading', 'Estamos preparando o sistema para você');
                    HomeService.obterDadosEstacao(ipEstacao, obterDadosEstacaoSucessoCallback, obterDadosEstacaoErroCallback);
                }, timeout);
            }
            else {
                atualizarTituloPagina(estacao.Estacao, estacao.Filial);
            }
        }
    };

    function limparLocalStorageERealizarBuscas(timeout) {
        $timeout(function () {
            EventosFactory.abrirModal($scope, 'Loading', 'Estamos preparando o sistema para você');
            
            HomeService.obterDadosEstacao(ipEstacao, obterDadosEstacaoSucessoCallback, obterDadosEstacaoErroCallback);
        }, timeout);
    }

    function obterDadosEstacaoSucessoCallback(dados) {
        $scope.$emit('fecharModalLoading');
        if (dados && dados.Data) {
            var estacaoIdentificada = false;

            for (var i = 0; i < dados.Data.length; i++) {
                if (dados.Data[i].Situacao == 0) {
                    estacaoIdentificada = true;
                    BalcaoStorage.salvarDadosEstacao(dados.Data[i]);

                    atualizarTituloPagina(dados.Data[i].Estacao, dados.Data[i].Filial);
                }
            }

            if (!estacaoIdentificada) {
                $timeout(function () {
                    var config = {
                        botoes: [{
                            texto: "Tentar novamente",
                            callback: function () {
                                vm.init(0);
                                $scope.$emit('fecharModalAlertaErro');
                            }
                        }],
                        esc: function () { },
                        enter: function () {
                            vm.init(0);
                            $scope.$emit('fecharModalAlertaErro');
                        }
                    };
                    EventosFactory.mensagemDeErro($scope, "Não foi possível identificar a filial. Entre em contato com a Central para registrar esta estação. IP: " + ipEstacao, null, config);
                });
            }
        }
        else {
            obterDadosEstacaoErroCallback("Não foi possível identificar a filial.");
        }
    }

    function obterDadosEstacaoErroCallback(mensagem) {
        $scope.$emit('fecharModalLoading');
        $timeout(function () {
            var config = {
                botoes: [{
                    texto: "Tentar novamente",
                    callback: function () {
                        vm.init(0);
                        $scope.$emit('fecharModalAlertaErro');
                    }
                }],
                esc: function () {
                },
                enter: function () {
                    vm.init(0);
                    $scope.$emit('fecharModalAlertaErro');
                }
            };
            EventosFactory.mensagemDeErro($scope, "Não foi possível identificar a filial.", null, config)
        });
    }

    function atualizarTituloPagina(balcao, filial) {
        var titulo = 'Balcão: ' + balcao + ' - Filial: ' + filial + ' - v' + GLOBAL_SERVER.VERSAO + ' - Drogaria Araujo S.A';
        $rootScope.tituloPagina = titulo;
    }
    
    var eventoSelecionarContainerPrincipal = $rootScope.$on('selecionarContainerPrincipal', function (event) {
        vm.focoOrcamento = false;
    });

    var eventoSelecionarContainerOrcamento = $rootScope.$on('selecionarContainerOrcamento', function (event) {
        vm.focoOrcamento = true;
    });

    $scope.$on('$destroy', function (event, param) {
        eventoSelecionarContainerPrincipal();
        eventoSelecionarContainerOrcamento();
    });


    AtalhosFactory.criarTelaDeAtalhosGenericos($scope);

    AtalhosFactory.criarAtalho($scope, ['ctrl', 'W'], function () { });
    AtalhosFactory.criarAtalho($scope,['ctrl', 'E'], function () { });
    AtalhosFactory.criarAtalho($scope,['ctrl', 'D'], function () { });
    AtalhosFactory.criarAtalho($scope,['ctrl', 'R'], function () { });
    AtalhosFactory.criarAtalho($scope,['ctrl', 'Q'], function () { });
    AtalhosFactory.criarAtalho($scope, ['ctrl', 'L'], function () { });
    AtalhosFactory.criarAtalho($scope, ['F1'], function () { });
    AtalhosFactory.criarAtalho($scope,['ctrl', 'home'], function () { });
    AtalhosFactory.criarAtalho($scope,['backspace'], function () { }, '#idBody');
});