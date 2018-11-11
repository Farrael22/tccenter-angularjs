angular.module("tccenter.perfil").controller("PerfilController", function ($scope, $rootScope, $timeout, $location, HomeService, ElementoAtivoFactory, EventosFactory, TccenterStorage, Util, AtalhosFactory) {

    if (!TccenterStorage.obterUsuario()) {
        $location.path("login");
    }

    var vm = this;
    vm.TopicosInteressantes = [];

    vm.iniciarHome = function () {
        AtalhosFactory.iniciarAtalhosDaTela($scope);
        HomeService.buscarTopicosInteressantes(buscarTopicosInteressantesSucesso);
    };

    function buscarTopicosInteressantesSucesso(data) {
        for (var i = 0; i < data.length; i++) {
            for (var j = 0; j < data[i].TopicosInteressantes.length; j++) {
                vm.TopicosInteressantes.push({
                    IdTopicoMestre: data[i].IdTopicoMestre,
                    IdTopicosInteressantes: data[i].TopicosInteressantes[j].IdTopicosInteressantes,
                    DescricaoTopico: data[i].TopicosInteressantes[j].DescricaoTopico
                });
                TccenterStorage.salvarTopicosInteressantes(vm.TopicosInteressantes);
            }
        }
    }
    
    vm.exibirCadastro = function () {
        $timeout(function () {
            EventosFactory.abrirModal($scope, 'CadastroPublicacao');
        });
    };

    AtalhosFactory.criarTelaDeAtalhosGenericos($scope);

    AtalhosFactory.criarAtalho($scope, ['ctrl', 'P'], function () {
        vm.exibirCadastro();
    });

});