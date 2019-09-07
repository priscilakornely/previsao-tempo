using PrevisaoTempo.Dominio.Entidades;
using PrevisaoTempo.Dominio.Interfaces.Repositorios;
using PrevisaoTempo.Dominio.Interfaces.Servicos;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PrevisaoTempo.Dominio.Servicos
{
    public class CidadeService : BaseService<Cidade>, ICidadeService
    {
        private ICidadeRepository _cidadeRepository;

        public CidadeService(ICidadeRepository cidadeRepository)
            : base(cidadeRepository)
        {
            _cidadeRepository = cidadeRepository;
        }

        public override void Adicionar(Cidade entidade)
        {
            if (_cidadeRepository.Buscar(c => c.OpenWeatherId == entidade.OpenWeatherId).Any())
                throw new ValidationException("Cidade já cadastrada.");

            base.Adicionar(entidade);
        }
    }
}
