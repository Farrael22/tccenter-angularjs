angular.module("tccenter.login").service("LoginService", function ($http, config, $q, Interceptor) {
    var that = this;
    var cancelarAnterior = $q.defer();

    this.efetuarLogin = function (data, callbackSucesso, callbackErro) {
        cancelarAnterior.resolve();
        cancelarAnterior = $q.defer();

        var request = $http({
            method: "POST",
            url: config.urlAPITccenter + 'login/efetuarLogin',
            data: data
        });

        Interceptor.executarCallbacks(request, data, callbackSucesso, callbackErro);
    };
});
