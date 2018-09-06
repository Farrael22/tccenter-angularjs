angular.module("balcao.cabecalho").service("CabecalhoService", function ($http, config, $q, Interceptor) {
    var that = this;
    var cancelarAnterior = $q.defer();

    this.obterFilialOnline = function (ip, callbackSucesso, callbackErro) {
        cancelarAnterior.resolve();
        cancelarAnterior = $q.defer();

        var request = $http({
            method: "GET",
            url: config.urlAPIBalcaoOffline + "estacao/ping"
        });

        Interceptor.executarCallbacks(request, null, callbackSucesso, callbackErro);
    }
});
