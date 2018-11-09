(function () {
    'use strict';

    angular.module("tccenter").factory('Util', function Util($timeout) {

        return {
            isNullOrUndefined: isNullOrUndefined,
            isNullUndefinedOrEmpty: isNullUndefinedOrEmpty,            
            apenasNumeros: function (num) {
                if (num) {
                    num = num.replace(/[^0-9]/g, '');
                    num = parseInt(num);
                    return isNaN(num) ? '' : num;
                }
            },
            apenasNumerosDecimais: function (num) {
                if (num) {
                    num = num.replace(/[^0-9|.]/g, '');
                    var removerPonto = false;
                    for (var i = 0; i < num.length; i++) {
                        if (removerPonto && num[i] === '.') {
                            num.slice(i, 1);
                        }
                        if (!removerPonto && num[i] === '.') {
                            removerPonto = true;
                        }
                    }
                    num = parseFloat(num);
                    return isNaN(num) ? '' : num;
                }
            },
            apenasLetras: function (string){
                string = string.replace(/[^A-z^À-ú\s]+/g, '');
                return string;
            },
            apenasLetrasENumeros: function (string){
                string = string.replace(/[^0-9^A-z^À-ú]+/g, '');
                return string;
            },
            naoPermitirQuantidadeNula: naoPermitirQuantidadeNula,
            naoPermitirQuantidadeNulaELimitarQuantidadeMaxima: function (qtdDesejada) {
                var qtd = naoPermitirQuantidadeNula(qtdDesejada);
                return qtd;
            },
            adicionarZeros: adicionarZeros,
            removerAcentos: function (value) {
                if (value) {
                    return value
                        .replace(/á/g, 'a')
                        .replace(/é/g, 'e')
                        .replace(/í/g, 'i')
                        .replace(/ó/g, 'o')
                        .replace(/ú/g, 'u')
                        .replace(/â/g, 'a')
                        .replace(/ê/g, 'e')
                        .replace(/ô/g, 'o')
                        .replace(/ã/g, 'a')
                        .replace(/õ/g, 'o')
                        .replace(/ñ/g, 'n')
                        .replace(/ç/g, 'c')
                        .replace(/à/g, 'a');
                }
            },
            gerarCodigoCompleto: function (produto) {
                if (produto) {
                    if(produto.Digito == undefined) {
                        produto.Digito = produto.DV;
                    }
                    return produto.Codigo + '' +produto.Digito;
                }
            },
            calcularQuantidadeFP: function(produto) {
                var qtdLiberado = (produto.quantidade * produto.QuantidadeDiasFP) / (produto.QuantidadeEmbalagemFP * produto.QuantidadeUnidadesFP);
                var qtdLiberadoInteiro = parseInt(qtdLiberado);
                var qtdLiberadoDecimal = qtdLiberado - qtdLiberadoInteiro;

                return qtdLiberadoInteiro + (qtdLiberadoDecimal > 0.01 ? 1 : 0);
            },
            gerarStringData: function(dt){
                var data = dt ? dt : new Date;
                var dia = adicionarZeros(data.getDate(), 2);
                var mes = adicionarZeros(data.getMonth() + 1, 2);
                var ano = data.getFullYear();

                return ano + mes + dia;
            },
            gerarStringDataHoraBr: function(dt){
                var data = dt ? dt : new Date;
                var dia = adicionarZeros(data.getDate(), 2);
                var mes = adicionarZeros(data.getMonth() + 1, 2);
                var ano = data.getFullYear();
                var hora = adicionarZeros(data.getHours(),2);
                var minuto = adicionarZeros(data.getMinutes(),2);
                var segundo = adicionarZeros(data.getSeconds(),2)

                return dia + '/' + mes + '/' + ano + ' ' + hora + ':' + minuto + ':' + segundo;
            },
            obterIdTipoReceita: obterIdTipoReceitaBancoDeDadosBalcao,
            permitirScroll: function () {
                $timeout(function () {
                    document.querySelector('body').classList.remove('modal-aberto');
                })
            },
            impedirScroll: function () {
                $timeout(function () {
                    document.querySelector('body').classList.add('modal-aberto');
                })
            },
            calcularTotalizadores: function (orcamento) {
                var totalizadores = {
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
                return totalizadores;
            },
            inverterDiaMes: function (data) {
                if (typeof (data) == 'undefined' || data == "") {
                    return "";
                }

                return data.substr(3, 2) + '/' + data.substr(0, 2) + '/' + data.substr(6, 4);
            },
            parentsUntil: parentsUntil,
            nextUntil: nextUntil,
            obterTitleClassificacao: function (classificacao) {
                if (classificacao == 'R') return "Referência";
                if (classificacao == 'S') return "Similar";
                if (classificacao == 'SI') return "Similar Intercambiável";
                if (classificacao == 'G') return "Genérico";
            },
            obterPrecoPor: function (produto) {
                if (produto && produto.PrecoPor == 0) {
                    produto.PrecoPor = produto.PrecoDe;
                    return produto.PrecoPor;
                }
                else if (produto) {
                    return produto.PrecoPor;
                }
            },
            validarNomeUsuario: function (data) {
                if (data && data.length > 0) {
                    return true;
                }
                return false;
            },
            validarEmailUsuario: function (data) {
                var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                if (re.test(data)) {
                    return true;
                }
                return false;
            },
            validarSenhaUsuario: function (data) {
                if (data && data.length >= 6) {
                    return true;
                }
                return false;
            },
            validarProfisssaoUsuario: function (data) {
                if (data && data.length > 0) {
                    return true;
                }
                return false;
            },
            validarCampoPreenchido: function (data) {
                if (data && data.length > 0) {
                    return true;
                }
                return false;
            }
        };

        function parentsUntil(elemento, seletor) {

            if (elemento.tagName == 'BODY') {
                return null
            }

            var encontrado = matchesSelector(seletor, elemento.parentElement);

            if (!encontrado) {
                return parentsUntil(elemento.parentElement, seletor);
            }

            return elemento.parentElement;
        }

        function nextUntil(elemento, seletor) {
            if (elemento.nextElementSibling == null) {
                return null;
            }

            var encontrado = matchesSelector(seletor, elemento.nextElementSibling);

            if (!encontrado) {
                return nextUntil(elemento.nextElementSibling, seletor);
            }

            return elemento.nextElementSibling;
        }

        function matchesSelector(selector, element) {
            var all = document.querySelectorAll(selector);
            for (var i = 0; i < all.length; i++) {
                if (all[i] === element) {
                    return true;
                }
            }
            return false;
        }

        function isNullOrUndefined(val) {
            if (typeof (val) === 'undefined') {
                return true;
            }

            if (val === null) {
                return true;
            }

            return false;
        }

        function isNullUndefinedOrEmpty(val) {
            if (isNullOrUndefined(val)) {
                return true;
            }

            if (val === "") {
                return true;
            }

            return false;
        }

        function adicionarZeros(valor, digitos) {
            if (valor || valor === 0) {
                valor = valor.toString();
                var stringValor = '';
                for (var i = 0; i < (digitos - valor.length) ; i++) {
                    stringValor += '0';
                }
                stringValor += valor;
                return stringValor;
            }
        }

        function naoPermitirQuantidadeNula(item, qtdDefault) {
            var qtd = !isNullUndefinedOrEmpty(qtdDefault) ? qtdDefault : 1
            if (item == null || item == '' || isNaN(item) || item == 0) {
                item = qtd;
            }
            return item;
        }

        function isString(val){
            return (typeof (val) === 'string')
        }
        
        function obterIdTipoReceitaBancoDeDadosBalcao(receita) {
            if (receita.Tipo == 'FP')
                return 1;
            else if (receita.Tipo == 'A')
                return 2;
            else if (receita.Tipo == 'B')
                return 3;
            else if (receita.Tipo == 'B2')
                return 4;
            else
                return 5;  
        }

        function substituirIndiceStringPorAsterisco(string, indice) {
            return string.substr(0, indice) + '*' + string.substr(indice + 1);
        }
    });
}());