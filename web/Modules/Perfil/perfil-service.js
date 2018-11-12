angular.module("tccenter.perfil").service("PerfilService", function ($http, config, $q, Interceptor) {
    var that = this;
    var cancelarAnterior = $q.defer();

    this.buscarUsuarioPorId = function (data, callbackSucesso, callbackErro) {
        cancelarAnterior.resolve();
        cancelarAnterior = $q.defer();

        var request = $http({
            method: "GET",
            url: config.urlAPITccenter + 'usuario/buscarPorId?idUsuario=' + data
        });

        Interceptor.executarCallbacks(request, data, callbackSucesso, callbackErro);
    };
    
    this.seguirUsuario = function (data, callbackSucesso, callbackErro) {
        cancelarAnterior.resolve();
        cancelarAnterior = $q.defer();

        var request = $http({
            method: "POST",
            url: config.urlAPITccenter + 'seguirUsuario?idUsuarioLogado=' + data.idUsuarioLogado + '&idSeguir=' + data.idUsuarioSeguir
        });

        Interceptor.executarCallbacks(request, data, callbackSucesso, callbackErro);
    };

    this.pararSeguirUsuario = function (data, callbackSucesso, callbackErro) {
        cancelarAnterior.resolve();
        cancelarAnterior = $q.defer();

        var request = $http({
            method: "POST",
            url: config.urlAPITccenter + 'pararSeguirUsuario?idUsuarioLogado=' + data.idUsuarioLogado + '&idPararSeguir=' + data.idUsuarioPararSeguir
        });

        Interceptor.executarCallbacks(request, data, callbackSucesso, callbackErro);
    };

});
