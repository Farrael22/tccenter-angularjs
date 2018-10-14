(function () {
    'use strict';

    angular.module("tccenter").factory('BalcaoStorage', function BalcaoStorage($timeout, $rootScope, Util) {
        return {
            
            // VERIFICAR SE DEVE LIMPAR BALCAO STORAGE COM BASE NA DATA ARMAZENADA
            verificarSeDeveLimparBalcaoStorage: function (dataGlobalServer, numeroDeDias) {
                var dataArmazenada = obterItemJson('data-armazenada')
                if (dataGlobalServer && dataArmazenada) {
                    dataArmazenada = new Date(dataArmazenada);
                    dataArmazenada.setDate(dataArmazenada.getDate() + numeroDeDias);
                    if (dataGlobalServer.getTime() <= dataArmazenada.getTime())
                        return true;
                    else
                        salvarItemJson('data-armazenada', dataGlobalServer);
                }
                else if (dataGlobalServer) {
                    salvarItemJson('data-armazenada', dataGlobalServer);
                }
                return false;
            },

            // ORÇAMENTO
            salvarOrcamento: function (orcamento) {
                salvarItemJson('orcamento', orcamento);
            },
            obterOrcamento: function () {
                if (obterItemJson('orcamento'))
                    return obterItemJson('orcamento');
                else
                    return [];
            },
            removerOrcamento: function () {
                removerItem('orcamento');
            },

            // DADOS ESTACAO
            salvarDadosEstacao: function (dadosEstacao) {
                salvarItemJson('dados-estacao', dadosEstacao);
            },
            obterDadosEstacao: function () {
                return obterItemJson('dados-estacao');
            },
            removerDadosEstacao: function () {
                removerItem('dados-estacao');
            },

            // IP ESTAÇÃO
            salvarIpEstacao: function (ipEstacao) {
                salvarItemJson('ip-estacao', ipEstacao);
            },
            obterIpEstacao: function () {
                return obterItem('ip-estacao');
            },
            removerIpEstacao: function () {
                removerItem('ip-estacao');
            }
        };

        // MÉTODOS PRIVADOS

        function salvarItemJson(item, dados) {
            localStorage.setItem(item, JSON.stringify(dados));
        }

        function obterItemJson(item) {
            return JSON.parse(localStorage.getItem(item));
        }

        function salvarItem(item, dados) {
            localStorage.setItem(item, dados);
        }

        function obterItem(item) {
            return localStorage.getItem(item);
        }

        function removerItem(item) {
            localStorage.removeItem(item);
        }
    });
}());