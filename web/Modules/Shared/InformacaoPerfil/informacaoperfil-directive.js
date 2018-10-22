angular.module("tccenter").directive("informacaoperfil", function () {
    return {
        templateUrl: 'Shared/informacaoperfil/informacaoperfil',
        restrict: 'E',
        scope: {
            Usuario: "="
        },
        controller: informacaoperfilController,
        controllerAs: 'ctrl',
    };
});

informacaoperfilController.$inject = ['$scope', 'ElementoAtivoFactory', 'TccenterStorage'];
function informacaoperfilController($scope, ElementoAtivoFactory, TccenterStorage) {
    var vm = this;
    vm.Usuario = TccenterStorage.obterUsuario();
}