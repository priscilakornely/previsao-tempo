using Newtonsoft.Json;
using System.Collections.Generic;

namespace PrevisaoTempo.Aplicacao.DTOs.OpenWeather
{
    public class CidadesDTO
    {
        [JsonProperty("list")]
        public List<CidadeDTO> Cidades { get; set; }
    }
}
