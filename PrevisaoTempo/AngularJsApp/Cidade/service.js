cidadeApp.service('cidadeService', function ($http) {

    this.buscarCidadesPeloNome = function (nome) {
        return $http.get("/Cidades/BuscarPorNome?nome=" + nome);
    }

    this.buscarCidadesCadastradas = function () {
        return $http.get("/Cidades/BuscarTodas");
    }

    this.adicionarCidade = function (cidade) {
        var request = $http({
            method: 'post',
            url: '/Cidades/Adicionar',
            data: cidade
        });

        return request;
    }

    this.buscarPrevisaoTempo = function (id) {
        return $http.get("/Cidades/PrevisaoTempo?id=" + id);
    }

    this.excluirCidade = function (id) {
        return $http.post("/Cidades/Excluir/" + id);
    }
});