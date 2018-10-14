(function () {
    'use strict';

    angular.module("tccenter").factory('ElementoAtivoFactory', function ElementoAtivoFactory($timeout, $rootScope) {

        var elementoAtivo = '';
        var pilhaElementosFocoModal = [];

        return {
            definirElementoAtivo: definirElementoAtivo,
            definirElementoFocoRetornoModal: definirElementoFocoRetornoModal,
            focarElemento: focarElemento,
            focarElementoFocoRetornoModal: function (modal, elemento) {
                desempilharElementoFocoRetornoModal(modal, elemento)
            }
        };

        function obterElementoAtivo() {
            if (document.activeElement)
                return document.activeElement.id;

            return '';
        }

        function definirElementoAtivo(idElemento) {
            if (idElemento)
                elementoAtivo = idElemento;
            else
                elementoAtivo = obterElementoAtivo();
        }

        function definirElementoFocoRetornoModal(modal, idElemento) {
            $timeout(function () {
                var elementoFocoRetornoModal;

                if (idElemento)
                    elementoFocoRetornoModal = idElemento;
                else if (obterElementoAtivo() && !existeModalAberto())
                    elementoFocoRetornoModal = obterElementoAtivo();
                else
                    elementoFocoRetornoModal = elementoAtivo;

                empilharElementoFocoRetornoModal(modal, elementoFocoRetornoModal);

                document.activeElement.blur();
            });
        }

        function focarElemento (elemento) {
            $timeout(function () {
                if (elemento) {
                    definirElementoAtivo(elemento);
                }
                if (document.getElementById(elementoAtivo)) {
                    document.getElementById(elementoAtivo).focus();
                }
            });
        }

        function existeModalAberto() {
            if (pilhaElementosFocoModal && pilhaElementosFocoModal.length > 0) {
                return true;
            }
            return false;
        }

        function empilharElementoFocoRetornoModal(modal, elemento) {
            var inserirNovo = true;
            for (var i = 0; i < pilhaElementosFocoModal.length; i++) {
                if (pilhaElementosFocoModal[i].modal == modal) {
                    var objetoElemento = angular.copy(pilhaElementosFocoModal[i]);
                    objetoElemento.elemento = elemento;
                    if (i != pilhaElementosFocoModal.length - 1) {
                        pilhaElementosFocoModal.splice(i, 1);
                        pilhaElementosFocoModal.push(objetoElemento);
                    }
                    inserirNovo = false;
                    break;
                }
            }
            if (inserirNovo) {
                pilhaElementosFocoModal.push({modal: modal, elemento: elemento});
            }
        }
         
        function desempilharElementoFocoRetornoModal(modal, elemento) {
            for (var i = 0; i < pilhaElementosFocoModal.length; i++) {
                if (pilhaElementosFocoModal[i].modal == modal) {
                    if (i == pilhaElementosFocoModal.length - 1) {
                        var elementoParaFocar = elemento ? elemento : pilhaElementosFocoModal[i].elemento
                        focarElemento(elementoParaFocar);
                    }
                    pilhaElementosFocoModal.splice(i, 1);
                    break;
                }
            }
        }
    });
}());