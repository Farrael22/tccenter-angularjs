angular.module("tccenter").service("SugestaoPublicacaoService", function ($http, config, $q, Interceptor) {
    var that = this;
    var cancelarAnterior = $q.defer();

    this.buscarQuantidadePublicacoesUsuario = function (data, callbackSucesso, callbackErro) {
        cancelarAnterior.resolve();
        cancelarAnterior = $q.defer();

        var request = $http({
            method: "GET",
            url: config.urlAPITccenter + 'usuario/quantidadePublicacao?idUsuario=' + data
        });

        Interceptor.executarCallbacks(request, data, callbackSucesso, callbackErro);
    };

    this.buscarQuantidadeSeguidoresUsuario = function (data, callbackSucesso, callbackErro) {
        cancelarAnterior.resolve();
        cancelarAnterior = $q.defer();

        var request = $http({
            method: "GET",
            url: config.urlAPITccenter + 'usuario/quantidadeSeguidores?idUsuario=' + data
        });

        Interceptor.executarCallbacks(request, data, callbackSucesso, callbackErro);
    };

    this.buscarUsuariosSeguidos = function (data, callbackSucesso, callbackErro) {
        cancelarAnterior.resolve();
        cancelarAnterior = $q.defer();

        var request = $http({
            method: "GET",
            url: config.urlAPITccenter + 'usuario/usuariosSeguidos?idUsuario=' + data
        });

        Interceptor.executarCallbacks(request, data, callbackSucesso, callbackErro);
    };
});
