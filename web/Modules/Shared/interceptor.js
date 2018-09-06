(function(){
    'use strict';

    angular.module("balcao").factory('Interceptor', function Interceptor($rootScope, $q) {
            return {
                request: function (config) {
                    //config.headers['X-TOKEN'] = "exemplo";
                    return config;
                },
                responseError: function (error) {
                    if (error.config && error.config.timeout && error.config.timeout.$$state && error.config.timeout.$$state.status === 1) {
                        //cancelada manualmente
                        return $q.reject();
                    }
                    else {
                        if (error.status === -1) {
                            error.message = "Não foi possível concluir a operação. Verifique a conexão de rede";
                        }
                        else if (error.status === 406 && error.data && error.data.Mensagem) {
                            error.message = error.data.Mensagem;
                        }
                        else if (error.status === 404 && error.data && error.data.Mensagem) {
                            error.message = error.data.Mensagem;
                        }
                        else if (error.status === 412 && error.data && error.data.Mensagem) {
                            error.message = error.data.Mensagem;
                        }
                        else if (error.status === 503 && error.data && error.data.Mensagem) {
                            error.message = error.data.Mensagem;
                        }
                        else if (typeof(error.status) === 'undefined') {
                            error.message = "";
                        }
                        else {
                            error.message = "Ocorreu um erro inesperado. Entre em contato com o suporte. Erro: " + error.status;
                        }
                        return $q.reject(error);
                    }
                },
                executarCallbacks: function (request, entryData, sucess, error) {
                    request.then(
                        function successCallback(response) {
                            sucessOutputFunc(response, sucess, entryData);
                        },
                        function errorCallback(response) {
                            errorOutputFunc(response, error, entryData);
                        }
                    );
                }
            };

            function errorOutputFunc(response, output, entryData) {
                if (response && typeof (output) === 'function') {
                    var parametros = entryData ? entryData.errorCallbackParameters : null
                    if (response.status === 412) {
                        $rootScope.$emit('testeInterceptorEmissorDeEvento');
                    }
                    if (response.status === 503) {
                        $rootScope.$emit('bancoIndisponivel', true);
                    }
                    output(response.message, parametros);
                }
            }

            function sucessOutputFunc(response, output, entryData) {
                if (response && typeof (output) === 'function') {
                    var parametros = entryData ? entryData.sucessCallbackParameters : null
                    var dados;
                    output(response.data, parametros);
                }
            }
        }
    );
}());