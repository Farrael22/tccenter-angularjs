(function () {
    'use strict';

    angular.module("balcao").factory('BalcaoEnum', function BalcaoEnum() {

        return {
            CODIGO_FORMULA_MANIPULADA: '900001',
            TIPOS_RECEITA: {
                A: 'A',
                B: 'B1',
                B2: 'B2',
                ANTIBIOTICOS: 'C1A',
                C: 'C1P',
                VENDA_LIVRE: 'C1VL',
                FARMACIA_POPULAR: 'FP',
                FORMULA_MANIPULADA: 'C1FM'
            }
        };

    });
}());