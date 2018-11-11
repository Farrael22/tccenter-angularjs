angular.module("tccenter.editarPerfil").service("EditarPerfilService", function ($http, config, $q, Interceptor) {
    var that = this;
    var cancelarAnterior = $q.defer();
    
    this.realizarCadastro = function (dados, callbackSucesso, callbackErro) {
        cancelarAnterior.resolve();
        cancelarAnterior = $q.defer();

        var request = $http({
            method: "POST",
            url: config.urlAPITccenter + 'usuario/alterar',
            data: dados
        });

        Interceptor.executarCallbacks(request, dados, callbackSucesso, callbackErro);
    };

    this.buscarTopicosInteressantes = function (callbackSucesso, callbackErro) {
        cancelarAnterior.resolve();
        cancelarAnterior = $q.defer();

        var request = $http({
            method: "GET",
            url: config.urlAPITccenter + 'topicosInteressantes/obter'
        });

        Interceptor.executarCallbacks(request, null, callbackSucesso, callbackErro);
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
});
