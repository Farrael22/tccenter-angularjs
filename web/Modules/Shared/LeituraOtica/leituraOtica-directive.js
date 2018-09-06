angular.module("balcao").directive("leituraotica", function () {
    return {
        templateUrl: "Shared/leituraotica/leituraotica",
        replace: true,
        restrict: "E",
        scope: {
            idElemento: "@",
            placeholder: "@",
            modelElemento: "=",
            vendedorInvalido: "=",
            loLoading: "="
        },
        controller: leituraOticaController,
        controllerAs: 'ctrl',
    };
});

leituraOticaController.$inject = ['$scope', 'ElementoAtivoFactory'];
function leituraOticaController($scope, ElementoAtivoFactory) {
    var vm = this;

    $scope.$watch('loLoading', function (val) {
        vm.loading = val == true ? true : false;
    });

    vm.loading = $scope.loLoading == true ? true : false;
    vm.idElemento = $scope.idElemento;
    vm.placeholder = $scope.placeholder;
    vm.modelElemento = $scope.modelElemento;
    vm.vendedorInvalido = $scope.vendedorInvalido;

    vm.focarCampoAtivo = ElementoAtivoFactory.focarElemento;

    vm.definirCampoAtivo = ElementoAtivoFactory.definirElementoAtivo;
    
}