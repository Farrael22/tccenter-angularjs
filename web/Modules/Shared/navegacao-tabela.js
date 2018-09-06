(function () {
    'use strict';
    angular.module("balcao").factory('NavegacaoTabela', function NavegacaoTabela($timeout, $rootScope) {
        var tabelasNavegaveis = [];

        return {
            criarTabelaNavegavel: criarTabela,
            selecionarLinha: function (idTabela, elemento) {
                var tabela = obterTabela(idTabela);
                var elemento = obterElementoPorChave(idTabela, elemento[tabela.chave]);
                selecionarLinha(elemento[tabela.chave], idTabela);
                ajustarGridAposClique(idTabela);
            },
            desselecionarLinha: desselecionarLinhaTabela,
            atualizarElementosTabelaNavegavel: atualizarElementos
        };


        function criarTabela(idTabela, campoBusca, listaElementos, funcaoAtualizarItem, chave, scope) {
            var tabelaExistente = obterTabela(idTabela);

            if (tabelaExistente) {
                tabelasNavegaveis.splice(tabelasNavegaveis.indexOf(tabelaExistente), 1);
            }

            tabelasNavegaveis.push({
                idTabela: idTabela,
                elementos: angular.copy(listaElementos),
                chave: chave,
                numeroDeElementos: Math.floor(obterTamanhoTabela(idTabela) / obterTamanhoLinha(idTabela)),
                primeiroElementoVisivel: '',
                ultimoElementoVisivel: '',
                itemSelecionado: '',
                campoBusca: campoBusca,
                scope: scope,
                funcaoAtualizarItem: funcaoAtualizarItem,
                existeLinhaHeaderSemScroll: false
            });

            $timeout(function () {
                var tabela = obterTabela(idTabela);
                tabela.numeroDeElementos = Math.floor(obterTamanhoTabela(idTabela) / obterTamanhoLinha(idTabela));
            })

            criarAtalhosTeclado(idTabela, scope);
        }

        function obterTabela(idTabela) {
            for (var i = 0; i < tabelasNavegaveis.length; i++) {
                if (tabelasNavegaveis[i].idTabela == idTabela) {
                    tabelasNavegaveis[i].Indice = i;
                    return tabelasNavegaveis[i];
                }
            }
            return null;
        }

        function atualizarElementos(idTabela, listaElementos, funcaoAtualizarItem, existeLinhaHeaderSemScroll) {
            var tabela = obterTabela(idTabela);
            tabela.elementos = angular.copy(listaElementos);
            tabela.existeLinhaHeaderSemScroll = existeLinhaHeaderSemScroll ? true : false;
            if (typeof (funcaoAtualizarItem) === 'function')
                tabela.funcaoAtualizarItem = funcaoAtualizarItem;
        }

        function obterElementoPorChave(idTabela, elemento) {
            var tabela = obterTabela(idTabela);
            for (var i = 0; i < tabela.elementos.length; i++) {
                if (tabela.elementos[i][tabela.chave] == elemento) {
                    return tabela.elementos[i];
                }
            }
            return null;
        }

        function obterTamanhoTabela(idTabela) {
            var tamanho = document.querySelector(idTabela + ' table tbody').offsetHeight
            return parseInt(tamanho);
        }

        function obterTamanhoLinha(idTabela) {
            var linha = document.querySelector(idTabela + ' tbody tr:not(.ng-hide)');
            if (linha)
                return parseInt(linha.offsetHeight);
            else
                return 0;
        }

        function atualizarDadosScroll(idTabela) {
            var tabela = obterTabela(idTabela);
            var numeroDeElementos = Math.floor(obterTamanhoTabela(idTabela) / obterTamanhoLinha(idTabela));
            if (isNaN(numeroDeElementos) || numeroDeElementos === Infinity) {
                return;
            }
            var posicaoInicioTabela = document.querySelector(idTabela + ' tbody').scrollTop;
            var posicaoFimTabela = posicaoInicioTabela + obterTamanhoTabela(idTabela);
            var indiceElemento;
            if (posicaoInicioTabela / obterTamanhoLinha(idTabela) == Math.floor(posicaoInicioTabela / obterTamanhoLinha(idTabela))) {
                indiceElemento = Math.floor(posicaoInicioTabela / obterTamanhoLinha(idTabela));
            }
            else {
                indiceElemento = Math.floor(posicaoInicioTabela / obterTamanhoLinha(idTabela)) + 1;
            }

            if (tabela.existeLinhaHeaderSemScroll) {
                indiceElemento = indiceElemento + 1; // Desconsiderar linha fixa no cálculo do scroll
            }

            tabela.numeroDeElementos = numeroDeElementos;
            tabela.primeiroElementoVisivel = tabela.elementos[indiceElemento][tabela.chave];
            tabela.ultimoElementoVisivel =  tabela.elementos[indiceElemento + numeroDeElementos - 1][tabela.chave];
        }

        function selecionarLinha(elemento, idTabela) {
            var tabela = obterTabela(idTabela);
            tabela.itemSelecionado = elemento;
            tabela.funcaoAtualizarItem(obterElementoPorChave(idTabela, elemento));
        }

        function desselecionarLinhaTabela(idTabela) {
            var tabela = obterTabela(idTabela);
            tabela.itemSelecionado = '';
            tabela.funcaoAtualizarItem(obterElementoPorChave(idTabela, ''));
        }

        function ativarCampoBusca(idTabela) {
            var tabela = obterTabela(idTabela);
            if (tabela.campoBusca && tabela.campoBusca != '#idBody') {
                desselecionarLinhaTabela(idTabela);
                document.activeElement.blur();
                document.querySelector(tabela.campoBusca).focus();
            }
        }

        function obterItemAnterior(idTabela) {
            var itemAnterior;
            var tabela = obterTabela(idTabela);
            if (tabela.itemSelecionado == '' || tabela.itemSelecionado == null)
                return itemAnterior;

            for (var i = 0; i < tabela.elementos.length; i++) {
                if (tabela.elementos[i][tabela.chave] == tabela.itemSelecionado) {
                    if (i === 0) {
                        ativarCampoBusca(idTabela);
                    }
                    break;
                }
                itemAnterior = tabela.elementos[i];
            }

            atualizarDadosScroll(idTabela);

            if (tabela.primeiroElementoVisivel == tabela.elementos[i][tabela.chave] && i != 0) {
                if (tabela.existeLinhaHeaderSemScroll) {
                    i--; // Desconsiderar linha fixa no cálculo do scroll
                }
                var posicaoInicioTabela = document.querySelector(idTabela + ' tbody').scrollTop;
                var posicaoInicioElemento = (i - 1) * obterTamanhoLinha(idTabela);
                if (posicaoInicioElemento < posicaoInicioTabela) {
                    document.querySelector(idTabela + ' tbody').scrollTop = posicaoInicioElemento;
                }
            }

            return itemAnterior;
        }

        function obterItemSeguinte(idTabela) {
            var itemSeguinte;
            var tabela = obterTabela(idTabela);
            if (tabela.itemSelecionado == '' || tabela.itemSelecionado == null)
                return itemSeguinte;

            for (var i = 0; i < tabela.elementos.length; i++) {
                if (tabela.elementos[i][tabela.chave] == tabela.itemSelecionado) {
                    break;
                }
            }
            itemSeguinte = tabela.elementos[i + 1];

            atualizarDadosScroll(idTabela);

            if (tabela.ultimoElementoVisivel == tabela.elementos[i][tabela.chave] && i != tabela.elementos.length - 1) {
                if (tabela.existeLinhaHeaderSemScroll) {
                    i--; // Desconsiderar linha fixa no cálculo do scroll
                }
                var posicaoInicioTabela = document.querySelector(idTabela + ' tbody').scrollTop;
                var posicaoFimTabela = posicaoInicioTabela + obterTamanhoTabela(idTabela);
                var posicaoInicioElemento = (i + 1) * obterTamanhoLinha(idTabela);
                var posicaoFimElemento = posicaoInicioElemento + obterTamanhoLinha(idTabela);
                if (posicaoFimElemento > posicaoFimTabela) {
                    document.querySelector(idTabela + ' tbody').scrollTop = posicaoFimElemento - obterTamanhoTabela(idTabela);
                }
            }

            return itemSeguinte;
        }

        function obterPaginaSeguinte(idTabela) {
            var tabela = obterTabela(idTabela);
            if (tabela.itemSelecionado == '' || tabela.itemSelecionado == null)
                return

            atualizarDadosScroll(idTabela);
            var offsetPagina = tabela.numeroDeElementos * obterTamanhoLinha(idTabela);

            var posicaoTabela = document.querySelector(idTabela + ' tbody').scrollTop;
            var tamanhoTotalTabela = document.querySelector(idTabela + ' tbody').scrollHeight;

            var elemento;

            if (posicaoTabela + offsetPagina > tamanhoTotalTabela - obterTamanhoTabela(idTabela)) {
                document.querySelector(idTabela + ' tbody').scrollTop = tamanhoTotalTabela - obterTamanhoTabela(idTabela);
                atualizarDadosScroll(idTabela);
                selecionarLinha(tabela.ultimoElementoVisivel, idTabela);
            }
            else {
                document.querySelector(idTabela + ' tbody').scrollTop = posicaoTabela + offsetPagina;
                atualizarDadosScroll(idTabela);
                selecionarLinha(tabela.primeiroElementoVisivel, idTabela);
            }
        }

        function obterPaginaAnterior(idTabela) {
            var tabela = obterTabela(idTabela);
            if (tabela.itemSelecionado == '' || tabela.itemSelecionado == null)
                return

            atualizarDadosScroll(idTabela);
            var offsetPagina = tabela.numeroDeElementos * obterTamanhoLinha(idTabela);
            var posicaoTabela = document.querySelector(idTabela + ' tbody').scrollTop;

            if (posicaoTabela - offsetPagina > 0) {
                document.querySelector(idTabela + ' tbody').scrollTop = posicaoTabela - offsetPagina;
            }
            else {
                document.querySelector(idTabela + ' tbody').scrollTop = 0;
            }

            atualizarDadosScroll(idTabela);
            selecionarLinha(tabela.primeiroElementoVisivel, idTabela);
        }

        function irParaPrimeiraPagina(idTabela) {
            var tabela = obterTabela(idTabela);
            document.querySelector(idTabela + ' tbody').scrollTop = 0;
            atualizarDadosScroll(idTabela);
            selecionarLinha(tabela.elementos[0][tabela.chave], idTabela);
        }

        function irParaUltimaPagina(idTabela) {
            var tabela = obterTabela(idTabela);
            var tamanhoTotalTabela = document.querySelector(idTabela + ' tbody').scrollHeight;
            document.querySelector(idTabela + ' tbody').scrollTop = tamanhoTotalTabela - obterTamanhoTabela(idTabela);
            atualizarDadosScroll(idTabela);
            selecionarLinha(tabela.elementos[tabela.elementos.length - 1][tabela.chave], idTabela);
        }

        function definirTamanhoTabela(idTabela) {
            tamanhoScrollbar = document.querySelector(idTabela + ' tbody').offsetWidth - document.querySelector(idTabela + ' tbody').clientWidth;
            var tamanhoTabela = {
                'width': 'calc(100% + ' + tamanhoScrollbar + 'px)'
            };
            return tamanhoTabela;
        }

        function ajustarGridAposClique(idTabela) {
            var tabela = obterTabela(idTabela);
            var primeiro = false;
            var ultimo = false;
            for (var i = 0; i < tabela.elementos.length; i++) {
                if (tabela.elementos[i][tabela.chave] == tabela.itemSelecionado) {
                    if (i === 0) primeiro = true;
                    if (i === tabela.elementos.length - 1) ultimo = true;
                    atualizarDadosScroll(idTabela);
                    if (!ultimo && tabela.elementos[i + 1][tabela.chave] == tabela.primeiroElementoVisivel && 
                         tabela.elementos[0][tabela.chave] != tabela.primeiroElementoVisivel) {
                        tabela.itemSelecionado = tabela.elementos[i + 1][tabela.chave];
                        obterItemAnterior(idTabela);
                        tabela.itemSelecionado = tabela.elementos[i][tabela.chave];
                    }
                    if (!primeiro && tabela.elementos[i - 1][tabela.chave] == tabela.ultimoElementoVisivel) {
                        tabela.itemSelecionado = tabela.elementos[i - 1][tabela.chave];
                        obterItemSeguinte(idTabela);
                        tabela.itemSelecionado = tabela.elementos[i][tabela.chave];
                    }
                    break;
                }
            }
        }

        function criarAtalhosTeclado(idTabela, scope) {
            var at = Atalho().init();
            var tabela = obterTabela(idTabela);

            at.criarAtalho(['down'], function () {
                if (document.querySelector(idTabela + ' tbody tr:not(.ng-hide)')) {
                    irParaPrimeiraPagina(idTabela);

                    document.querySelector(tabela.campoBusca).blur();
                    document.querySelector(idTabela).focus();
                }
                scope.$apply();
            }, tabela.campoBusca);

            at.criarAtalho(['up'], function () {
                var item = obterItemAnterior(idTabela);
                if (item != null && item != undefined) {
                    selecionarLinha(item[tabela.chave], idTabela);
                }
                scope.$apply();
            }, idTabela);

            at.criarAtalho(['down'], function () {
                var item = obterItemSeguinte(idTabela);
                if (item != null && item != undefined) {
                    selecionarLinha(item[tabela.chave], idTabela);
                }
                scope.$apply();
            }, idTabela);

            at.criarAtalho(['pgup'], function () {
                obterPaginaAnterior(idTabela);
                scope.$apply();
            }, idTabela);

            at.criarAtalho(['pgdn'], function () {
                obterPaginaSeguinte(idTabela);
                scope.$apply();
            }, idTabela);

            at.criarAtalho(['home'], function () {
                irParaPrimeiraPagina(idTabela);
                scope.$apply();
            }, idTabela);

            at.criarAtalho(['end'], function () {
                irParaUltimaPagina(idTabela);
                scope.$apply();
            }, idTabela);
        }
    });
}());