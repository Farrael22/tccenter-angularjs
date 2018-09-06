angular.module("balcao.orcamento").controller("OrcamentoController", function ($scope, $rootScope, $location, $routeParams, $timeout, Util, ElementoAtivoFactory, EventosFactory, BalcaoStorage, BalcaoEnum, OrcamentoService, AtalhosFactory, KitVirtualFactory) {
    var vm = this;
    AtalhosFactory.criarTelaDeAtalhosGenericos($scope);
    vm.orcamento = [];
    var times = [];
    vm.consultarKit = true;
    vm.totalizadores = {
        subtotal: 0.0,
        desconto: 0.0,
        total: 0.0
    }

    vm.init = function () {
        vm.orcamento = BalcaoStorage.obterOrcamento();
        AtalhosFactory.iniciarAtalhosDaTela($scope);
        configurarOrcamentoProdutosSelecionados(vm.orcamento);
    }

    vm.calcularPrecoProduto = function (prod) {
        return prod.quantidadeForaKit * prod.PrecoPor
    }

    /*Eventos recebidos*/

    var eventoLimparOrcamento = $rootScope.$on('limparOrcamento', function (event) {
        limparOrcamento();
    });

    var eventoAdicionarProduto = $rootScope.$on('adicionarProduto', function (event, prod) {
        incluirOrcamento(angular.copy(prod), false, false);

    });

    var eventoAdicionarProdutoEspecial = $rootScope.$on('adicionarProdutoEspecial', function (event, prod) {
        if (prod.IndicFP /*|| prod.IndicPBM*/)
            incluirOrcamento(angular.copy(prod), true, false);
        else
            incluirOrcamento(angular.copy(prod), false, false);
    });

    $scope.$on('$destroy', function (event, param) {
        eventoAdicionarProduto();
        eventoAdicionarProdutoEspecial();
        eventoLimparOrcamento();
    });

    /**/
    /*Eventos enviados*/

    function fecharModalLoading() {
        $scope.$emit('fecharModalLoading')
    }

    vm.mensagemDeErro = function (mensagem, elemento) {
        EventosFactory.mensagemDeErro($scope, mensagem, elemento);
    }

    vm.desselecionarLinhaTabela = function () {
        $scope.$emit('desselecionarLinhaTabela');
    }

    vm.limparTela = function () {
        limparOrcamento();
        $scope.$emit('limparCampos');
    }

    function limparOrcamento() {
        vm.orcamento = [];
        vm.KitVirtual = [];
        calcularTotalizadoresProdutosSelecionados();
        BalcaoStorage.removerOrcamento();
    }

    vm.confirmarLimparTela = function () {
        var config = {
            botoes: [
                {
                    texto: "Cancelar (ESC)",
                    callback: function () {
                        $scope.$emit('fecharModalAlertaErro');
                    }
                },
                {
                    texto: "Limpar (ENTER)",
                    callback: function () {
                        vm.limparTela();
                        $scope.$emit('fecharModalAlertaErro');
                    }
                },
            ],
            esc: function () {
                $scope.$emit('fecharModalAlertaErro');
            },
            enter: function () {
                vm.limparTela();
                $scope.$emit('fecharModalAlertaErro');
            }
        };

        EventosFactory.mensagemDeAlerta($scope, "Deseja limpar todos os dados?", null, config)
    }



    /*Funcionalidades do grid de orçamentos*/

    function localizarIndexItemOrcamento(codigo, especial) {
        var idxProd = -1;
        for (var i = 0; i < vm.orcamento.length; i++) {
            if (codigo == Util.gerarCodigoCompleto(vm.orcamento[i]) && vm.orcamento[i].especial == especial) {
                idxProd = i;
            }
        }
        return idxProd;
    }

    function destacarItemOrcamento(idx) {
        setTimeout(function () {
            if (document.querySelector('#container-grid-orcamento table tr:nth-child(' + (idx + 1) + ')'))
                document.querySelector('#container-grid-orcamento table tr:nth-child(' + (idx + 1) + ')').classList.add('destaque-inclusao');
        });

        if (times[idx]) {
            clearTimeout(times[idx]);
        }

        times[idx] = setTimeout(function () {
            if (document.querySelector('#container-grid-orcamento table tr:nth-child(' + (idx + 1) + ')'))
                document.querySelector('#container-grid-orcamento table tr:nth-child(' + (idx + 1) + ')').classList.remove('destaque-inclusao');
        }, 2000);
    }

    function selecionarProximoItem(item) {
        var next = item.parentElement.parentElement.parentElement.nextElementSibling;

        if (next != null)
            next.getElementsByClassName('quantidade-produto')[0].select();
    }

    vm.removerOrcamento = function (item, $event) {
        vm.consultarKit = true;
        var idx = localizarIndexItemOrcamento(Util.gerarCodigoCompleto(item), item.especial);

        if (!Util.isNullUndefinedOrEmpty(vm.orcamento[idx].kit) && vm.orcamento[idx].quantidadeEmKit > 0) {
            vm.orcamento[idx].exibirLinhaProduto = false;
            $timeout(function () {
                vm.orcamento[idx].quantidadeForaKit = 0;
                vm.atualizarQuantidadeOrcamento(vm.orcamento[idx]);
            });
        }
        else {
            vm.orcamento.splice(idx, 1);

            if (vm.orcamento.length == 0) {
                $scope.$emit('ativarBuscaProdutos');
                if ($event) $event.stopPropagation();
            }

            calcularTotalizadoresProdutosSelecionados();
        }
    }

    /*Auxiliares de exibição ou conversão de dados*/

    function obterQuantidadeEspecialProduto(prod) {
        var idx = localizarIndexItemOrcamento(Util.gerarCodigoCompleto(prod), !prod.especial);
        var qtdEspecial = 0;
        if (idx >= 0 && !prod.especial) {
            qtdEspecial = Util.calcularQuantidadeFP(vm.orcamento[idx]);
        }
        else if (idx >= 0) {
            qtdEspecial = vm.orcamento[idx].quantidade;
        }
        return qtdEspecial;
    }

    vm.permitirSomenteNumeros = function (item, ehKit) {
        if (ehKit) {
            item.quantidadeEmKit = Util.apenasNumeros(item.quantidadeEmKit);
        }
        item.quantidadeForaKit = Util.apenasNumeros(item.quantidadeForaKit);
    }

    vm.naoPermitirQuantidadeInvalida = function (prod, ehKit) {
        if (ehKit) {
            return Util.naoPermitirQuantidadeNulaELimitarQuantidadeMaxima(prod.quantidadeEmKit);
        }
        else if (prod.exibirLinhaProduto) {
            return Util.naoPermitirQuantidadeNulaELimitarQuantidadeMaxima(prod.quantidadeForaKit);
        }
        return prod.quantidadeForaKit;
    };

    vm.verificarValorQuantidadeFP = function (quantidade) {
        if (isNaN(quantidade)) {
            return "";
        }
        else
            return quantidade
    }

    vm.selecionarItemOrcamento = function (event) {
        event.currentTarget.querySelector(".quantidade-produto").select();
    }

    /* Funcionalidades Kit Virtual */
    function configurarOrcamentoProdutosSelecionados(produtos) {
        vm.KitVirtual = [];
        for (var i = 0; i < produtos.length; i++) {
            produtos[i].quantidadeForaKit = parseInt(produtos[i].quantidade);
            produtos[i].exibirLinhaProduto = true;
            produtos[i].quantidadeEmKit = 0;
            produtos[i].indicConvenioMelhorQueKit = false;
        }
        consultarKitVirtual(vm.orcamento);
    }

    function consultarKitVirtual(produtos) {
        var prod = [];
        for (var i = 0; i < produtos.length; i++) {
            prod.push({
                CodigoProduto: produtos[i].Codigo,
                Quantidade: produtos[i].quantidade
            });
        }
        if (prod.length > 0) {
            OrcamentoService.consultarKitVirtual(prod, consultarKitVirtualSucessoCallback, consultarKitVirtualErroCallback);
        }
    }

    function consultarKitVirtualSucessoCallback(dados) {
        if (dados && dados.Data && dados.Data.length > 0) {
            var data = KitVirtualFactory.preparar(dados.Data, vm.orcamento);

            vm.KitVirtual = data.kitVirtual;
            vm.orcamento = data.produtos;
        }
        calcularTotalizadoresProdutosSelecionados();
        criarAtalhosProdutosSelecionados();
    }

    function consultarKitVirtualErroCallback(mensagem) {
        calcularTotalizadoresProdutosSelecionados();
        criarAtalhosProdutosSelecionados();
    }

    function calcularTotalizadoresProdutosSelecionados() {
        vm.totalizadores = {
            subtotal: 0,
            desconto: 0,
            total: 0
        }

        for (var idxKit = 0; idxKit < vm.KitVirtual.length; idxKit++) {
            vm.totalizadores.subtotal += vm.KitVirtual[idxKit].precoTotalOriginal;
            vm.totalizadores.total += vm.KitVirtual[idxKit].precoTotal;
        }

        for (var idxProd = 0; idxProd < vm.orcamento.length; idxProd++) {
            if (vm.orcamento[idxProd].exibirLinhaProduto || vm.KitVirtual.length == 0) {
                var quantidade = (vm.KitVirtual.length == 0 ? vm.orcamento[idxProd].quantidade : vm.orcamento[idxProd].quantidadeForaKit);

                vm.totalizadores.subtotal += vm.orcamento[idxProd].PrecoDe * quantidade;
                vm.totalizadores.total += vm.calcularPrecoProduto(vm.orcamento[idxProd]);
            }
        }

        vm.totalizadores.desconto = vm.totalizadores.subtotal - vm.totalizadores.total;
        BalcaoStorage.salvarOrcamento(vm.orcamento);
    }

    vm.excluirProdutoPeloProdutoKit = function (produtoKit) {
        vm.consultarKit = true;
        var idxProdOriginal = idxProdutoPeloProdutoKit(produtoKit);
        vm.orcamento[idxProdOriginal].quantidadeEmKit = 0;
        vm.atualizarQuantidadeOrcamento(vm.orcamento[idxProdOriginal]);
    }

    function idxProdutoPeloProdutoKit(produtoKit) {
        var idxProdOriginal = -1;
        for (var idxProd = 0; idxProd < vm.orcamento.length; idxProd++) {
            if (vm.orcamento[idxProd].Codigo == produtoKit.Codigo && vm.orcamento[idxProd].Digito == produtoKit.Digito) {
                idxProdOriginal = idxProd;
                break;
            }
        }
        return idxProdOriginal;
    }

    vm.atualizarQuantidadeOrcamentoPeloProdutoKit = function (produtoKit) {
        var idxProdOriginal = idxProdutoPeloProdutoKit(produtoKit);

        if (idxProdOriginal >= 0) {
            vm.orcamento[idxProdOriginal].quantidadeEmKit = parseInt(produtoKit.quantidadeEmKit);
        }

        vm.atualizarQuantidadeOrcamento(vm.orcamento[idxProdOriginal]);
    }
    /* Fim Kit Virtual */

    vm.atualizarQuantidadeOrcamento = function (produto) {
        var idxProdOriginal = idxProdutoPeloProdutoKit(produto);
        vm.consultarKit = true;

        if (vm.orcamento[idxProdOriginal].quantidade == vm.orcamento[idxProdOriginal].quantidadeEmKit + vm.orcamento[idxProdOriginal].quantidadeForaKit) {
            vm.consultarKit = false;
        }
        produto.quantidade = parseInt(produto.quantidadeEmKit) + parseInt(produto.quantidadeForaKit);

        if (produto.quantidade === 0) {
            vm.orcamento.splice(idxProdutoPeloProdutoKit(produto), 1);
        }

        for (var i = 0; i < vm.orcamento.length; i++) {
            if (vm.consultarKit) {
                vm.orcamento[i].exibirLinhaProduto = true;
                vm.orcamento[i].quantidadeForaKit = vm.orcamento[i].quantidade;
                vm.orcamento[i].quantidadeEmKit = 0;
            }
        }

        if (vm.consultarKit) {
            vm.KitVirtual = [];
            consultarKitVirtual(vm.orcamento);
        }
    }

    function incluirOrcamento(prod, especial) {
        var idx = localizarIndexItemOrcamento(Util.gerarCodigoCompleto(prod), especial);

        var qtdAdicionada = prod.quantidade ? prod.quantidade : 1;
        var qtdFinal = (idx >= 0 && vm.orcamento[idx].quantidade != null ?
            vm.orcamento[idx].quantidade + qtdAdicionada : qtdAdicionada);

        prod.quantidade = qtdFinal;
        prod.especial = especial;

        if (idx >= 0) {
            vm.orcamento[idx].quantidade = qtdFinal;
            if (document.getElementById('container-grid-orcamento') && document.querySelector('#container-grid-orcamento table tr:nth-child(' + (idx + 1) + ')')) {
                document.getElementById('container-grid-orcamento').scrollTop = document.querySelector('#container-grid-orcamento table tr:nth-child(' + (idx + 1) + ')').offsetTop
            }
        }
        else {
            idx = vm.orcamento.push(prod) - 1;
            $timeout(function () {
                document.getElementById('container-grid-orcamento').scrollTop = document.querySelector('#container-grid-orcamento table').offsetHeight + 100;
            });
        }

        configurarOrcamentoProdutosSelecionados(vm.orcamento);

        destacarItemOrcamento(idx >= 0 ? idx : localizarIndexItemOrcamento(Util.gerarCodigoCompleto(prod), especial));

        $timeout(function () {
            $scope.$apply();
            this.orcamento = vm;
        });
    }

    /*Atalhos do teclado*/
    function criarAtalhosProdutosSelecionados() {
        AtalhosFactory.criarAtalho($scope, ['ctrl', 'del'], function () {
            var item = document.activeElement;

            if (item && item.className.indexOf('quantidade-produto') >= 0) {
                var tr = item.parentElement.parentElement.parentElement,
                    next = tr.nextElementSibling;

                if (next != null) {
                    tr.getElementsByClassName('quantidade-produto')[0].select();
                } else {
                    var previus = tr.previousElementSibling;

                    if (previus != null) {
                        previus.getElementsByClassName('quantidade-produto')[0].select();
                    }
                }
                if (item.id.indexOf('kit') > 0) {
                    vm.excluirProdutoPeloProdutoKit(JSON.parse(tr.querySelector('input[type=hidden]').value));
                }
                else {
                    vm.removerOrcamento(JSON.parse(tr.querySelector('input[type=hidden]').value));
                }
            }
        }, '.quantidade-produto');

        AtalhosFactory.criarAtalho($scope, ['enter'], function () {
            var produto = document.activeElement;
            var focarProximo = false;

            for (var i = 0; i < vm.orcamento.length; i++) {
                if (focarProximo && vm.orcamento[i].exibirLinhaProduto == true) {
                    document.getElementById('quantidade-produto_' + vm.orcamento[i].Codigo).focus();
                    document.getElementById('quantidade-produto_' + vm.orcamento[i].Codigo).select();
                    return;
                }

                if (vm.orcamento[i].Codigo == produto.id.split('quantidade-produto_')[1]) {
                    focarProximo = true;
                }
            }

            for (var i = 0; i < vm.KitVirtual.length; i++) {
                for (var j = 0; j < vm.KitVirtual[i].produtos.length; j++) {
                    if (focarProximo) {
                        document.getElementById('quantidade-produto-kit_' + vm.KitVirtual[i].produtos[j].Codigo).focus();
                        document.getElementById('quantidade-produto-kit_' + vm.KitVirtual[i].produtos[j].Codigo).select();
                        return;
                    }

                    if (vm.KitVirtual[i].produtos[j].Codigo == produto.id.split('quantidade-produto-kit_')[1]) {
                        focarProximo = true;
                    }
                }
            }
        }, '.quantidade-produto');
    }
    AtalhosFactory.criarAtalho($scope, ['ctrl', 'O'], function () {
        var produto = document.activeElement;

        for (var i = 0; i < vm.orcamento.length; i++) {
            if (vm.orcamento[i].exibirLinhaProduto == true) {
                document.getElementById('quantidade-produto_' + vm.orcamento[i].Codigo).focus();
                document.getElementById('quantidade-produto_' + vm.orcamento[i].Codigo).select();
                document.getElementById('container-grid-orcamento').scrollTop = 0;
                $scope.$emit('selecionarContainerOrcamento');
                return;
            }
        }

        for (var i = 0; i < vm.KitVirtual.length; i++) {
            for (var j = 0; j < vm.KitVirtual[i].produtos.length; j++) {
                document.getElementById('quantidade-produto-kit_' + vm.KitVirtual[i].produtos[j].Codigo).focus();
                document.getElementById('quantidade-produto-kit_' + vm.KitVirtual[i].produtos[j].Codigo).select();
                document.getElementById('container-grid-orcamento').scrollTop = 0;
                $scope.$emit('selecionarContainerOrcamento');
                return;
            }
        }
    });

    AtalhosFactory.criarAtalho($scope, ['F12'], function () {
        vm.confirmarLimparTela();
    });

    criarAtalhosProdutosSelecionados();
    /**/
});