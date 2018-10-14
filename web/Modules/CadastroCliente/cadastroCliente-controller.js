angular.module("tccenter.cadastroCliente").controller("CadastroClienteController", function ($scope, $rootScope, $location, $timeout, CadastroClienteService, ElementoAtivoFactory, EventosFactory, BalcaoStorage, Util, AtalhosFactory) {

    var vm = this;

    vm.nomeValido = true;
    vm.emailValido = true;
    vm.senhaValida = true;
    vm.profissaoValida = true;

    AtalhosFactory.criarTelaDeAtalhosGenericos($scope);

    vm.validarNomeUsuario = function () {
        vm.nomeValido = Util.validarNomeUsuario(vm.Usuario.Nome);
    };

    vm.validarEmailUsuario = function () {
        vm.emailValido = Util.validarEmailUsuario(vm.Usuario.Email);
    };

    vm.validarSenhaUsuario = function () {
        vm.senhaValida = Util.validarSenhaUsuario(vm.Usuario.Senha);
    };

    vm.validarProfisssaoUsuario = function () {
        vm.profissaoValida = Util.validarProfisssaoUsuario(vm.Usuario.Profissao);
    };

    AtalhosFactory.iniciarAtalhosDaTela($scope);

});