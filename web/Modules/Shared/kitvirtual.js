(function () {
    'use strict';

    angular.module("balcao").factory('KitVirtualFactory', function EventosFactory($timeout, $rootScope, ElementoAtivoFactory) {

        var organizarProdutosPorId = function (produtos) {
            var produtosPorId = [];

            for (var idxProOri = 0; idxProOri < produtos.length; idxProOri++) {
                delete produtos[idxProOri].kit;

                produtosPorId[produtos[idxProOri].Codigo] = {
                    Codigo: produtos[idxProOri].Codigo,
                    Digito: produtos[idxProOri].Digito,
                    DescricaoResumida: produtos[idxProOri].DescricaoResumida,
                    Classificacao: produtos[idxProOri].Classificacao,
                    IndicUsoContinuo: produtos[idxProOri].IndicUsoContinuo,
                    IndicGeladeira: produtos[idxProOri].IndicGeladeira,
                    Fabricante: produtos[idxProOri].Fabricante,
                    IndicREC: produtos[idxProOri].IndicREC,
                    PrecoDe: produtos[idxProOri].PrecoDe,
                    Preco: produtos[idxProOri].PrecoPor,
                    PercentualSubsidio: produtos[idxProOri].PercentualSubsidio,
                    preAutorizado: produtos[idxProOri].preAutorizado,
                    TipoEmbalagem: produtos[idxProOri].TipoEmbalagem
                };
            }

            return produtosPorId;
        }

        var precoKitMelhorPrecoOriginal = function (produtosOriginaisPorId, listaProduto) {
            var precoForaKit = 0;
            var precoDentroKit = 0;

            for (var idxProdKit = 0; idxProdKit < listaProduto.length; idxProdKit++) {
                precoForaKit += produtosOriginaisPorId[listaProduto[idxProdKit].CodigoProduto].Preco;
                precoDentroKit += listaProduto[idxProdKit].Valor;
            }

            return (precoDentroKit < precoForaKit);
        }

        var adicionarKitEmProdutosOriginais = function (produtos, produtosDoKit, produtosOriginaisPorId) {
            for (var idxProd = 0; idxProd < produtos.length; idxProd++) {

                if (typeof (produtosOriginaisPorId[produtos[idxProd].Codigo].kit) !== 'undefined') {
                    produtos[idxProd].kit = produtosOriginaisPorId[produtos[idxProd].Codigo].kit;

                    produtos[idxProd].quantidadeForaKit = parseInt(produtos[idxProd].quantidade) - produtos[idxProd].kit.length;
                    produtos[idxProd].exibirLinhaProduto = produtos[idxProd].quantidadeForaKit != 0;
                    produtos[idxProd].quantidadeEmKit = produtos[idxProd].kit.length;
                }
                else {
                    delete produtos[idxProd].kit;

                    produtos[idxProd].quantidadeForaKit = parseInt(produtos[idxProd].quantidade);
                    produtos[idxProd].exibirLinhaProduto = produtos[idxProd].quantidadeForaKit != 0;
                    produtos[idxProd].quantidadeEmKit = 0;
                }
            }

            return produtos;
        }

        return {
            preparar: function (dadosKit, produtos) {
                var produtosDoKit = [],
                    kitVirtual = [];

                if (dadosKit != null) {
                    var produtosOriginaisPorId = organizarProdutosPorId(produtos);

                    for (var idxKit = 0; idxKit < dadosKit.length; idxKit++) {
                        var kit = dadosKit[idxKit];

                        kitVirtual.push({
                            codigo: kit.CodigoKit,
                            titulo: kit.TituloKit,
                            descricao: kit.DescricaoKit,
                            quantidade: kit.ListaProdutos.length,
                            precoTotal: 0,
                            precoTotalOriginal: 0,
                            precoTotalSubsidio: 0,
                            produtos: []
                        });

                        for (var idxListaProdKit = 0; idxListaProdKit < kit.ListaProdutos.length; idxListaProdKit++) {
                            var listaProduto = kit.ListaProdutos[idxListaProdKit];

                            if (precoKitMelhorPrecoOriginal(produtosOriginaisPorId, listaProduto)) {
                                for (var idxProdKit = 0; idxProdKit < listaProduto.length; idxProdKit++) {
                                    var prod = listaProduto[idxProdKit];

                                    kitVirtual[idxKit].precoTotal += prod.Valor;
                                    kitVirtual[idxKit].precoTotalOriginal += produtosOriginaisPorId[prod.CodigoProduto].PrecoDe;
                                    kitVirtual[idxKit].precoTotalSubsidio += produtosOriginaisPorId[prod.CodigoProduto].PercentualSubsidio && produtosOriginaisPorId[prod.CodigoProduto].PercentualSubsidio > 0 ?
                                        parseFloat((prod.Valor * (produtosOriginaisPorId[prod.CodigoProduto].PercentualSubsidio / 100)).toFixed(2)) : 0;

                                    if (typeof (produtosDoKit[prod.CodigoProduto]) === 'undefined') {
                                        produtosDoKit[prod.CodigoProduto] = {
                                            CodigoKit: kit.CodigoKit,
                                            CodigoProduto: prod.CodigoProduto,
                                            Quantidade: prod.Quantidade,
                                            Valor: prod.Valor
                                        };

                                        kitVirtual[idxKit].produtos.push({
                                            Codigo: produtosOriginaisPorId[prod.CodigoProduto].Codigo,
                                            Digito: produtosOriginaisPorId[prod.CodigoProduto].Digito,
                                            DescricaoResumida: produtosOriginaisPorId[prod.CodigoProduto].DescricaoResumida,
                                            Classificacao: produtosOriginaisPorId[prod.CodigoProduto].Classificacao,
                                            IndicUsoContinuo: produtosOriginaisPorId[prod.CodigoProduto].IndicUsoContinuo,
                                            IndicGeladeira: produtosOriginaisPorId[prod.CodigoProduto].IndicGeladeira,
                                            Fabricante: produtosOriginaisPorId[prod.CodigoProduto].Fabricante,
                                            IndicREC: produtosOriginaisPorId[prod.CodigoProduto].IndicREC,
                                            Preco: prod.Valor,
                                            quantidadeEmKit: prod.Quantidade,
                                            preAutorizado: produtosOriginaisPorId[prod.CodigoProduto].preAutorizado,
                                            TipoEmbalagem: produtosOriginaisPorId[prod.CodigoProduto].TipoEmbalagem
                                        });

                                    }
                                    else {
                                        produtosDoKit[prod.CodigoProduto].Quantidade += prod.Quantidade;
                                        produtosDoKit[prod.CodigoProduto].Valor += prod.Valor;

                                        for (var i = 0; i < kitVirtual[idxKit].produtos.length; i++) {
                                            if (prod.CodigoProduto == kitVirtual[idxKit].produtos[i].Codigo) {
                                                kitVirtual[idxKit].produtos[i].Preco += prod.Valor;
                                                kitVirtual[idxKit].produtos[i].quantidadeEmKit += prod.Quantidade;
                                            }
                                        }
                                    }

                                    if (typeof (produtosOriginaisPorId[prod.CodigoProduto].kit) === 'undefined') {
                                        produtosOriginaisPorId[prod.CodigoProduto].kit = [];
                                    }

                                    produtosOriginaisPorId[prod.CodigoProduto].kit.push({
                                        codigoKit: kit.CodigoKit,
                                        sequencial: prod.Sequencial,
                                        valor: prod.Valor
                                    });
                                }
                            }
                        }
                    }

                    produtos = adicionarKitEmProdutosOriginais(produtos, produtosDoKit, produtosOriginaisPorId);
                }
                return {
                    kitVirtual: kitVirtual,
                    produtos: produtos
                }

            },
        };

    });
}());