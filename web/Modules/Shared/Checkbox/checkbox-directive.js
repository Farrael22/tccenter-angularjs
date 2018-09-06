angular.module("balcao").directive("checkbox", function () {
    return {
        templateUrl: "Shared/checkbox/checkbox",
        replace: true,
        restrict: "E",
        scope: {
            cbModel: "=",
            cbText: "@",
            cbSingleSelectGroup: "@"
        },
        controller: checkbox,
        controllerAs: 'ctrl',
    };
});

checkbox.$inject = ['$scope', '$rootScope'];
function checkbox($scope, $rootScope) {
    var vm = this;

    vm.cbModel = $scope.cbModel;
    vm.cbText = $scope.cbText;
    vm.cbSingleSelectGroup = $scope.cbSingleSelectGroup;

    $scope.$watch("cbModel", function () {
        vm.cbModel = $scope.cbModel;
    });

    vm.toggleCheckbox = function(checked){
        $scope.cbModel = vm.cbModel = !checked;
        if (vm.cbModel && vm.cbSingleSelectGroup) {
            $scope.$emit('disableCheckbox', vm);
        }
    }

    $rootScope.$on('disableCheckbox', function (event, vmCheckBoxSelected) {
        if (vmCheckBoxSelected != vm && vm.cbModel && vm.cbSingleSelectGroup == vmCheckBoxSelected.cbSingleSelectGroup) {
            vm.cbModel = false;
        }
    });
}