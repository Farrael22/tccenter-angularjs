angular.module("tccenter.modalCadastroPublicacao").service("CadastroPublicacaoService", function ($http, config, $q, Interceptor) {
    var that = this;
    var cancelarAnterior = $q.defer();
    
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

    this.obterOrientadores = function (callbackSucesso) {
        cancelarAnterior.resolve();
        cancelarAnterior = $q.defer();

        var request = $http({
            method: "GET",
            url: config.urlAPITccenter + 'orientador/obterOrientadores'
        });

        Interceptor.executarCallbacks(request, null, callbackSucesso, null);
    };
});
