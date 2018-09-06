angular.module("balcao.orcamento").service("OrcamentoService", function ($http, config, $q, Interceptor) {
    var that = this;
    var cancelarAnterior = $q.defer();
    var contador = 0;
    var produtosCompletos = [];


    /* Funcionalidades Kit Virtual */

    this.consultarKitVirtual = function (data, callbackSucesso, callbackErro) {
        cancelarAnterior.resolve();
        cancelarAnterior = $q.defer();

        var request = $http({
            method: "POST",
            url: config.urlAPIBalcaoOffline + 'kitvirtual/ObterKitsVirtuaisPossiveis',
            data: data,
            timeout: cancelarAnterior.promise
        });

        Interceptor.executarCallbacks(request, data, callbackSucesso, callbackErro);
    }

});
