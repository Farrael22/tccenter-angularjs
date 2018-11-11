angular.module("tccenter.cadastroCliente").controller("CadastroClienteController", function ($scope, $rootScope, $location, $timeout, CadastroClienteService, ElementoAtivoFactory, EventosFactory, TccenterStorage, Util, AtalhosFactory) {

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
        vm.topicoMestre = data;
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
            for (var i = 0; i < vm.topicoMestre.length; i++) {
                topicos.push({
                    IdTopicoMestre: vm.topicoMestre[i].IdTopicoMestre,
                    DescricaoTopicoMestre: vm.topicoMestre[i].DescricaoTopicoMestre,
                    TopicosInteressantes: []
                });

                for (var j = 0; j < vm.topicoMestre[i].TopicosInteressantes.length; j++) {
                    if (vm.topicoMestre[i].TopicosInteressantes[j].checked) {
                        topicos[i].TopicosInteressantes.push({
                            IdTopicosInteressantes: vm.topicoMestre[i].TopicosInteressantes[j].IdTopicosInteressantes
                        });
                    }
                }
            }

            var param = {
                Nome: vm.Usuario.Nome,
                Email: vm.Usuario.Email,
                Senha: vm.Usuario.Senha,
                Profissao: vm.Usuario.Profissao,
                TopicosInteressesMestre: topicos
            };
            CadastroClienteService.realizarCadastro(param, realizarCadastroSucessoCallback, realizarCadastroErroCallback);
        }
    };

    function realizarCadastroSucessoCallback() {
        $location.path("login");
    }

    function realizarCadastroErroCallback(mensagem) {
        mensagemDeErro(mensagem);
    }

    function mensagemDeErro(mensagem) {
        EventosFactory.mensagemDeErro($scope, mensagem);
    }

    AtalhosFactory.criarAtalho($scope, ['enter'], function () {
        vm.cadastrarUsuario();
    }, '#submitFormulario');

});