angular.module("tccenter.cadastroCliente").service("CadastroClienteService", function ($http, config, $q, Interceptor) {
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

    this.realizarCadastro = function (dados, callbackSucesso, callbackErro) {
        cancelarAnterior.resolve();
        cancelarAnterior = $q.defer();

        var request = $http({
            method: "POST",
            url: config.urlAPITccenter + 'usuario/cadastrar',
            data: dados
        });

        Interceptor.executarCallbacks(request, dados, callbackSucesso, callbackErro);
    };
});
