namespace PrevisaoTempo.Infra.Data.Migrations
{
    using PrevisaoTempo.Infra.Data.Contexto;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<PrevisaoTempoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PrevisaoTempoContext context)
        {
        }
    }
}
