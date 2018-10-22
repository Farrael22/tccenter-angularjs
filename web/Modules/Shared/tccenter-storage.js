(function () {
    'use strict';

    angular.module("tccenter").factory('TccenterStorage', function TccenterStorage($timeout, $rootScope, Util) {
        return {
            // USUARIO
            salvarUsuario: function (usuario) {
                salvarItemJson('usuario', usuario);
            },
            obterUsuario: function () {
                if (obterItemJson('usuario'))
                    return obterItemJson('usuario');
                else
                    return false;
            },
            removerUsuario: function () {
                removerItem('usuario');
            },

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