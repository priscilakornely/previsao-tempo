using Moq;
using PrevisaoTempo.Aplicacao.Interface;
using PrevisaoTempo.Aplicacao.Servicos;
using PrevisaoTempo.Aplicacao.ViewModels;
using PrevisaoTempo.Dominio.Entidades;
using PrevisaoTempo.Dominio.Interfaces.Servicos;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PrevisaoTempo.Testes.Servicos
{
    public class CidadeAppServiceTestes
    {
        private readonly ICidadeAppService _cidadeAppService;
        private readonly Mock<ICidadeService> _cidadeServiceMock;
        private readonly Mock<CidadeAppService> _cidadeAppServiceMock;

        public CidadeAppServiceTestes()
        {
            _cidadeServiceMock = new Mock<ICidadeService>();
            _cidadeAppServiceMock = new Mock<CidadeAppService>(_cidadeServiceMock.Object) { CallBase = true };
            _cidadeAppService = _cidadeAppServiceMock.Object;
        }

        [Fact]
        public void ConsigoBuscarTodasCidades()
        {
            _cidadeServiceMock
                .Setup(c => c.BuscarTodos())
                .Returns(new List<Cidade>
                {
                    new Cidade
                    {
                        Id = 1,
                        OpenWeatherId = "789",
                        Nome = "Berlim",
                        Latitude =  45,
                        Longitude = 29.79,
                        Pais = "DE"
                    },
                    new Cidade
                    {
                        Id = 5,
                        OpenWeatherId = "334",
                        Nome = "Gaspar",
                        Latitude =  77.1,
                        Longitude = 55.98,
                        Pais = "BR"
                    }
                });

            var cidades = _cidadeAppService.BuscarTodas();

            Assert.NotNull(cidades);
            Assert.Equal(2, cidades.Count);

            var cidade = cidades.First();
            Assert.Equal(1, cidade.Id);
            Assert.Equal("789", cidade.OpenWeatherId);
            Assert.Equal("Berlim", cidade.Nome);
            Assert.Equal(45, cidade.Latitude);
            Assert.Equal(29.79, cidade.Longitude);
            Assert.Equal("DE", cidade.Pais);

            cidade = cidades.Last();
            Assert.Equal(5, cidade.Id);
            Assert.Equal("334", cidade.OpenWeatherId);
            Assert.Equal("Gaspar", cidade.Nome);
            Assert.Equal(77.1, cidade.Latitude);
            Assert.Equal(55.98, cidade.Longitude);
            Assert.Equal("BR", cidade.Pais);

            _cidadeServiceMock.Verify(c => c.BuscarTodos(), Times.Once);
        }

        [Fact]
        public void ConsigoAdicionarCidade()
        {
            _cidadeAppService.Adicionar(new CidadeViewModel
            {
                Id = 0,
                OpenWeatherId = "111",
                Nome = "Blumenau",
                Latitude = 67.98,
                Longitude = 11.4,
                Pais = "BR"
            });

            _cidadeServiceMock.Verify(c => c.Adicionar(It.IsAny<Cidade>()), Times.Once);
        }
    }
}
