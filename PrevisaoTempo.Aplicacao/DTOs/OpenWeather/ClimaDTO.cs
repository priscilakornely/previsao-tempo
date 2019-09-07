using Newtonsoft.Json;

namespace PrevisaoTempo.Aplicacao.DTOs.OpenWeather
{
    public class ClimaDTO
    {
        [JsonProperty("description")]
        public string Descricao { get; set; }

        [JsonProperty("icon")]
        public string Icone { get; set; }
    }
}
