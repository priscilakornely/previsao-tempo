using PrevisaoTempo.Aplicacao.ViewModels;
using PrevisaoTempo.Dominio.Entidades;
using System.Collections.Generic;

namespace PrevisaoTempo.Aplicacao.Interface
{
    public interface ICidadeAppService : IBaseAppService<Cidade>
    {
        List<CidadeViewModel> BuscarTodas();

        void Adicionar(CidadeViewModel cidadeVM);
    }
}
