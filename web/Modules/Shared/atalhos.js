(function () {
    'use strict';

    angular.module("balcao").factory('AtalhosFactory', function AtalhosFactory($timeout, $rootScope, ElementoAtivoFactory) {

        var at = Atalho().init();

        var telasBalcao = [];


        function funcaoVazia() { }

        function obterTela(scope) {
            for (var i = 0; i < telasBalcao.length; i++) {
                if (telasBalcao[i].scope === scope) {
                    return telasBalcao[i];
                }
            }
            return null;
        }

        function criarTela(scope, tipoControllerTela) {
            var tela = obterTela(scope);
            if (tela === null) {
                tela = {
                    scope: scope,
                    atalhosGenericos: [],
                    atalhosAtivos: false,
                    scopeBloqueio: null
                }
                telasBalcao.push(tela);
            }
            return tela;
        }

        function removerTela(scope) {
            for (var i = 0; i < telasBalcao.length; i++) {
                if (telasBalcao[i].scope === scope) {
                    telasBalcao.splice(i, 1);
                    break;
                }
            }
        }

        function ativarAtalhosDaTela(scope) {
            var tela = obterTela(scope);
            if (tela) {
                tela.atalhosAtivos = true;
                for (var indiceAtalho = 0; indiceAtalho < tela.atalhosGenericos.length; indiceAtalho++) {
                    recriarAtalho(tela, indiceAtalho);
                }
            }
        }

        function bloquearAtalhosDaTela(scope, liberarAcaoPadraoDasTeclas) {
            var tela = obterTela(scope);
            if (tela && tela.atalhosGenericos.length > 0) {
                tela.atalhosAtivos = false;
                for (var i = 0; i < tela.atalhosGenericos.length; i++) {
                    var prevent = liberarAcaoPadraoDasTeclas ? false : tela.atalhosGenericos[i].prevent;
                    at.criarAtalho(tela.atalhosGenericos[i].teclas, funcaoVazia, null, prevent);
                }
            }
        }

        function bloquearAtalhosDeOutrasTelas(scope) {
            for (var i = 0; i < telasBalcao.length; i++) {
                if (telasBalcao[i].scope !== scope && telasBalcao[i].atalhosAtivos === true) {
                    telasBalcao[i].scopeBloqueio = scope;
                    bloquearAtalhosDaTela(telasBalcao[i].scope);
                }
            }
        }

        function desbloquearAtalhosDeOutrasTelas(scope) {
            for (var i = 0; i < telasBalcao.length; i++) {
                if (telasBalcao[i].scopeBloqueio === scope && telasBalcao[i].atalhosAtivos === false) {
                    telasBalcao[i].scopeBloqueio = null;
                    ativarAtalhosDaTela(telasBalcao[i].scope);
                }
            }
        }

        function recriarAtalho(tela, indiceAtalho) {
            at.criarAtalho(tela.atalhosGenericos[indiceAtalho].teclas, function () {
                tela.atalhosGenericos[indiceAtalho].funcao();
                tela.scope.$apply();
            }, null, tela.atalhosGenericos[indiceAtalho].prevent);
        }

        //Função para bloquear atalhos que não estão sendo usados pelas telas carregadas e que possuem atalhos ativos.
        //function bloquearAtalhosNavegador(scope) {
        //    var vet = [['ctrl', 'H'], ['ctrl', 'O'], ['esc'], ['ctrl', 'R']];

        //    for (var i = 0; i < telasBalcao.length ; i++) {
        //        if (telasBalcao[i].atalhosAtivos) {
        //            for (var j = 0; j < telasBalcao[i].atalhosGenericos.length; j++) {
        //                for (var k = 0; k < vet.length; k++) {
        //                    if (vet[k] != null && vet[k].toString() == telasBalcao[i].atalhosGenericos[j].teclas.toString())
        //                        vet[k] = null;
        //                }
        //            }
        //        }
        //    }

        //    for (var x = 0; x < vet.length; x++) {
        //        if (vet[x] != null) {
        //            obterTela(scope).atalhosGenericos.push({
        //                teclas: vet,
        //                funcao: funcaoVazia,
        //                prevent: true
        //            });
        //        }
        //    }
        //}

        return {
            criarTelaDeAtalhosGenericos: function (scope) {
                var tela = criarTela(scope);
                scope.$on('$destroy', function (event, param) {
                    bloquearAtalhosDaTela(tela.scope, true);
                    desbloquearAtalhosDeOutrasTelas(tela.scope);
                    removerTela(tela.scope);
                });
            },
            iniciarAtalhosDaTela: function (scope, ehModal) {
                if (obterTela(scope)) {
                    if (ehModal === true) {
                        bloquearAtalhosDeOutrasTelas(scope);
                    }
                    ativarAtalhosDaTela(scope);
                }
            },
            fecharModal: function (scope, nomeModal, elementoFoco) {
                bloquearAtalhosDaTela(scope, true);
                desbloquearAtalhosDeOutrasTelas(scope);
                ElementoAtivoFactory.focarElementoFocoRetornoModal(nomeModal, elementoFoco);
            },
            criarAtalho: function (scope, teclas, funcao, elementoFoco, prevent) {
                var telaAtalho = obterTela(scope);
                if (telaAtalho && elementoFoco) {
                    at.criarAtalho(teclas, function () {
                        funcao();
                        scope.$apply();
                    }, elementoFoco, prevent);
                    scope.$on('$destroy', function (event, param) {
                        at.criarAtalho(teclas, funcaoVazia, elementoFoco, false)
                    });
                }
                else if (telaAtalho) {
                    telaAtalho.atalhosGenericos.push({
                        teclas: teclas,
                        funcao: funcao,
                        prevent: prevent
                    });
                }
            }
        };

    });
}());