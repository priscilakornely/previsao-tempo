using Newtonsoft.Json;

namespace PrevisaoTempo.Aplicacao.DTOs.OpenWeather
{
    public class CoordenadasDTO
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }
    }
}
