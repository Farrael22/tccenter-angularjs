angular.module("tccenter").constant("config", {
    // Recupera valores das variaveis de servidor renderizadas no arquivo _Layout.cshtml
    urlAPITccenter: GLOBAL_SERVER.API_URL
});

// Help: https://docs.bugsnag.com/platforms/browsers/configuration-options/
Bugsnag.apiKey = "c0968facbcf63ba02982f4b9a9ff27c1";