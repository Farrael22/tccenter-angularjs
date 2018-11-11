angular.module("tccenter.modalCadastroPublicacao").controller("modalCadastroPublicacaoController", function ($scope, $rootScope, $timeout, Util, ElementoAtivoFactory, AtalhosFactory, CadastroPublicacaoService, TccenterStorage, EventosFactory) {
    var vm = this;

    vm.tituloPublicacaoValido = true;
    vm.resumoPublicacaoValido = true;
    vm.resultadoPublicacaoValido = true;
    vm.linkPublicacaoValido = true;
    vm.CadastroPublicacao = {
        IdUsuario: null,
        TituloPublicacao: null,
        DescPublicacao: null,
        ResultadoPublicacao: null,
        LinkPublicacao: null,
        TopicoInteresse: null,
        Orientador: null
    };

    vm.exibirModal = false;

    vm.iniciarCadastroPublicacao = function () {
    };

    var eventoAbrirModalCadastroPublicacao = $rootScope.$on('abrirModalCadastroPublicacao', function (event) {
        vm.abrirModal();
    });

    
    vm.fecharModal = function () {
        vm.exibirModal = false;
        Util.permitirScroll();
        AtalhosFactory.fecharModal($scope, 'CadastroPublicacao');
    };

    vm.abrirModal = function () {
        AtalhosFactory.iniciarAtalhosDaTela($scope, true);
        vm.Usuario = TccenterStorage.obterUsuario();
        vm.TopicosInteressantes = TccenterStorage.obterTopicosInteressantes();
        CadastroPublicacaoService.obterOrientadores(obterOrientadoresSucessoCallback);
        vm.exibirModal = true;
        Util.impedirScroll();
    };

    function obterOrientadoresSucessoCallback(data) {
        vm.Orientadores = data;
    }

    function validarTituloPublicacao() {
        vm.tituloPublicacaoValido = Util.validarCampoPreenchido(vm.CadastroPublicacao.TituloPublicacao);
    }

    function validarResumoPublicacao() {
        vm.resumoPublicacaoValido = Util.validarCampoPreenchido(vm.CadastroPublicacao.DescPublicacao);
    }

    function validarResultadoPublicacao() {
        vm.resultadoPublicacaoValido = Util.validarCampoPreenchido(vm.CadastroPublicacao.ResultadoPublicacao);
    }

    function validarLinkPublicacao() {
        vm.linkPublicacaoValido = Util.validarCampoPreenchido(vm.CadastroPublicacao.LinkPublicacao);
    }

    function validarInteressePublicacao() {
        if (vm.CadastroPublicacao.TopicoInteresse) {
            return true;
        }
        return false;
    }

    function validarOrientadorPublicacao() {
        if (vm.CadastroPublicacao.Orientador) {
            return true;
        }
        return false;
    }

    vm.limparFormulario = function () {
        vm.CadastroPublicacao.TituloPublicacao = "";
        vm.CadastroPublicacao.DescPublicacao = "";
        vm.CadastroPublicacao.ResultadoPublicacao = "";
        vm.CadastroPublicacao.LinkPublicacao = "";
        vm.CadastroPublicacao.TopicoInteresse = null;
        vm.CadastroPublicacao.Orientador = null;
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

        if (vm.tituloPublicacaoValido && vm.resumoPublicacaoValido && vm.resultadoPublicacaoValido &&
            vm.linkPublicacaoValido && validarInteressePublicacao() && validarOrientadorPublicacao()) {
            vm.CadastroPublicacao.IdUsuario = vm.Usuario.Id;
            CadastroPublicacaoService.cadastrarPublicacao(vm.CadastroPublicacao, cadastrarPublicacaoSucessoCallBack, cadastrarPublicacaoErroCallBack);
        }
    };

    function cadastrarPublicacaoSucessoCallBack() {
        vm.limparFormulario();
    }

    function cadastrarPublicacaoErroCallBack(mensagem) {
        mensagemDeErro(mensagem);
    }

    function mensagemDeErro(mensagem) {
        EventosFactory.mensagemDeErro($scope, mensagem);
    }

    AtalhosFactory.criarTelaDeAtalhosGenericos($scope);
    
    vm.enter = vm.fecharModal;
    AtalhosFactory.criarAtalho($scope, ['F1'], function () {
        vm.cadastrarPublicacao();
    });

    vm.esc = vm.fecharModal;
    AtalhosFactory.criarAtalho($scope, ['F2'], function () {
        vm.esc();
    });

    AtalhosFactory.criarAtalho($scope, ['tab'], function () {});
});