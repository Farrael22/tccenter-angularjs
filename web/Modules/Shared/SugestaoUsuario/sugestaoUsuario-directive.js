angular.module("tccenter").directive("sugestaousuario", function () {
    return {
        templateUrl: 'Shared/sugestaousuario/sugestaousuario',
        restrict: 'E',
        scope: {
            Usuario: "="
        },
        controller: sugestaousuarioController,
        controllerAs: 'ctrl',
    };
});

sugestaousuarioController.$inject = ['$scope', '$location', 'ElementoAtivoFactory', 'TccenterStorage', 'SugestaoUsuarioService'];
function sugestaousuarioController($scope, $location, ElementoAtivoFactory, TccenterStorage, SugestaoUsuarioService) {
    var vm = this;

}