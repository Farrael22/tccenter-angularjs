(function () {
    angular.module("balcao.produto").service("ProdutoService", function ($http, config, $q, Interceptor) {
        var that = this;
        var cancelarAnterior = $q.defer();

        this.pesquisa = function (data, callbackSucesso, callbackErro) {
            cancelarAnterior.resolve();
            cancelarAnterior = $q.defer();

            var request = $http({
                method: "GET",
                url: config.urlAPIBalcaoOffline + "pesquisa/filial/" + data.filial + "?texto=" + data.busca,
                timeout: cancelarAnterior.promise
            });

            Interceptor.executarCallbacks(request, data, callbackSucesso, callbackErro);
        }

        this.equivalentes = function (data, callbackSucesso, callbackErro) {
            cancelarAnterior.resolve();
            cancelarAnterior = $q.defer();

            var request = $http({
                method: "GET",
                url: config.urlAPIBalcaoOffline + "pesquisa/filial/" + data.filial + "/equivalentes?codigoProduto=" + data.produto,
                timeout: cancelarAnterior.promise
            });

            Interceptor.executarCallbacks(request, data, callbackSucesso, callbackErro);
        }

        this.cancelarRequisicoes = function () {
            cancelarAnterior.resolve();
        }
    });
}());