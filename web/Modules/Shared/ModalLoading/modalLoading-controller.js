angular.module("balcao.modalLoading").controller("modalLoadingController", function ($scope, $rootScope, $timeout, Util, ElementoAtivoFactory, AtalhosFactory) {
    var vm = this;

    vm.exibirModal = false;
    
    var eventoFecharModalLoading = $rootScope.$on('fecharModalLoading', function (event) {
        if (vm.exibirModal)
            vm.fecharModal();
    });

    var eventoAbrirModalLoading = $rootScope.$on('abrirModalLoading', function (event, mensagem, titulo) {
        vm.mensagem = mensagem;
        vm.titulo = typeof(titulo) != "undefined" ? titulo : "Aguarde um instante";
        vm.abrirModal();
    });

    $scope.$on('$destroy', function (event, param) {
        eventoAbrirModalLoading();
        eventoFecharModalLoading();
    });

    vm.abrirModal = function () {
        AtalhosFactory.iniciarAtalhosDaTela($scope, true);
        vm.exibirModal = true;
        Util.impedirScroll();
    }

    vm.fecharModal = function () {
        vm.exibirModal = false;
        Util.permitirScroll();
        AtalhosFactory.fecharModal($scope, 'Loading');
    }

    AtalhosFactory.criarTelaDeAtalhosGenericos($scope);

    AtalhosFactory.criarAtalho($scope, ['tab'], function () { });

});