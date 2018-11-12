angular.module("tccenter").directive("publicacao", function () {
    return {
        templateUrl: 'Shared/publicacao/publicacao',
        restrict: 'E',
        scope: {
            Publicacoes: "="
        },
        controller: publicacaoController,
        controllerAs: 'ctrl',
    };
});

publicacaoController.$inject = ['$scope', '$location', 'ElementoAtivoFactory', 'TccenterStorage', 'PublicacaoService'];
function publicacaoController($scope, $location, ElementoAtivoFactory, TccenterStorage, PublicacaoService) {
    var vm = this;

}