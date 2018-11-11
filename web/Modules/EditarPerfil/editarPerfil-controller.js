angular.module("tccenter.editarPerfil").controller("EditarPerfilController", function ($scope, $rootScope, $timeout, $location, EditarPerfilService, EventosFactory, Util, TccenterStorage, AtalhosFactory) {

    if (!TccenterStorage.obterUsuario()) {
        $location.path("login");
    }
    var vm = this;
    vm.topicoMestre = [];
    vm.nomeValido = true;
    vm.emailValido = true;
    vm.senhaAntigaValida = true;
    vm.senhaValida = true;
    vm.profissaoValida = true;

    vm.iniciarEditarPerfil = function () {
        AtalhosFactory.iniciarAtalhosDaTela($scope);
        vm.Usuario = TccenterStorage.obterUsuario();
        EditarPerfilService.buscarTopicosInteressantes(buscarTopicosInteressantesSucesso);
    };

    function buscarTopicosInteressantesSucesso(data) {
        vm.topicoMestre = data;
        marcarInteressesPreviosUsuario();
    }

    function marcarInteressesPreviosUsuario() {
        for (var i = 0; i < vm.topicoMestre.length; i++) {
            for (var j = 0; j < vm.topicoMestre[i].TopicosInteressantes.length; j++) {

                for (var x = 0; x < vm.Usuario.TopicosInteressesMestre.length; x++) {
                    for (var y = 0; y < vm.Usuario.TopicosInteressesMestre[x].TopicosInteressantes.length; y++) {

                        if (vm.topicoMestre[i].TopicosInteressantes[j].IdTopicosInteressantes === vm.Usuario.TopicosInteressesMestre[x].TopicosInteressantes[y].IdTopicosInteressantes) {
                            vm.marcarCheck(vm.topicoMestre[i].TopicosInteressantes[j]);
                        }
                    }
                }
            }
        }
    }

    vm.marcarCheck = function (item) {
        if (item.checked) {
            item.checked = false;
            return;
        }
        item.checked = true;
    };

    vm.validarNomeUsuario = function () {
        vm.nomeValido = Util.validarNomeUsuario(vm.Usuario.Nome);
    };

    vm.validarEmailUsuario = function () {
        vm.emailValido = Util.validarEmailUsuario(vm.Usuario.Email);
    };

    vm.validarSenhaUsuario = function () {
        vm.senhaValida = Util.validarSenhaUsuario(vm.Usuario.Senha);
    };

    vm.validarSenhaAntigaUsuario = function () {
        vm.senhaAntigaValida = Util.validarSenhaUsuario(vm.Usuario.SenhaAntiga);
    };

    vm.validarProfisssaoUsuario = function () {
        vm.profissaoValida = Util.validarProfisssaoUsuario(vm.Usuario.Profissao);
    };

    vm.salvarAlteracoesUsuario = function () {
        var topicos = [];
        vm.validarNomeUsuario();
        vm.validarEmailUsuario();
        vm.validarProfisssaoUsuario();

        if (vm.Usuario.Senha || vm.Usuario.SenhaAntiga) {
            vm.validarSenhaUsuario();
            vm.validarSenhaAntigaUsuario();
        }

        if (vm.nomeValido && vm.emailValido && vm.senhaValida && vm.senhaAntigaValida && vm.profissaoValida) {
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
                Id: vm.Usuario.Id,
                Nome: vm.Usuario.Nome,
                Email: vm.Usuario.Email,
                Senha: vm.Usuario.Senha,
                SenhaAntiga: vm.Usuario.SenhaAntiga,
                Profissao: vm.Usuario.Profissao,
                TopicosInteressesMestre: topicos
            };
            EditarPerfilService.realizarCadastro(param, realizarCadastroSucessoCallback, realizarCadastroErroCallback);
        }
    };

    function realizarCadastroSucessoCallback(data) {
        EventosFactory.mensagemDeSucesso($scope, "Alteração de perfil realizada com sucesso!");
        EditarPerfilService.buscarUsuarioPorId(data, buscarUsuarioPorIdSucessoCallback);
    }

    function realizarCadastroErroCallback(mensagem) {
        EventosFactory.mensagemDeErro($scope, mensagem);
    }

    function buscarUsuarioPorIdSucessoCallback(data) {
        TccenterStorage.salvarUsuario(data);
        $rootScope.$emit('atualizarInfoPerfil');
    }

    AtalhosFactory.criarTelaDeAtalhosGenericos($scope);

});