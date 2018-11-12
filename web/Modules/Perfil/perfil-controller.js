angular.module("tccenter.perfil").controller("PerfilController", function ($scope, $rootScope, $timeout, $location, ElementoAtivoFactory, EventosFactory, TccenterStorage, Util, AtalhosFactory, PerfilService, InformacaoPerfilService) {

    if (!TccenterStorage.obterUsuario()) {
        $location.path("login");
    }

    var vm = this;
    vm.usuarioSeguido = false;

    vm.iniciarPerfil = function () {
        $timeout(function () {
            vm.UsuarioLogado = TccenterStorage.obterUsuario();
            buscarQuantidadePublicacoesUsuario();
            buscarQuantidadeSeguidoresUsuario();
            verificarUsuarioJaSeguido();
        });
        AtalhosFactory.iniciarAtalhosDaTela($scope);

    };

    var eventoExibirPerfilUsuario = $rootScope.$on('exibirPerfilUsuario', function (event, idUsuario) {
        PerfilService.buscarUsuarioPorId(idUsuario, buscarUsuarioPorIdSucessoCallback);
    });

    function buscarUsuarioPorIdSucessoCallback(data) {
        vm.UsuarioPerfil = data;
    }

    function buscarQuantidadePublicacoesUsuario() {
        InformacaoPerfilService.buscarQuantidadePublicacoesUsuario(vm.UsuarioPerfil.Id, buscarQuantidadePublicacoesUsuarioSucesso);
    }

    function buscarQuantidadePublicacoesUsuarioSucesso(data) {
        vm.quantidadePublicacoes = data;
    }

    function buscarQuantidadeSeguidoresUsuario() {
        InformacaoPerfilService.buscarQuantidadeSeguidoresUsuario(vm.UsuarioPerfil.Id, buscarQuantidadeSeguidoresUsuarioSucesso);
    }

    function buscarQuantidadeSeguidoresUsuarioSucesso(data) {
        vm.quantidadeSeguidores = data;
    }

    function verificarUsuarioJaSeguido() {
        InformacaoPerfilService.buscarUsuariosSeguidos(vm.UsuarioLogado.Id, buscarUsuariosSeguidosSucessoCallback);
    }

    function buscarUsuariosSeguidosSucessoCallback(data) {
        for (var i = 0; i < data.length; i++) {
            if (data.Id === vm.UsuarioPerfil.Id) {
                vm.usuarioSeguido = true;
            }
        }
    }

    vm.seguirUsuario = function () {
        data = {
            idUsuarioLogado: vm.UsuarioLogado.Id,
            idUsuarioSeguir: vm.UsuarioPerfil.Id
        };

        PerfilService.seguirUsuario(data, seguirUsuarioSucessoCallback, seguirUsuarioErroCallback);
    };

    function seguirUsuarioSucessoCallback() {
        vm.usuarioSeguido = true;
        EventosFactory.mensagemDeSucesso($scope, "Usuário seguido com sucesso!");
    }

    function seguirUsuarioErroCallback(mensagem) {
        EventosFactory.mensagemDeErro($scope, mensagem);
    }

    vm.pararDeSeguirUsuario = function () {
        data = {
            idUsuarioLogado: vm.UsuarioLogado.Id,
            idUsuarioSeguir: vm.UsuarioPerfil.Id
        };

        PerfilService.pararSeguirUsuario(data, pararSeguirUsuarioSucessoCallback, pararSeguirUsuarioErroCallback);
    };

    function pararSeguirUsuarioSucessoCallback() {
        vm.usuarioSeguido = false;
        EventosFactory.mensagemDeSucesso($scope, "Parou de seguir o usuário com sucesso!");
    }

    function pararSeguirUsuarioErroCallback(mensagem) {
        EventosFactory.mensagemDeErro($scope, mensagem);
    }

    AtalhosFactory.criarTelaDeAtalhosGenericos($scope);

    AtalhosFactory.criarAtalho($scope, ['ctrl', 'P'], function () {
        vm.exibirCadastro();
    });

});