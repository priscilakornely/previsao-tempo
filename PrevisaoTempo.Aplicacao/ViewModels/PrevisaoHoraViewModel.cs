namespace PrevisaoTempo.Aplicacao.ViewModels
{
    public class PrevisaoHoraViewModel
    {
        public string Hora { get; set; }

        public string Icone { get; set; }

        public string Descricao { get; set; }

        public double TemperaturaMinima { get; set; }

        public double TemperaturaMaxima { get; set; }

        public int Umidade { get; set; }
    }
}