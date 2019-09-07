using PrevisaoTempo.Dominio.Entidades;
using PrevisaoTempo.Dominio.Interfaces.Repositorios;
using PrevisaoTempo.Infra.Data.Contexto;

namespace PrevisaoTempo.Infra.Data.Repositorios
{
    public class CidadeRepository : BaseRepository<Cidade>, ICidadeRepository
    {
        public CidadeRepository(PrevisaoTempoContext context)
            : base(context)
        {
        }
    }
}
