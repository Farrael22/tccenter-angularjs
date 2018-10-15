angular.module("tccenter.cadastroCliente").controller("CadastroClienteController", function ($scope, $rootScope, $location, $timeout, CadastroClienteService, ElementoAtivoFactory, EventosFactory, BalcaoStorage, Util, AtalhosFactory) {

    var vm = this;

    vm.nomeValido = true;
    vm.emailValido = true;
    vm.senhaValida = true;
    vm.profissaoValida = true;
    vm.Usuario = {
        Nome: null,
        Email: null,
        Senha: null,
        Profissao: null
    };

    AtalhosFactory.criarTelaDeAtalhosGenericos($scope);

    vm.iniciarCadastroCliente = function () {
        AtalhosFactory.iniciarAtalhosDaTela($scope);

        CadastroClienteService.buscarTopicosInteressantes(buscarTopicosInteressantesSucesso);
    };

    function buscarTopicosInteressantesSucesso(data) {
        vm.topicosInteressantes = data;
    }

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

    vm.marcarCheck = function (item) {
        if (item.checked) {
            item.checked = false;
            return;
        }
        item.checked = true;
    };

    vm.obterImagemAvatar = function (element) {
        var dataFile = element.files[0];
        var reader = new FileReader();
        reader.onload = function (e) {
            vm.$apply(function () {
                vm.dataFile = reader.result;
            });
        };
        reader.readAsBinaryString(dataFile);
    };

    vm.cadastrarUsuario = function () {
        var topicos = [];
        vm.validarNomeUsuario();
        vm.validarEmailUsuario();
        vm.validarSenhaUsuario();
        vm.validarProfisssaoUsuario();

        if (vm.nomeValido && vm.emailValido && vm.senhaValida && vm.profissaoValida) {
            for (var i = 0; i < vm.topicosInteressantes.length; i++) {
                if (vm.topicosInteressantes[i].checked) {
                    topicos.push(vm.topicosInteressantes[i].IdTopicosInteressantes);
                }
            }

            var param = {
                Nome: vm.Usuario.Nome,
                Email: vm.Usuario.Email,
                Senha: vm.Usuario.Senha,
                Profissao: vm.Usuario.Profissao,
                Avatar: imagem,
                TopicosInteressantes: topicos
            };
            CadastroClienteService.realizarCadastro(param, realizarCadastroSucessoCallback, realizarCadastroErroCallback);
        }
    };

    function realizarCadastroSucessoCallback() {

    }

    function realizarCadastroErroCallback() {

    }



    AtalhosFactory.criarAtalho($scope, ['enter'], function () {
        vm.cadastrarUsuario();
    },'#submitFormulario');

});