/*
Colocar neste arquivo toda configuracao global relacionada ao Angular
- Modulo Master
- Services comuns
- Factories comuns
- Rotas comuns
- etc..
*/

(function(){
    'use strict';

    var app = angular.module('tccenter', [
        "ngRoute",
        "ui.mask",
        //SHARED
        "tccenter.login",
        "tccenter.cabecalho",
        "tccenter.rodape",
        "tccenter.modalAlertaErro",
        "tccenter.cadastroCliente",
    ]);
    
    app.config(function ($routeProvider, $locationProvider, $httpProvider) {
        $locationProvider.hashPrefix('');
        $routeProvider.otherwise({ redirectTo: "/login" });
        $httpProvider.interceptors.push('Interceptor');
    });

    app.config(['uiMask.ConfigProvider', function (uiMaskConfigProvider) {
        uiMaskConfigProvider.addDefaultPlaceholder(false);
    }]);

    app.run(['$route', '$rootScope', '$location', function ($route, $rootScope, $location) {
        var original = $location.path;
        $location.path = function (path, notReload) {
            if (notReload === true) {
                var lastRoute = $route.current;
                var un = $rootScope.$on('$locationChangeSuccess', function () {
                    $route.current = lastRoute;
                    un();
                });
            }
            return original.apply($location, [path]);
        };
    }])

}());