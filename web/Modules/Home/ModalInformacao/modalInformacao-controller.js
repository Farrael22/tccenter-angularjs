angular.module("balcao.modalInformacao").controller("modalInformacaoController", function ($scope, $rootScope, $timeout, Util, ElementoAtivoFactory, EventosFactory, BalcaoStorage, AtalhosFactory) {
    var vm = this;

    vm.exibirModal = false;
    vm.produto = {};

    vm.funcaoAdicionarProduto = function () { };
    vm.funcaoAdicionarProdutoEspecial = function () { };
    
    var eventoAbrirModalInformacao = $rootScope.$on('abrirModalInformacao', function (event, parametros) {
        vm.produto = parametros.produto;
        vm.funcaoAdicionarProduto = parametros.funcaoAdicionarProduto;
        vm.funcaoAdicionarProdutoEspecial = parametros.funcaoAdicionarProdutoEspecial;
        vm.abrirModal();
    });

    $scope.$on('$destroy', function (event, param) {
        eventoAbrirModalInformacao();
    });

    vm.abrirModal = function () {
        AtalhosFactory.iniciarAtalhosDaTela($scope, true);
        vm.exibirModal = true;
        Util.impedirScroll();
    }

    vm.fecharModal = function () {
        vm.exibirModal = false;
        Util.permitirScroll();
        AtalhosFactory.fecharModal($scope, 'Informacao');
    }

    vm.incluirProduto = function () {
        vm.funcaoAdicionarProduto();
        vm.fecharModal();
    }

    vm.incluirProdutoEspecial = function () {
        vm.funcaoAdicionarProdutoEspecial();
        vm.fecharModal();
    }
    
    vm.obterTitleClassificacao = Util.obterTitleClassificacao;

    vm.obterPrecoPor = function (produto) {
        if (produto && produto.PrecoPor == 0) {
            produto.PrecoPor = produto.PrecoDe;
            return produto.PrecoPor;
        }
        else if (produto) {
            return produto.PrecoPor;
        }
    }

    vm.gerarCodigoCompleto = Util.gerarCodigoCompleto;

    vm.obterTipoReceita = function (produto) {
        if (!produto.TipoReceita) {
            return '';
        }
        var stringTipoReceita = 'Receita '
        if (produto && (produto.IndicFP || produto.IndicAntibiotico || produto.TipoReceita == 'F')) {
            stringTipoReceita += 'C1';
        }
        else if (produto && produto.TipoReceita) {
            stringTipoReceita += produto.TipoReceita;
        }
        return stringTipoReceita;
    }

    AtalhosFactory.criarTelaDeAtalhosGenericos($scope);

    AtalhosFactory.criarAtalho($scope, ['enter'], function () {
        vm.incluirProduto(); //vm.incluirProdutoEspecial();
    });

    AtalhosFactory.criarAtalho($scope, ['ctrl', 'enter'], function () {
        vm.incluirProduto();
    });

    AtalhosFactory.criarAtalho($scope, ['esc'], function () {
        vm.fecharModal();
    });

    AtalhosFactory.criarAtalho($scope, ['tab'], function () { });
});