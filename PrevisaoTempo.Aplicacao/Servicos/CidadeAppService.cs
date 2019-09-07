using PrevisaoTempo.Aplicacao.Interface;
using PrevisaoTempo.Aplicacao.ViewModels;
using PrevisaoTempo.Dominio.Entidades;
using PrevisaoTempo.Dominio.Interfaces.Servicos;
using System.Collections.Generic;
using System.Linq;

namespace PrevisaoTempo.Aplicacao.Servicos
{
    public class CidadeAppService : BaseAppService<Cidade>, ICidadeAppService
    {
        private readonly ICidadeService _cidadeService;

        public CidadeAppService(ICidadeService cidadeService)
            : base(cidadeService)
        {
            _cidadeService = cidadeService;
        }

        public virtual List<CidadeViewModel> BuscarTodas()
        {
            return _cidadeService.BuscarTodos()
                .Select(c => new CidadeViewModel
                {
                    Id = c.Id,
                    OpenWeatherId = c.OpenWeatherId,
                    Nome = c.Nome,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude,
                    Pais = c.Pais
                }).ToList();
        }

        public virtual void Adicionar(CidadeViewModel cidadeVM)
        {
            var entidade = new Cidade
            {
                OpenWeatherId = cidadeVM.OpenWeatherId,
                Nome = cidadeVM.Nome,
                Latitude = cidadeVM.Latitude,
                Longitude = cidadeVM.Longitude,
                Pais = cidadeVM.Pais
            };

            base.Adicionar(entidade);
        }
    }
}
