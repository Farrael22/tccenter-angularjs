angular.module("tccenter").directive("notificacao", function () {
    return {
        templateUrl: "Shared/notificacao/notificacao",
        replace: true,
        restrict: "E",
        scope: {
            array: "="
        },
        controller: notificacaoController,
        controllerAs: 'ctrl',
    };
});

notificacaoController.$inject = ['$scope', 'ElementoAtivoFactory'];
function notificacaoController($scope, ElementoAtivoFactory) {
    var tipos = ['padrao', 'sucesso', 'alerta', 'erro'];
    var vm = this;

    vm.array = $scope.array;

    function setTimeExpiracao(idx) {
        if (vm.array[idx].tempo != null && vm.array[idx].tempo < 0) return;

        setTimeout(function () {
            vm.array[idx].expirado = true;
            vm.array.splice(idx, 1);
            $scope.$apply();
        }, isNaN(vm.array[idx].tempo) ? 5000 : vm.array[idx].tempo);
    }

    for (var i = 0; i < vm.array.length; i++) {
        vm.array[i].idx = i;
        vm.array[i].tipo = (!vm.array[i].tipo || tipos.indexOf(vm.array[i].tipo.toLowerCase()) < 0) ? 'padrao' : vm.array[i].tipo;

        setTimeExpiracao(i);        
    }
    
    vm.fecharNotificacao = function (idx) {
        vm.array[idx].expirado = true;

        if (typeof (vm.array[idx].callbackFechar) == 'function') {
            vm.array[idx].callbackFechar(vm.array[idx]);
        } 
    }

    vm.calcPosicao = function (key) {
        var margin = 0;

        for (var i = 0; i < vm.array.length; i++) {
            if (!vm.array[i].expirado && i <= key) {
                margin += 55;
            }
        }

        return margin - 45;
    }

    vm.executarCallback = function (n) {
        if (typeof (n.callback) == 'function') {
            n.callback(n);
        }
    }

}