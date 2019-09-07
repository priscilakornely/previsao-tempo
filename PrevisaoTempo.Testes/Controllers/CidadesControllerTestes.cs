using Moq;
using Newtonsoft.Json.Linq;
using PrevisaoTempo.Aplicacao.Interface;
using PrevisaoTempo.Aplicacao.ViewModels;
using PrevisaoTempo.Controllers;
using PrevisaoTempo.Dominio.Entidades;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace PrevisaoTempo.Testes.Controllers
{
    public class CidadesControllerTestes
    {
        private readonly CidadesController _cidadesController;
        private readonly Mock<ICidadeAppService> _cidadeAppServiceMock;
        private readonly Mock<IPrevisaoTempoCidadeAppService> _previsaoTempoCidadeAppServiceMock;
        private readonly List<CidadeViewModel> _cidadesViewModel;

        public CidadesControllerTestes()
        {
            _cidadeAppServiceMock = new Mock<ICidadeAppService>();
            _previsaoTempoCidadeAppServiceMock = new Mock<IPrevisaoTempoCidadeAppService>();
            _cidadesController = new CidadesController(
                _cidadeAppServiceMock.Object, _previsaoTempoCidadeAppServiceMock.Object
            );

            _cidadesViewModel = new List<CidadeViewModel>
            {
                new CidadeViewModel
                {
                    Id = 1,
                    OpenWeatherId = "555",
                    Nome = "Berlim",
                    Latitude =  77,
                    Longitude = 88.12,
                    Pais = "DE"
                },
                new CidadeViewModel
                {
                    Id = 345,
                    OpenWeatherId = "4567865",
                    Nome = "Blumenau",
                    Latitude =  100.15,
                    Longitude = 200.12,
                    Pais = "BR"
                }
            };
        }

        [Fact]
        public void ConsigoBuscarTodasCidades()
        {
            _cidadeAppServiceMock
                .Setup(c => c.BuscarTodas())
                .Returns(_cidadesViewModel);

            var resultado = _cidadesController.BuscarTodas();
            Assert.IsType<JsonResult>(resultado);

            var resultadoJson = (JsonResult)resultado;
            Assert.NotNull(resultadoJson.Data);
            Assert.IsType<List<CidadeViewModel>>(resultadoJson.Data);
            Assert.Equal(JsonRequestBehavior.AllowGet, resultadoJson.JsonRequestBehavior);

            var cidadesVM = (List<CidadeViewModel>)resultadoJson.Data;
            Assert.Equal(2, cidadesVM.Count);
            Assert.Equal(1, cidadesVM.First().Id);
            Assert.Equal(345, cidadesVM.Last().Id);

            _cidadeAppServiceMock.Verify(c => c.BuscarTodas(), Times.Once);
        }

        [Fact]
        public async Task ConsigoBuscarCidadePorNome()
        {
            _previsaoTempoCidadeAppServiceMock
                .Setup(p => p.BuscarCidadesPeloNomeAsync(It.IsAny<string>()))
                .ReturnsAsync(_cidadesViewModel);

            var resultado = await _cidadesController.BuscarPorNome("blumenau");
            Assert.IsType<JsonResult>(resultado);

            var resultadoJson = (JsonResult)resultado;
            Assert.NotNull(resultadoJson.Data);
            Assert.Equal(JsonRequestBehavior.AllowGet, resultadoJson.JsonRequestBehavior);

            var jo = JObject.FromObject(resultadoJson.Data);
            Assert.True((bool)jo["sucesso"]);
            Assert.Equal(200, jo["status"]);

            var cidadesVM = jo["conteudo"].ToObject<List<CidadeViewModel>>();
            Assert.Equal(2, cidadesVM.Count);
            Assert.Equal(1, cidadesVM.First().Id);
            Assert.Equal(345, cidadesVM.Last().Id);

            _previsaoTempoCidadeAppServiceMock.Verify(p => p.BuscarCidadesPeloNomeAsync(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task RetornaErroAoTentarBuscarCidadeSemPassarNome(string nome)
        {
            var resultado = await _cidadesController.BuscarPorNome(nome);
            Assert.IsType<JsonResult>(resultado);

            var resultadoJson = (JsonResult)resultado;
            Assert.NotNull(resultadoJson.Data);
            Assert.Equal(JsonRequestBehavior.AllowGet, resultadoJson.JsonRequestBehavior);

            var jo = JObject.FromObject(resultadoJson.Data);
            Assert.False((bool)jo["sucesso"]);
            Assert.Equal(400, jo["status"]);

            _previsaoTempoCidadeAppServiceMock.Verify(p => p.BuscarCidadesPeloNomeAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task RetornaErroAoTentarBuscarCidadePorNomeInexistente()
        {
            _previsaoTempoCidadeAppServiceMock
                .Setup(p => p.BuscarCidadesPeloNomeAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<CidadeViewModel>());

            var resultado = await _cidadesController.BuscarPorNome("aa");
            Assert.IsType<JsonResult>(resultado);

            var resultadoJson = (JsonResult)resultado;
            Assert.NotNull(resultadoJson.Data);
            Assert.Equal(JsonRequestBehavior.AllowGet, resultadoJson.JsonRequestBehavior);

            var jo = JObject.FromObject(resultadoJson.Data);
            Assert.False((bool)jo["sucesso"]);
            Assert.Equal(404, jo["status"]);

            _previsaoTempoCidadeAppServiceMock.Verify(p => p.BuscarCidadesPeloNomeAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ConsigoBuscarPrevisaoTempoCidade()
        {
            _previsaoTempoCidadeAppServiceMock
                .Setup(p => p.BuscarPrevisaoTempoCidadeAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<PrevisaoTempoViewModel>
                {
                    new PrevisaoTempoViewModel
                    {
                        Data = "quinta-feira, 01 agosto",
                        PrevisaoHoras = new List<PrevisaoHoraViewModel>()
                    },
                    new PrevisaoTempoViewModel
                    {
                        Data = "quinta-feira, 05 agosto",
                        PrevisaoHoras = new List<PrevisaoHoraViewModel>()
                    }
                });

            var resultado = await _cidadesController.PrevisaoTempo("1233");
            Assert.IsType<JsonResult>(resultado);

            var resultadoJson = (JsonResult)resultado;
            Assert.NotNull(resultadoJson.Data);
            Assert.Equal(JsonRequestBehavior.AllowGet, resultadoJson.JsonRequestBehavior);

            var jo = JObject.FromObject(resultadoJson.Data);
            Assert.True((bool)jo["sucesso"]);
            Assert.Equal(200, jo["status"]);

            var previsoesVM = jo["conteudo"].ToObject<List<PrevisaoTempoViewModel>>();
            Assert.Equal(2, previsoesVM.Count);
            Assert.Equal("quinta-feira, 01 agosto", previsoesVM.First().Data);
            Assert.Equal("quinta-feira, 05 agosto", previsoesVM.Last().Data);

            _previsaoTempoCidadeAppServiceMock.Verify(p => p.BuscarPrevisaoTempoCidadeAsync(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task RetornaErroAoTentarBuscarPrevisaoTempoCidadeSemPassarId(string id)
        {
            var resultado = await _cidadesController.PrevisaoTempo(id);
            Assert.IsType<JsonResult>(resultado);

            var resultadoJson = (JsonResult)resultado;
            Assert.NotNull(resultadoJson.Data);
            Assert.Equal(JsonRequestBehavior.AllowGet, resultadoJson.JsonRequestBehavior);

            var jo = JObject.FromObject(resultadoJson.Data);
            Assert.False((bool)jo["sucesso"]);
            Assert.Equal(400, jo["status"]);

            _previsaoTempoCidadeAppServiceMock.Verify(p => p.BuscarPrevisaoTempoCidadeAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task RetornaErroAoTentarBuscarPrevisaoTempoCidadeInexistente()
        {
            _previsaoTempoCidadeAppServiceMock
                .Setup(p => p.BuscarPrevisaoTempoCidadeAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<PrevisaoTempoViewModel>());

            var resultado = await _cidadesController.PrevisaoTempo("8765");
            Assert.IsType<JsonResult>(resultado);

            var resultadoJson = (JsonResult)resultado;
            Assert.NotNull(resultadoJson.Data);
            Assert.Equal(JsonRequestBehavior.AllowGet, resultadoJson.JsonRequestBehavior);

            var jo = JObject.FromObject(resultadoJson.Data);
            Assert.False((bool)jo["sucesso"]);
            Assert.Equal(404, jo["status"]);

            _previsaoTempoCidadeAppServiceMock.Verify(p => p.BuscarPrevisaoTempoCidadeAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void ConsigoAdicionarCidade()
        {
            var resultado = _cidadesController.Adicionar(new CidadeViewModel
            {
                Id = 0,
                OpenWeatherId = "333",
                Nome = "Blumenau",
                Pais = "BR",
                Latitude = 12.4,
                Longitude = 77
            });

            Assert.IsType<JsonResult>(resultado);

            var resultadoJson = (JsonResult)resultado;
            Assert.NotNull(resultadoJson.Data);

            var jo = JObject.FromObject(resultadoJson.Data);
            Assert.True((bool)jo["sucesso"]);
            Assert.Equal(201, jo["status"]);

            _cidadeAppServiceMock.Verify(c => c.Adicionar(It.IsAny<CidadeViewModel>()), Times.Once);
        }

        [Fact]
        public void RetornaErroAoTentarCidadeJaCadastrada()
        {
            _cidadeAppServiceMock
                .Setup(c => c.Adicionar(It.IsAny<CidadeViewModel>()))
                .Throws(new ValidationException("Cidade já cadastrada."));

            var resultado = _cidadesController.Adicionar(_cidadesViewModel.First());
            Assert.IsType<JsonResult>(resultado);

            var resultadoJson = (JsonResult)resultado;
            Assert.NotNull(resultadoJson.Data);

            var jo = JObject.FromObject(resultadoJson.Data);
            Assert.False((bool)jo["sucesso"]);
            Assert.Equal(400, jo["status"]);

            _cidadeAppServiceMock.Verify(c => c.Adicionar(It.IsAny<CidadeViewModel>()), Times.Once);
        }

        [Fact]
        public void ConsigoExcluirCidade()
        {
            _cidadeAppServiceMock
                .Setup(c => c.BuscarPorId(It.IsAny<long>()))
                .Returns(new Cidade
                {
                    Id = 10,
                    Nome = "Blumenau"
                });

            _cidadeAppServiceMock
                .Setup(c => c.Excluir(It.IsAny<Cidade>()));

            var resultado = _cidadesController.Excluir(10);
            Assert.IsType<JsonResult>(resultado);

            var resultadoJson = (JsonResult)resultado;
            Assert.NotNull(resultadoJson.Data);

            var jo = JObject.FromObject(resultadoJson.Data);
            Assert.True((bool)jo["sucesso"]);
            Assert.Equal(200, jo["status"]);

            _cidadeAppServiceMock.Verify(c => c.BuscarPorId(It.IsAny<long>()), Times.Once);
            _cidadeAppServiceMock.Verify(c => c.Excluir(It.IsAny<Cidade>()), Times.Once);
        }

        [Fact]
        public void RetornaErroAoTentarExcluirCidadeSemPassarId()
        {
            var resultado = _cidadesController.Excluir(0);
            Assert.IsType<JsonResult>(resultado);

            var resultadoJson = (JsonResult)resultado;
            Assert.NotNull(resultadoJson.Data);

            var jo = JObject.FromObject(resultadoJson.Data);
            Assert.False((bool)jo["sucesso"]);
            Assert.Equal(400, jo["status"]);

            _cidadeAppServiceMock.Verify(c => c.BuscarPorId(It.IsAny<long>()), Times.Never);
            _cidadeAppServiceMock.Verify(c => c.Excluir(It.IsAny<Cidade>()), Times.Never);
        }
    }
}
