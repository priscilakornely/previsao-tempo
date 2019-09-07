using System.Collections.Generic;

namespace PrevisaoTempo.Aplicacao.ViewModels
{
    public class PrevisaoTempoViewModel
    {
        public string Data { get; set; }

        public List<PrevisaoHoraViewModel> PrevisaoHoras { get; set; }
    }
}