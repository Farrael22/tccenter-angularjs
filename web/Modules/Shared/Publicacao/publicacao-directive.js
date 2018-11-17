angular.module("tccenter").directive("publicacao", function () {
    return {
        templateUrl: 'Shared/publicacao/publicacao',
        restrict: 'E',
        scope: {
            array: "="
        },
        controller: publicacaoController,
        controllerAs: 'ctrl'
    };
});

publicacaoController.$inject = ['$scope', '$location', 'TccenterStorage', 'PublicacaoService'];
function publicacaoController($scope, $location, TccenterStorage, PublicacaoService) {
    var vm = this;

    vm.array = $scope.array;
    vm.Usuario = TccenterStorage.obterUsuario();

    buscarInformacoesAdicionaisPublicacoes();

    function buscarInformacoesAdicionaisPublicacoes() {
        for (var i = 0; i < vm.array.length; i++) {
            vm.array[i].Curtida = false;
            PublicacaoService.buscarUsuarioPorId(vm.array[i].IdUsuario, buscarUsuarioPorIdSucesso);

            data = {
                idUsuario: vm.Usuario.Id,
                idPublicacao: vm.array[i].IdPublicacao
            };
            PublicacaoService.verificarPublicacaoCurtida(data, verificarPublicacaoCurtidaSucesso);

            PublicacaoService.obterComentarios(vm.array[i].IdPublicacao, obterComentariosSucesso);
        }
    }

    function buscarUsuarioPorIdSucesso(data) {
        for (var i = 0; i < vm.array.length; i++) {
            if (vm.array[i].IdUsuario === data.Id) {
                vm.array[i].Usuario = data;
            }
        }
    }

    function verificarPublicacaoCurtidaSucesso(data) {
        for (var i = 0; i < vm.array.length; i++) {
            if (vm.array[i].IdPublicacao === data) {
                vm.array[i].Curtida = true;
            }
        }
    }

    function obterComentariosSucesso(data) {
        for (var i = 0; i < vm.array.length; i++) {
            if (data[0] && vm.array[i].IdPublicacao === data[0].IdPublicacao) {
                vm.array[i].Mensagens = data;
            }
        }
    }

    vm.exibirMensagens = function (idx) {
        if (vm.array[idx].exibirMensagens) {
            vm.array[idx].exibirMensagens = false;
            return;
        }
        vm.array[idx].exibirMensagens = true;
    };

    vm.curtirPublicacao = function (IdPublicacao) {
        data = {
            idPublicacao: IdPublicacao,
            idUsuario: vm.Usuario.Id
        };

        PublicacaoService.curtirPublicacao(data, curtirPublicacaoSucesso);
    };

    function curtirPublicacaoSucesso() {
        buscarInformacoesAdicionaisPublicacoes();
    }

    vm.descurtirPublicacao = function (IdPublicacao) {
        data = {
            idPublicacao: IdPublicacao,
            idUsuario: vm.Usuario.Id
        };

        PublicacaoService.descurtirPublicacao(data, descurtirPublicacaoSucesso);
    };

    function descurtirPublicacaoSucesso() {
        buscarInformacoesAdicionaisPublicacoes();
    }

    vm.enviarComentario = function (publicacao) {
        if (publicacao.Comentario && publicacao.Comentario.length > 0) {
            data = {

                IdPublicacao: publicacao.IdPublicacao,
                IdUsuarioComentou: vm.Usuario.Id,
                DescMensagem: publicacao.Comentario
            };
            publicacao.Comentario = '';
            PublicacaoService.comentarPublicacao(data, comentarPublicacaoSucesso);
        }
    };

    function comentarPublicacaoSucesso(data) {
        buscarInformacoesAdicionaisPublicacoes();
    }

    vm.exibirPerfilUsuario = function (idUsuario) {
        $scope.$root.$emit('exibirPerfilUsuario', idUsuario);
        $location.path("perfil");
    };
}