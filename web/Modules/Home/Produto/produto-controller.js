angular.module("balcao.produto").controller("produtoController", function ($scope, $rootScope, $timeout, ProdutoService, ElementoAtivoFactory, EventosFactory, BalcaoStorage, Util, BalcaoEnum, AtalhosFactory, NavegacaoTabela) {
    var vm = this;
    var lo = new LeitorOptico();
    AtalhosFactory.criarTelaDeAtalhosGenericos($scope);

    vm.produtos = [];
    vm.codigoProdutoSelecionado = '';
    vm.textoBusca = '';
    vm.loading = false;
    vm.textoBotaoEquivalentes = "Equivalentes (F3)";

    $scope.viewModel = vm;

    $scope.$watch('viewModel.produtoEquivalente', function () {
        definirAlturaTabela();
    });

    var scrollGridPrincipal = 0;
    var produtosGridPrincipal = [];

    vm.ctrlInit = function () {
        $timeout(function () {
            if (document.getElementById('input-busca-produtos')){
                AtalhosFactory.iniciarAtalhosDaTela($scope);
                document.getElementById('input-busca-produtos').focus();
                NavegacaoTabela.criarTabelaNavegavel('#resultado-busca', '#input-busca-produtos', vm.produtos, atualizarItemSelecionado, 'Codigo', $scope);
            }
            else {
                vm.ctrlInit();
            }
        },100);
    };

    /*Itens para tabela navegavel*/
    vm.selecionarLinha = function (produto) {
        NavegacaoTabela.selecionarLinha('#resultado-busca', produto);
    }

    vm.desselecionarLinha = function () {
        NavegacaoTabela.desselecionarLinha('#resultado-busca');
    }

    function atualizarItemSelecionado(item) {
        vm.codigoProdutoSelecionado = Util.gerarCodigoCompleto(item);
    }

    /*Acesso a serviços*/

    vm.buscarProdutos = function (bipado, value) {
        if (!bipado && vm.textoBusca.length < 2) {
            mensagemDeAlerta("Para realizar a busca, digite 2 ou mais caracteres");
            return;
        }
        vm.bipado = bipado;
        vm.produtos = [];
        vm.produtoEquivalente = null;
        vm.textoBotaoEquivalentes = "Equivalentes (F3)";
        vm.codigoProdutoSelecionado = '';

        vm.loading = true;

        var estacao = BalcaoStorage.obterDadosEstacao();
        if (estacao == null) {
            mensagemDeErro("Não foi possível obter a filial");
            vm.loading = false;
        }
        else {
            var param = {
                busca: bipado ? value : vm.textoBusca,
                filial: estacao != null ? estacao.Filial : 1,
                sucessCallbackParameters: {
                    bipado: bipado,
                    value: value
                }
            };
            ProdutoService.pesquisa(param, buscarProdutosSucessoCallback, buscarProdutosErroCallback);
        }
    }

    function exibirProdutos(data, bipado, valorBipado) {
        vm.produtos = data;
        vm.codigoProdutoSelecionado = '';
        $timeout(function () {
            var lista_produtos_navegacao = [];
            if(vm.produtoEquivalente) lista_produtos_navegacao.push(vm.produtoEquivalente);
            for (var i = 0; i < vm.produtos.length; i++) {
                lista_produtos_navegacao.push(vm.produtos[i]);
            }
            NavegacaoTabela.atualizarElementosTabelaNavegavel('#resultado-busca', lista_produtos_navegacao, atualizarItemSelecionado, vm.produtoEquivalente);
            if (bipado || vm.produtoEquivalente) {
                vm.selecionarLinha(lista_produtos_navegacao[0]);
                document.getElementById('input-busca-produtos').blur();
                document.getElementById('resultado-busca').focus();
            }
            else {
                document.getElementById('input-busca-produtos').focus();
            }
            definirLarguraTabela();
        });
        vm.loading = false;
    }

    function buscarProdutosSucessoCallback(dados, parametros) {
        if (dados && dados.Data && dados.Data.length > 0) {
            if (parametros && parametros.bipado) {
                var bipado = parametros.bipado;
                var value = parametros.value;
            }
            exibirProdutos(dados.Data, bipado, value);
        }
        else {
            mensagemDeAlerta("Nenhum produto encontrado na pesquisa");
            vm.loading = false;
        }

        $scope.$emit('bancoIndisponivel', false);
    }

    function buscarProdutosErroCallback(mensagem, parametros) {
        vm.ativarBuscaProdutos();
        mensagemDeErro(mensagem);
        vm.loading = false;        
    }

    vm.obterEquivalentes = function () {
        if (vm.produtoEquivalente == null && vm.codigoProdutoSelecionado) {
            var estacao = BalcaoStorage.obterDadosEstacao();
            if (estacao == null) {
                mensagemDeErro("Não foi possível obter a filial");
                return;
            }
            produtosGridPrincipal = vm.produtos.slice();
            scrollGridPrincipal = document.querySelector('#resultado-busca tbody').scrollTop;
            vm.produtoEquivalente = obterProdutoSelecionado(vm.codigoProdutoSelecionado);
            definirLarguraTabela(); 
            vm.produtos = [];
            var param = {
                produto: Util.gerarCodigoCompleto(vm.produtoEquivalente),
                filial: estacao != null ? estacao.Filial : 1
            };
            vm.loading = true;
            ProdutoService.equivalentes(param, obterEquivalentesSucessoCallback, obterEquivalentesErroCallback);
            vm.textoBotaoEquivalentes = "Todos (F3)";
        }
        else if (vm.produtoEquivalente) {
            ProdutoService.cancelarRequisicoes();
            exibirPesquisaDeProdutos();
            $timeout(function () {
                document.querySelector('#resultado-busca tbody').scrollTop = scrollGridPrincipal;
                document.getElementById('resultado-busca').focus();
            });
            vm.loading = false;
        }
    }

    function obterEquivalentesSucessoCallback(dados) {
        vm.loading = false;
        if (dados && dados.Data && dados.Data.length > 0) {
            exibirProdutos(dados.Data, false);
            document.querySelector('#resultado-busca tbody').scrollTop = 0;
            vm.codigoProdutoSelecionado = '';
            vm.textoBotaoEquivalentes = "Todos (F3)";
            vm.selecionarLinha(vm.produtoEquivalente);
        }
        else {
            exibirPesquisaDeProdutos();
            mensagemDeAlerta("Nenhum produto equivalente encontrado");
        }
    }

    function obterEquivalentesErroCallback(mensagem) {
        exibirPesquisaDeProdutos()
        mensagemDeErro(mensagem);
        vm.loading = false;
    }

    function exibirPesquisaDeProdutos() {
        vm.produtos = produtosGridPrincipal.slice();
        vm.textoBotaoEquivalentes = "Equivalentes (F3)";
        vm.selecionarLinha(vm.produtoEquivalente);
        vm.produtoEquivalente = null;
        NavegacaoTabela.atualizarElementosTabelaNavegavel('#resultado-busca', vm.produtos, atualizarItemSelecionado, false);
        $timeout(function () {
            definirLarguraTabela();
            document.querySelector('#resultado-busca tbody').scrollTop = scrollGridPrincipal;
        })
    }

    /**/

    /*Eventos Enviados*/

    vm.adicionarProduto = function () {
        var prod = obterProdutoSelecionado();

        if (vm.bipado) {
            if (prod != undefined && prod.IndicREC) {
                prod.bipado = vm.bipado;
            }
            vm.bipado = false;
        }

        if (prod != undefined) {
            if (prod.IndicExclusivoFP) {
                $timeout(function () {
                    mensagemDeAlerta("Este produto não pode ser incluído no orçamento pois a funcionalidade Farmácia Popular não está disponível no sistema offline");
                });
            }
            else {
                $timeout(function () {
                    $scope.$emit('adicionarProduto', prod);
                });
            }
        }
    }

    vm.adicionarProdutoEspecial = function () {
        mensagemDeAlerta("A funcionalidade Farmácia Popular não está disponível no sistema offline");
        /* TODO: Validar remoção
         * 
         var prod = obterProdutoSelecionado();

        if (vm.bipado) {
            if (prod != undefined && prod.IndicREC) {
                prod.bipado = vm.bipado;
            }
            vm.bipado = false;
        }

        if (prod != undefined) {
            $timeout(function () {
                $scope.$emit('adicionarProdutoEspecial', prod);
            });
        }
        */


    }

    function mensagemDeErro(mensagem) {
        $timeout(function () {
            EventosFactory.mensagemDeErro($scope, mensagem);
        });
    }

    function mensagemDeAlerta(mensagem) {
        $timeout(function () {
            EventosFactory.mensagemDeAlerta($scope, mensagem);
        });
    }

    /**/

    /*Auxiliares de exibição ou conversão de dados*/

    vm.obterPrecoPor = function (produto) {
        var precoPor = 0;

        if (produto.PrecoPor == 0) {
            precoPor = produto.PrecoPor = produto.PrecoDe;
        } else {
            precoPor = produto.PrecoPor;
        }

        return precoPor;
    }

    vm.obterTitleClassificacao = Util.obterTitleClassificacao;

    
    vm.ativarBuscaProdutos = function () {
        $scope.$emit('selecionarContainerPrincipal');
        vm.desselecionarLinha();
        if (document.getElementById('input-busca-produtos')) {
            document.getElementById('input-busca-produtos').select();
            document.getElementById('input-busca-produtos').focus();
        }
    }
    

    function obterProdutoSelecionado() {
        var produtoSelecionado;
        if (vm.produtos != undefined && vm.produtos != null) {
            for (var i = 0; i < vm.produtos.length; i++) {
                if (Util.gerarCodigoCompleto(vm.produtos[i]) == vm.codigoProdutoSelecionado) {
                    produtoSelecionado = vm.produtos[i];
                    break;
                }
            }
        }
        if (!produtoSelecionado && vm.produtoEquivalente != null && Util.gerarCodigoCompleto(vm.produtoEquivalente) == vm.codigoProdutoSelecionado) {
            produtoSelecionado = vm.produtoEquivalente;
        }
        return produtoSelecionado;
    }

    /**/

    /*Eventos Recebidos*/

    var eventoAtivarBuscaProdutos = $rootScope.$on('ativarBuscaProdutos', function (event) {
        vm.ativarBuscaProdutos('#resultado-busca');
    });

    var eventoDesselecionarLinhaTabela = $rootScope.$on('desselecionarLinhaTabela', function (event) {
        vm.desselecionarLinha('#resultado-busca');
    });

    var eventoLimparCampos = $rootScope.$on('limparCampos', function (event) {
        vm.desselecionarLinha('#resultado-busca');
        vm.produtos = [];
        vm.textoBusca = '';
        vm.loading = false;
        vm.produtoEquivalente = null;
        vm.textoBotaoEquivalentes = "Equivalentes (F3)";
        ProdutoService.cancelarRequisicoes();
        $timeout(function () {
            $scope.$emit('selecionarContainerPrincipal');
            ElementoAtivoFactory.focarElemento('input-busca-produtos');
        });
    });

    $scope.$on('$destroy', function (event, param) {
        eventoAtivarBuscaProdutos();
        eventoLimparCampos();
        eventoDesselecionarLinhaTabela();
    });

    /**/

    vm.exibirInformacoes = function (produto) {
        if (produto) {
            if (vm.bipado && produto.IndicREC) {
                produto.bipado = vm.bipado;
                vm.bipado = false;
            }
            var parametros = {
                produto: produto,
                funcaoAdicionarProduto: vm.adicionarProduto,
                funcaoAdicionarProdutoEspecial: vm.adicionarProdutoEspecial
            }
            $timeout(function () {
                EventosFactory.abrirModal($scope, 'Informacao', parametros, 'resultado-busca');
            });
        }
    }

    vm.ehKitModeloA = function (tipoRegraKit) {
        if (tipoRegraKit && (tipoRegraKit == 'RD01' || tipoRegraKit.trim() == 'RD01'))
            return true;
        else
            return false;
    }

    vm.ehKitModeloB = function (tipoRegraKit) {
        if (tipoRegraKit && (tipoRegraKit == 'RD02' || tipoRegraKit == 'RD03' || tipoRegraKit.trim() == 'RD02' || tipoRegraKit.trim() == 'RD03'))
            return true;
        else
            return false;
    }

    function obterTamanhoLinha() {
        var linha = document.querySelector('#resultado-busca tbody tr');
        return Util.isNullUndefinedOrEmpty(linha) ? 0 : linha.offsetHeight;
    }

    /**/

    //Funções de navegação na tabela de produtos
    
    function definirLarguraTabela() {
        var tamanhoScrollbar = document.querySelector('#resultado-busca tbody').offsetWidth - document.querySelector('#resultado-busca tbody').clientWidth;

        if (!vm.tamanhoTabela) {
            vm.tamanhoTabela = {
                'width': 'calc(100% + ' + tamanhoScrollbar + 'px)'
            };
        }
        else {
            vm.tamanhoTabela.width = 'calc(100% + ' + tamanhoScrollbar + 'px)';
        }
    }

    function definirAlturaTabela() {
        var altura = 0;
        if (vm.produtoEquivalente != null) {
            altura = obterTamanhoLinha() + 9;
        }

        if (!vm.tamanhoTabela) {
            definirLarguraTabela();
        }

        vm.tamanhoTabela['max-height'] = 'calc(100vh - 262px - ' + altura + 'px)';
    }

    vm.cliqueTabela = function (event) {
        event.stopPropagation();
        $scope.$emit('selecionarContainerPrincipal');
    }

    /**/

    /*Atalhos do teclado*/

    AtalhosFactory.criarAtalho($scope, ['ctrl', 'B'], function () {
        vm.desselecionarLinha('#resultado-busca');
        $scope.$emit('selecionarContainerPrincipal');
        vm.ativarBuscaProdutos();
    });

    AtalhosFactory.criarAtalho($scope, ['enter'], function () {
    }, '#input-busca-produtos');

    AtalhosFactory.criarAtalho($scope, ['enter'], function () {
        vm.adicionarProduto(); //vm.adicionarProdutoEspecial();
    }, '#resultado-busca');

    AtalhosFactory.criarAtalho($scope, ['ctrl', 'enter'], function () {
        vm.adicionarProduto();
    }, '#resultado-busca');

    AtalhosFactory.criarAtalho($scope, ['F1'], function () {
        vm.exibirInformacoes(obterProdutoSelecionado());
    }, '#resultado-busca');

    AtalhosFactory.criarAtalho($scope, ['F3'], function () {
        vm.obterEquivalentes();
    });

    AtalhosFactory.iniciarAtalhosDaTela($scope);
    /**/

});