angular.module("balcao").directive("campobusca", ['ElementoAtivoFactory', function (ElementoAtivoFactory) {
    return {
        templateUrl: "Shared/campobusca/campobusca",
        replace: true,
        restrict: "E",
        scope: {
            idElemento: "@",
            placeholder: "@",
            modelElemento: "=",
            buscaChange: "=",
            loading: "=",
            funcaoBusca: "&",
            disable: "="
        },
        link: function campoBuscaController($scope) {
            $scope.buscar = function () {
                if (!$scope.disable) {
                    $scope.funcaoBusca({ texto: $scope.modelElemento });
                    ElementoAtivoFactory.focarElemento($scope.idElemento);
                }
            }
            $scope.clicarCampoBusca = function () {
                if ($scope.disable) {
                    $scope.disable = false;
                    $scope.modelElemento = '';
                    $scope.funcaoBusca({ texto: $scope.modelElemento, bipado: false });
                    ElementoAtivoFactory.focarElemento($scope.idElemento);
                }
            }
            var lo = new LeitorOptico();
            lo.verificar($scope.idElemento, {
                success: function (value) { // caso tenha identificado a inserção por leitura óptica
                    $scope.funcaoBusca({ texto: value, bipado: true });
                    $scope.modelElemento = '';
                    $scope.$apply();
                },
                error: function () { // caso a insersão não tenha sido por leitura óptica
                    $scope.funcaoBusca({ texto: $scope.modelElemento, bipado: false });
                }
            });
        }
    };
}]);
