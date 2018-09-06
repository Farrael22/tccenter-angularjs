angular.module("balcao.home").service("HomeService", function ($http, config, $q, Interceptor) {
    var that = this;
    var cancelarAnterior = $q.defer();

    this.obterDadosEstacao = function (ip, callbackSucesso, callbackErro) {
        
        var request = $http({
            method: "GET",
            url: config.urlAPIBalcaoOffline + "estacao/ip?endereco=" + ip
        });

        Interceptor.executarCallbacks(request, ip, callbackSucesso, callbackErro);        
    }

});
