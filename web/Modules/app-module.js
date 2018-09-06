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

    var app = angular.module('balcao', [
        "ngRoute",
        "ui.mask",
        //SHARED
        "balcao.cabecalho",
        "balcao.modalLoading",
        "balcao.modalAlertaErro",
        //HOME
        "balcao.home",
        "balcao.produto",
        "balcao.orcamento",
        "balcao.modalInformacao",
    ]);
    
    app.config(function ($routeProvider, $locationProvider, $httpProvider) {
        $locationProvider.hashPrefix('');
        $routeProvider.otherwise({ redirectTo: "/home" });
        $httpProvider.interceptors.push('Interceptor');
    });

    app.config(['uiMask.ConfigProvider', function (uiMaskConfigProvider) {
        uiMaskConfigProvider.addDefaultPlaceholder(false);
        //uiMaskConfigProvider.maskDefinitions({'A': /[a-z]/, '*': /[a-zA-Z0-9]/});
        //uiMaskConfigProvider.clearOnBlur(true);
        //uiMaskConfigProvider.clearOnBlurPlaceholder(false);
        //uiMaskConfigProvider.allowInvalidValue(true);
        //uiMaskConfigProvider.eventsToHandle(['input', 'keyup', 'click']); 
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