angular.module("tccenter.home").controller("HomeController", function ($scope, $rootScope, $timeout, $location, HomeService, ElementoAtivoFactory, EventosFactory, TccenterStorage, Util, AtalhosFactory) {

    if (!TccenterStorage.obterUsuario()) {
        $location.path("login");
    }

    var vm = this;
    vm.TopicosInteressantes = [];
    vm.TopicoSelecionado = null;
    vm.tituloPublicacaoValido = true;
    vm.resumoPublicacaoValido = true;
    vm.resultadoPublicacaoValido = true;
    vm.linkPublicacaoValido = true;
    vm.exibirCadastroPublicacao = false;
    vm.CadastroPublicacao = {
        Titulo: null,
        Resumo: null,
        Resultado: null,
        Link: null,
        TopicoSelecionado: null
    };

    AtalhosFactory.criarTelaDeAtalhosGenericos($scope);
    HomeService.buscarTopicosInteressantes(buscarTopicosInteressantesSucesso);

    function buscarTopicosInteressantesSucesso(data) {
        for (var i = 0; i < data.length; i++) {
            for (var j = 0; j < data[i].TopicosInteressantes.length; j++) {
                vm.TopicosInteressantes.push({
                    IdTopicoMestre: data[i].IdTopicoMestre,
                    IdTopicoInteressantes: data[i].TopicosInteressantes[j].IdTopicosInteressantes,
                    DescricaoTopico: data[i].TopicosInteressantes[j].DescricaoTopico
                });
                TccenterStorage.salvarTopicosInteressantes(vm.TopicosInteressantes);
            }
        }
    }

    function validarTituloPublicacao() {
        vm.tituloPublicacaoValido = Util.validarCampoPreenchido(vm.CadastroPublicacao.Titulo);
    };

    function validarResumoPublicacao() {
        vm.resumoPublicacaoValido = Util.validarCampoPreenchido(vm.CadastroPublicacao.Resumo);
    };

    function validarResultadoPublicacao() {
        vm.resultadoPublicacaoValido = Util.validarCampoPreenchido(vm.CadastroPublicacao.Resultado);
    };

    function validarLinkPublicacao() {
        vm.linkPublicacaoValido = Util.validarCampoPreenchido(vm.CadastroPublicacao.Link);
    };

    function validarInteressePublicacao() {
        if (vm.CadastroPublicacao.TopicoSelecionado) {
            return true;
        }
        return false;
    }

    vm.limparFormulario = function () {
        vm.CadastroPublicacao.Titulo = "";
        vm.CadastroPublicacao.Resumo = "";
        vm.CadastroPublicacao.Resultado = "";
        vm.CadastroPublicacao.Link = "";
        vm.CadastroPublicacao.TopicoSelecionado = null;
        vm.tituloPublicacaoValido = true;
        vm.resumoPublicacaoValido = true;
        vm.resultadoPublicacaoValido = true;
        vm.linkPublicacaoValido = true;
    };

    vm.cadastrarPublicacao = function () {
        validarTituloPublicacao();
        validarResumoPublicacao();
        validarResultadoPublicacao();
        validarLinkPublicacao();
        validarInteressePublicacao();

        if (vm.tituloPublicacaoValido && vm.resumoPublicacaoValido && vm.resultadoPublicacaoValido && vm.linkPublicacaoValido && validarInteressePublicacao()) {
            HomeService.cadastrarPublicacao(vm.CadastroPublicacao, cadastrarPublicacaoSucessoCallBack, cadastrarPublicacaoErroCallBack);
        }
    };

    function cadastrarPublicacaoSucessoCallBack() {
        vm.limparFormulario();
        vm.exibirEsconderCadastro();
    }

    function cadastrarPublicacaoErroCallBack(mensagem) {
        mensagemDeErro(mensagem);
    }

    function mensagemDeErro(mensagem) {
        EventosFactory.mensagemDeErro($scope, mensagem);
    }

    vm.exibirEsconderCadastro = function () {
        if (vm.exibirCadastroPublicacao) {
            vm.exibirCadastroPublicacao = false;
        }
        else {
            vm.exibirCadastroPublicacao = true;
        }
    };

    AtalhosFactory.criarAtalho($scope, ['ctrl', 'P'], function () {
        vm.exibirEsconderCadastro();
    });

    AtalhosFactory.iniciarAtalhosDaTela($scope);
});