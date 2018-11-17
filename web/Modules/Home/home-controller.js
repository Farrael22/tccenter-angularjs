angular.module("tccenter.home").controller("HomeController", function ($scope, $rootScope, $timeout, $location, HomeService, ElementoAtivoFactory, EventosFactory, TccenterStorage, Util, AtalhosFactory) {

    if (!TccenterStorage.obterUsuario()) {
        $location.path("login");
    }

    var vm = this;
    vm.TopicosInteressantes = [];
    vm.Publicacoes = [];

    vm.iniciarHome = function () {
        AtalhosFactory.iniciarAtalhosDaTela($scope);
        vm.Usuario = TccenterStorage.obterUsuario();
        HomeService.buscarTopicosInteressantes(buscarTopicosInteressantesSucesso);
        HomeService.obterPublicacoesPorInteresse(vm.Usuario.Id, obterPublicacoesPorInteresseSucesso);
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

    function obterPublicacoesPorInteresseSucesso(data) {
        vm.Publicacoes = data;
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