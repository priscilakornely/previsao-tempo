using Newtonsoft.Json;
using System.Collections.Generic;

namespace PrevisaoTempo.Aplicacao.DTOs.OpenWeather
{
    public class PrevisaoTempoDTO
    {
        [JsonProperty("list")]
        public List<PrevisaoTempoDataHoraDTO> PrevisaoTempoDataHoraDTO { get; set; }
    }
}
