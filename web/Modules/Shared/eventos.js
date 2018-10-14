(function () {
    'use strict';

    angular.module("tccenter").factory('EventosFactory', function EventosFactory($timeout, $rootScope, ElementoAtivoFactory) {
        return {
            mensagemDeErro: function (scope, mensagem, elemento, config) {
                ElementoAtivoFactory.definirElementoFocoRetornoModal('AlertaErro', elemento);
                scope.$emit('abrirModalErro', mensagem, config);
            },
            mensagemDeAlerta: function (scope, mensagem, elemento, config) {
                ElementoAtivoFactory.definirElementoFocoRetornoModal('AlertaErro', elemento);
                scope.$emit('abrirModalAlerta', mensagem, config);
            },
            abrirModal: function (scope, nome, parametros, elementoFocoRetorno) {
                ElementoAtivoFactory.definirElementoFocoRetornoModal(nome, elementoFocoRetorno);
                scope.$emit('abrirModal' + nome, parametros);
            },
            abrirModalLoading: function (scope, mensagem, elemento) {
                ElementoAtivoFactory.definirElementoFocoRetornoModal('Loading', elemento);
                scope.$emit('abrirModalLoading', mensagem);
            }
        };
      
    });
}());