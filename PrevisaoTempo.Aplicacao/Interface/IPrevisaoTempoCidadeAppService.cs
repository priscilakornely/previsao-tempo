using PrevisaoTempo.Aplicacao.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrevisaoTempo.Aplicacao.Interface
{
    public interface IPrevisaoTempoCidadeAppService
    {
        Task<List<CidadeViewModel>> BuscarCidadesPeloNomeAsync(string nome);

        Task<List<PrevisaoTempoViewModel>> BuscarPrevisaoTempoCidadeAsync(string id);
    }
}
