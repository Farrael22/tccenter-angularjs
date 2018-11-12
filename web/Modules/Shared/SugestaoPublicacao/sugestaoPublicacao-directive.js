angular.module("tccenter").directive("sugestaopublicacao", function () {
    return {
        templateUrl: 'Shared/sugestaopublicacao/sugestaopublicacao',
        restrict: 'E',
        scope: {
            Usuario: "="
        },
        controller: sugestaopublicacaoController,
        controllerAs: 'ctrl',
    };
});

sugestaopublicacaoController.$inject = ['$scope', '$location', 'ElementoAtivoFactory', 'TccenterStorage', 'SugestaoPublicacaoService'];
function sugestaopublicacaoController($scope, $location, ElementoAtivoFactory, TccenterStorage, InformacaoPerfilService) {
    var vm = this;

}