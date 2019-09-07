using Newtonsoft.Json;

namespace PrevisaoTempo.Aplicacao.DTOs.OpenWeather
{
    public class PaisDTO
    {
        [JsonProperty("country")]
        public string Sigla { get; set; }
    }
}
