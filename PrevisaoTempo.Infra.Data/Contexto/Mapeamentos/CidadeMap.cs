using PrevisaoTempo.Dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace PrevisaoTempo.Infra.Data.Contexto.Mapeamentos
{
    public class CidadeMap : EntityTypeConfiguration<Cidade>
    {
        public CidadeMap()
        {
            HasKey(c => c.Id);

            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.OpenWeatherId)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("Index") { IsUnique = true } }));

            Property(t => t.Nome)
                .HasMaxLength(100)
                .IsRequired();

            Property(t => t.Latitude)
                .IsRequired();

            Property(t => t.Longitude)
                .IsRequired();

            Property(t => t.Pais)
                .HasMaxLength(2)
                .IsRequired();
        }
    }
}
