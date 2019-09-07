using Moq;
using PrevisaoTempo.Aplicacao.DTOs.OpenWeather;
using PrevisaoTempo.Aplicacao.Interface;
using PrevisaoTempo.Aplicacao.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PrevisaoTempo.Testes.Servicos
{
    public class PrevisaoTempoCidadeAppServiceTestes
    {
        private readonly IPrevisaoTempoCidadeAppService _previsaoTempoCidadeAppService;
        private readonly Mock<PrevisaoTempoCidadeAppService> _previsaoTempoCidadeAppServiceMock;

        public PrevisaoTempoCidadeAppServiceTestes()
        {
            _previsaoTempoCidadeAppService = new PrevisaoTempoCidadeAppService();
            _previsaoTempoCidadeAppServiceMock = new Mock<PrevisaoTempoCidadeAppService>();
            _previsaoTempoCidadeAppService = _previsaoTempoCidadeAppServiceMock.Object;
        }

        [Fact]
        public async Task ConsigoBuscarPrevisaoTempoCidade()
        {
            _previsaoTempoCidadeAppServiceMock
                .Setup(p => p.BuscarAsync<PrevisaoTempoDTO>(It.IsAny<string>()))
                .ReturnsAsync(new PrevisaoTempoDTO
                {
                    PrevisaoTempoDataHoraDTO = new List<PrevisaoTempoDataHoraDTO>
                    {
                        new PrevisaoTempoDataHoraDTO
                        {
                            Temperatura = new TemperaturaDTO
                            {
                                TemperaturaMinima = 10.05,
                                TemperaturaMaxima = 20.35,
                                Umidade = 70
                            },
                            Clima = new List<ClimaDTO>
                            {
                                new ClimaDTO
                                {
                                    Descricao = "nublado",
                                    Icone = "teste"
                                },
                                new ClimaDTO
                                {
                                    Descricao = "chuva fraca",
                                    Icone = "teste2"
                                }
                            },
                            DataHora = new DateTime(2019, 8, 1, 18, 0, 0)
                        },
                        new PrevisaoTempoDataHoraDTO
                        {
                            Temperatura = new TemperaturaDTO
                            {
                                TemperaturaMinima = 13,
                                TemperaturaMaxima = 19,
                                Umidade = 50
                            },
                            Clima = new List<ClimaDTO>
                            {
                                new ClimaDTO
                                {
                                    Descricao = "nuvens dispersas",
                                    Icone = "teste3"
                                }
                            },
                            DataHora = new DateTime(2019, 8, 1, 21, 0, 0)
                        }
                    }
                });

            var previsao = await _previsaoTempoCidadeAppService.BuscarPrevisaoTempoCidadeAsync("123");

            Assert.NotNull(previsao);
            Assert.Single(previsao);

            var prev = previsao.First();
            Assert.Equal("quinta-feira, 01 agosto", prev.Data);
            Assert.Equal(2, prev.PrevisaoHoras.Count);

            var prevHora = prev.PrevisaoHoras.First();
            Assert.Equal("15:00", prevHora.Hora);
            Assert.Equal("nublado", prevHora.Descricao);
            Assert.Equal("teste", prevHora.Icone);
            Assert.Equal(10.05, prevHora.TemperaturaMinima);
            Assert.Equal(20.35, prevHora.TemperaturaMaxima);
            Assert.Equal(70, prevHora.Umidade);

            prevHora = prev.PrevisaoHoras.Last();
            Assert.Equal("18:00", prevHora.Hora);
            Assert.Equal("nuvens dispersas", prevHora.Descricao);
            Assert.Equal("teste3", prevHora.Icone);
            Assert.Equal(13, prevHora.TemperaturaMinima);
            Assert.Equal(19, prevHora.TemperaturaMaxima);
            Assert.Equal(50, prevHora.Umidade);

            _previsaoTempoCidadeAppServiceMock.Verify(p => p.BuscarAsync<PrevisaoTempoDTO>(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ConsigoBuscarBuscarCidadesPeloNome()
        {
            _previsaoTempoCidadeAppServiceMock
                .Setup(p => p.BuscarAsync<CidadesDTO>(It.IsAny<string>()))
                .ReturnsAsync(new CidadesDTO
                {
                    Cidades = new List<CidadeDTO>
                    {
                        new CidadeDTO
                        {
                            OpenWeatherId = "123",
                            Nome = "Blumenau",
                            Coordenadas = new CoordenadasDTO
                            {
                                Latitude = 12.44,
                                Longitude = 55.8
                            },
                            Pais = new PaisDTO
                            {
                                Sigla = "BR"
                            }
                        },
                        new CidadeDTO
                        {
                            OpenWeatherId = "555",
                            Nome = "Blumenau",
                            Coordenadas = new CoordenadasDTO
                            {
                                Latitude = 45,
                                Longitude = 29.79
                            },
                            Pais = new PaisDTO
                            {
                                Sigla = "DE"
                            }
                        }
                    }
                });

            var cidades = await _previsaoTempoCidadeAppService.BuscarCidadesPeloNomeAsync("blumenau");

            Assert.NotNull(cidades);
            Assert.Equal(2, cidades.Count);

            var cidade = cidades.First();
            Assert.Equal(0, cidade.Id);
            Assert.Equal("123", cidade.OpenWeatherId);
            Assert.Equal("Blumenau", cidade.Nome);
            Assert.Equal(12.44, cidade.Latitude);
            Assert.Equal(55.8, cidade.Longitude);
            Assert.Equal("BR", cidade.Pais);

            cidade = cidades.Last();
            Assert.Equal(0, cidade.Id);
            Assert.Equal("555", cidade.OpenWeatherId);
            Assert.Equal("Blumenau", cidade.Nome);
            Assert.Equal(45, cidade.Latitude);
            Assert.Equal(29.79, cidade.Longitude);
            Assert.Equal("DE", cidade.Pais);

            _previsaoTempoCidadeAppServiceMock.Verify(p => p.BuscarAsync<CidadesDTO>(It.IsAny<string>()), Times.Once);
        }
    }
}
