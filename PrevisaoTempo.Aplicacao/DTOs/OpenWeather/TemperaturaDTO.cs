using Newtonsoft.Json;

namespace PrevisaoTempo.Aplicacao.DTOs.OpenWeather
{
    public class TemperaturaDTO
    {
        [JsonProperty("temp_min")]
        public double TemperaturaMinima { get; set; }

        [JsonProperty("temp_max")]
        public double TemperaturaMaxima { get; set; }

        [JsonProperty("humidity")]
        public int Umidade { get; set; }
    }
}
