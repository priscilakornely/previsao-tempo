using PrevisaoTempo.Infra.Data.Contexto.Mapeamentos;
using PrevisaoTempo.Dominio.Entidades;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PrevisaoTempo.Infra.Data.Contexto
{
    public class PrevisaoTempoContext : DbContext
    {
        public PrevisaoTempoContext()
            : base("PrevisaoTempo")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<PrevisaoTempoContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            _ = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new CidadeMap());
        }

        public DbSet<Cidade> Cidades { get; set; }
    }
}
