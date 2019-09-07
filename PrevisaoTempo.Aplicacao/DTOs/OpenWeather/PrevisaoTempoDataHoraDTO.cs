using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PrevisaoTempo.Aplicacao.DTOs.OpenWeather
{
    public class PrevisaoTempoDataHoraDTO
    {
        [JsonProperty("main")]
        public TemperaturaDTO Temperatura { get; set; }

        [JsonProperty("weather")]
        public List<ClimaDTO> Clima { get; set; }

        [JsonProperty("dt_txt")]
        public DateTime DataHora { get; set; }
    }
}
