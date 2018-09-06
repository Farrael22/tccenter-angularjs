angular.module("balcao").directive("titulopagina", function () {
    return {
        templateUrl: "Shared/titulopagina/titulopagina",
        replace: true,
        restrict: "E",
        scope: {
            titulo: "@",
            voltar: "&"
        },
        controller : titulopaginaController,
        controllerAs: 'ctrl',
    };
});

titulopaginaController.$inject = ['$scope'];
function titulopaginaController($scope) {
    var vm = this;
    vm.voltar = $scope.voltar;
    vm.titulo = $scope.titulo;

    vm.btnVoltar = function () {
        vm.voltar();
    }
}