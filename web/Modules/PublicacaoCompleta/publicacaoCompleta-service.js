angular.module("tccenter.publicacaoCompleta").service("PublicacaoCompletaService", function ($http, config, $q, Interceptor) {
    var that = this;
    var cancelarAnterior = $q.defer();

    this.buscarTopicosInteressantes = function (callbackSucesso, callbackErro) {
        cancelarAnterior.resolve();
        cancelarAnterior = $q.defer();

        var request = $http({
            method: "GET",
            url: config.urlAPITccenter + 'topicosInteressantes/obter'
        });

        Interceptor.executarCallbacks(request, null, callbackSucesso, callbackErro);
    };

    this.cadastrarPublicacao = function (param, callbackSucesso, callbackErro) {
        cancelarAnterior.resolve();
        cancelarAnterior = $q.defer();

        var request = $http({
            method: "POST",
            url: config.urlAPITccenter + 'publicacao/cadastrarPublicacao',
            data: param
        });

        Interceptor.executarCallbacks(request, param, callbackSucesso, callbackErro);
    };
});
