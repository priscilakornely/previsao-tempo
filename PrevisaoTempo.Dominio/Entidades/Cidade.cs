namespace PrevisaoTempo.Dominio.Entidades
{
    public class Cidade
    {
        public long Id { get; set; }

        public string OpenWeatherId { get; set; }

        public string Nome { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Pais { get; set; }
    }
}
