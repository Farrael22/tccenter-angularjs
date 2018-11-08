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

informacaoperfilController.$inject = ['$scope', 'ElementoAtivoFactory', 'TccenterStorage', 'InformacaoPerfilService'];
function informacaoperfilController($scope, ElementoAtivoFactory, TccenterStorage, InformacaoPerfilService) {
    var vm = this;
    vm.Usuario = TccenterStorage.obterUsuario();

    buscarQuantidadePublicacoesUsuario();
    buscarQuantidadeSeguidoresUsuario();
    buscarUsuariosSeguidos();

    function buscarQuantidadePublicacoesUsuario() {
        InformacaoPerfilService.buscarQuantidadePublicacoesUsuario(vm.Usuario.Id, buscarQuantidadePublicacoesUsuarioSucesso, buscarQuantidadePublicacoesUsuarioErro);
    }

    function buscarQuantidadePublicacoesUsuarioSucesso(data) {
        vm.quantidadePublicacoes = data;
    }

    function buscarQuantidadePublicacoesUsuarioErro(mensagem) {
        vm.quantidadePublicacoes = "Erro";
    }

    function buscarQuantidadeSeguidoresUsuario() {
        InformacaoPerfilService.buscarQuantidadeSeguidoresUsuario(vm.Usuario.Id, buscarQuantidadeSeguidoresUsuarioSucesso, buscarQuantidadeSeguidoresUsuarioErro);
    }

    function buscarQuantidadeSeguidoresUsuarioSucesso(data) {
        vm.quantidadeSeguidores = data;
    }

    function buscarQuantidadeSeguidoresUsuarioErro(mensagem) {
        vm.quantidadeSeguidores = "Erro";
    }

    function buscarUsuariosSeguidos() {
        InformacaoPerfilService.buscarUsuariosSeguidos(vm.Usuario.Id, buscarUsuariosSeguidosSucesso);
    }

    function buscarUsuariosSeguidosSucesso(data) {
        vm.usuariosSeguidos = data;
    }

    vm.exibirPerfilUsuario = function () {

    };

    vm.editarUsuario = function () {

    };

    vm.exibirPrimeiroNome = function (nome) {
        return nome.split(' ')[0];
    };
}