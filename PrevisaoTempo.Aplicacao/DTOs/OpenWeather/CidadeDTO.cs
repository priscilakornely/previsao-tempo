using Newtonsoft.Json;

namespace PrevisaoTempo.Aplicacao.DTOs.OpenWeather
{
    public class CidadeDTO
    {
        [JsonProperty("id")]
        public string OpenWeatherId { get; set; }

        [JsonProperty("name")]
        public string Nome { get; set; }

        [JsonProperty("coord")]
        public CoordenadasDTO Coordenadas { get; set; }

        [JsonProperty("sys")]
        public PaisDTO Pais { get; set; }
    }
}
