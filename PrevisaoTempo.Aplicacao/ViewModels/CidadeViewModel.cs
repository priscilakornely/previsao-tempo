namespace PrevisaoTempo.Aplicacao.ViewModels
{
    public class CidadeViewModel
    {
        public long Id { get; set; }

        public string OpenWeatherId { get; set; }

        public string Nome { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Pais { get; set; }
    }
}