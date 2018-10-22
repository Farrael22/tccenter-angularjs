angular.module("tccenter.login").controller("LoginController", function ($scope, $rootScope, $timeout, $location, LoginService, ElementoAtivoFactory, EventosFactory, TccenterStorage, Util, AtalhosFactory) {

    var vm = this;
    vm.emailValido = true;
    vm.senhaValida = true;
    vm.elemeemailValidontoAtivo = '';
    AtalhosFactory.criarTelaDeAtalhosGenericos($scope);


    vm.validarEmail = function () {
        vm.emailValido = Util.validarEmailUsuario(vm.email);
    };

    vm.validarSenha = function () {
        vm.senhaValida = Util.validarSenhaUsuario(vm.senha);
    };

    vm.efetuarLogin = function () {
        vm.validarEmail();
        vm.validarSenha();

        if (vm.emailValido && vm.senhaValida) {
            var param = {
                email: vm.email,
                senha: vm.senha
            };
            LoginService.efetuarLogin(param, efetuarLoginSucessoCallback, efetuarLoginErroCallback);
        }
    };

    function efetuarLoginSucessoCallback(data) {
        TccenterStorage.salvarUsuario(data);
        $rootScope.$emit('UsuarioLogado');
        $location.path("home");
    }

    function efetuarLoginErroCallback(mensagem) {
        mensagemDeErro(mensagem);
    }

    function mensagemDeErro(mensagem) {
        EventosFactory.mensagemDeErro($scope, mensagem);
    }

    AtalhosFactory.criarAtalho($scope, ['enter'], function () {
        vm.efetuarLogin(); 
    });

    AtalhosFactory.iniciarAtalhosDaTela($scope);

});