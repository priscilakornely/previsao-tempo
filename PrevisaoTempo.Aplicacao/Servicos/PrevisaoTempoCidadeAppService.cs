using Newtonsoft.Json;
using PrevisaoTempo.Aplicacao.DTOs.OpenWeather;
using PrevisaoTempo.Aplicacao.Interface;
using PrevisaoTempo.Aplicacao.ViewModels;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Data;
using System.Linq;

namespace PrevisaoTempo.Aplicacao.Servicos
{
    public class PrevisaoTempoCidadeAppService : IPrevisaoTempoCidadeAppService
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlBase;
        private readonly string _chave;

        public PrevisaoTempoCidadeAppService()
        {
            _httpClient = new HttpClient();
            _urlBase = ConfigurationManager.AppSettings.Get("OpenWeather.UrlBase");
            _chave = $"&APPID={ConfigurationManager.AppSettings.Get("OpenWeather.Chave")}&lang=pt&units=metric";
        }

        public async Task<List<PrevisaoTempoViewModel>> BuscarPrevisaoTempoCidadeAsync(string id)
        {
            var previsaoTempoDTO = await BuscarAsync<PrevisaoTempoDTO>($"{_urlBase}/forecast?id={id}{_chave}");
            var idioma = new CultureInfo("pt-BR");

            if (previsaoTempoDTO == null)
                return null;

            var previsaoTempoDiasVM = new List<PrevisaoTempoViewModel>();
            var previsaoTempoDiasDTO = previsaoTempoDTO
                .PrevisaoTempoDataHoraDTO
                .GroupBy(p => p.DataHora.ToLocalTime().Date);

            foreach (var previsaoDia in previsaoTempoDiasDTO)
            {
                var previsaoDiaVM = new PrevisaoTempoViewModel
                {
                    Data = previsaoDia.Key.ToLocalTime().ToString("dddd, dd MMMM"),
                    PrevisaoHoras = new List<PrevisaoHoraViewModel>()
                };

                var previsaoTempoDia = previsaoTempoDTO
                    .PrevisaoTempoDataHoraDTO
                    .Where(p => p.DataHora.ToLocalTime().Date == previsaoDia.Key.Date);

                foreach (var previsaoHora in previsaoTempoDia)
                {
                    var clima = previsaoHora.Clima.First();

                    previsaoDiaVM.PrevisaoHoras.Add(new PrevisaoHoraViewModel
                    {
                        Hora = previsaoHora.DataHora.ToLocalTime().ToString("HH:mm"),
                        Descricao = clima.Descricao,
                        Icone = clima.Icone,
                        TemperaturaMinima = previsaoHora.Temperatura.TemperaturaMinima,
                        TemperaturaMaxima = previsaoHora.Temperatura.TemperaturaMaxima,
                        Umidade = previsaoHora.Temperatura.Umidade
                    });
                }

                previsaoTempoDiasVM.Add(previsaoDiaVM);
            }

            return previsaoTempoDiasVM;
        }

        public async Task<List<CidadeViewModel>> BuscarCidadesPeloNomeAsync(string nome)
        {
            var cidadesDTO = (await BuscarAsync<CidadesDTO>($"{_urlBase}/find?q={nome}{_chave}")).Cidades;

            if (cidadesDTO == null)
                return null;

            return cidadesDTO
                .Select(c => new CidadeViewModel
                {
                    OpenWeatherId = c.OpenWeatherId,
                    Nome = c.Nome,
                    Latitude = c.Coordenadas.Latitude,
                    Longitude = c.Coordenadas.Longitude,
                    Pais = c.Pais.Sigla
                }).ToList();
        }

        public virtual async Task<T> BuscarAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<T>(content);

            throw new HttpRequestException($"Erro ao buscar informações: {content}");
        }
    }
}
