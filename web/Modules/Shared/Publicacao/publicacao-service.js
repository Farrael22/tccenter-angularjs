angular.module("tccenter").service("PublicacaoService", function ($http, config, $q, Interceptor) {
    var that = this;
    var cancelarAnterior = $q.defer();

    this.verificarPublicacaoCurtida = function (data, callbackSucesso, callbackErro) {
        cancelarAnterior.resolve();
        cancelarAnterior = $q.defer();

        var request = $http({
            method: "GET",
            url: config.urlAPITccenter + 'publicacao/verificarCurtida?idUsuario=' + data.idUsuario + '&idPublicacao=' + data.idPublicacao
        });

        Interceptor.executarCallbacks(request, data, callbackSucesso, callbackErro);
    };

    this.buscarUsuarioPorId = function (data, callbackSucesso, callbackErro) {
        cancelarAnterior.resolve();
        cancelarAnterior = $q.defer();

        var request = $http({
            method: "GET",
            url: config.urlAPITccenter + 'usuario/buscarPorId?idUsuario=' + data
        });

        Interceptor.executarCallbacks(request, data, callbackSucesso, callbackErro);
    };

    this.curtirPublicacao = function (data, callbackSucesso, callbackErro) {
        cancelarAnterior.resolve();
        cancelarAnterior = $q.defer();

        var request = $http({
            method: "GET",
            url: config.urlAPITccenter + 'publicacao/curtirPublicacao?idUsuario=' + data.idUsuario + '&idPublicacao=' + data.idPublicacao
        });

        Interceptor.executarCallbacks(request, data, callbackSucesso, callbackErro);
    };

    this.descurtirPublicacao = function (data, callbackSucesso, callbackErro) {
        cancelarAnterior.resolve();
        cancelarAnterior = $q.defer();

        var request = $http({
            method: "GET",
            url: config.urlAPITccenter + 'publicacao/descurtirPublicacao?idUsuario=' + data.idUsuario + '&idPublicacao=' + data.idPublicacao
        });

        Interceptor.executarCallbacks(request, data, callbackSucesso, callbackErro);
    };

    this.comentarPublicacao = function (param, callbackSucesso, callbackErro) {
        cancelarAnterior.resolve();
        cancelarAnterior = $q.defer();

        var request = $http({
            method: "POST",
            url: config.urlAPITccenter + 'publicacao/comentarPublicacao',
            data: param
        });

        Interceptor.executarCallbacks(request, param, callbackSucesso, callbackErro);
    };

    this.obterComentarios = function (data, callbackSucesso, callbackErro) {
        cancelarAnterior.resolve();
        cancelarAnterior = $q.defer();

        var request = $http({
            method: "GET",
            url: config.urlAPITccenter + 'publicacao/obterComentarios?idPublicacao=' + data
        });

        Interceptor.executarCallbacks(request, data, callbackSucesso, callbackErro);
    };
});
