angular.module("balcao").constant("config", {
    // Recupera valores das variaveis de servidor renderizadas no arquivo _Layout.cshtml
    urlBalcaoOnline: GLOBAL_SERVER.SISTEMA_ONLINE,
    urlAPIBalcaoOffline: GLOBAL_SERVER.API_URL,
    urlAPIRetaguarda: {
        produtos: GLOBAL_SERVER.API_ARAUJO_PRODUTOS,
        filiais: GLOBAL_SERVER.API_ARAUJO_FILIAIS,
        kitvirtual: GLOBAL_SERVER.API_ARAUJO_KITVIRTUAL,
    }
});

// Help: https://docs.bugsnag.com/platforms/browsers/configuration-options/
Bugsnag.apiKey = "c0968facbcf63ba02982f4b9a9ff27c1";