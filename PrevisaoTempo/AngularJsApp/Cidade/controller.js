cidadeApp.controller('cidadeCtrl', function ($scope, cidadeService) {

    $scope.cidades = [];
    $scope.cidadesCadastradas = [];
    $scope.nome = '';

    buscarCidadesCadastradas();

    function buscarCidadesCadastradas() {
        $scope.cidades = [];

        cidadeService.buscarCidadesCadastradas()
            .then(function (response) {
                $scope.cidadesCadastradas = response.data;
            },
            function () {
                alert("Ocorreu um erro ao tentar listar as cidades.");
            });
    }

    $scope.buscarCidadesPeloNome = function () {
        cidadeService.buscarCidadesPeloNome($scope.nome)
            .then(function (response) {
                if (response.data.sucesso) {
                    $scope.cidades = response.data.conteudo;
                } else {
                    alert(response.data.mensagem)
                }
            },
            function () {
                alert("Ocorreu um erro ao tentar listar as cidades.");
        });
    }

    $scope.adicionarCidade = function (cidade) {
        cidadeService.adicionarCidade(cidade)
            .then(function (response) {
                if (response.data.sucesso) {
                    alert("Cidade adicionada.")
                    $("#AdicionarCidade").modal('hide');
                    limparDados();
                    buscarCidadesCadastradas();
                } else {
                    alert(response.data.mensagem)
                }
            },
            function () {
                alert("Ocorreu um erro ao adicionar a cidade.");
            });
    }

    $scope.buscarPrevisaoTempo = function (id) {
        cidadeService.buscarPrevisaoTempo(id)
            .then(function (response) {
                if (response.data.sucesso) {
                    $scope.previsoesTempo = response.data.conteudo;
                } else {
                    alert(response.data.mensagem)
                }
            },
            function () {
                alert("Ocorreu um erro ao tentar buscar a previsão do tempo da cidade.");
            });
    }

    $scope.excluirCidade = function (id) {
        cidadeService.excluirCidade(id)
            .then(function (response) {
                if (response.data.sucesso) {
                    alert("Cidade excluída.");
                    buscarCidadesCadastradas();
                } else {
                    alert(response.data.mensagem)
                }
            },
            function () {
                alert("Ocorreu um erro ao tentar excluir a cidade.");
            });
    }

    $('#AdicionarCidade').on('hidden.bs.modal', function () {
        limparDados();
    });

    function limparDados () {
        $scope.nome = '';
        $scope.cidades = [];
    }
});