using PrevisaoTempo.Aplicacao.Interface;
using PrevisaoTempo.Dominio.Interfaces.Servicos;

namespace PrevisaoTempo.Aplicacao.Servicos
{
    public class BaseAppService<TEntidade> : IBaseAppService<TEntidade>
        where TEntidade : class
    {
        private readonly IBaseService<TEntidade> _baseService;

        public BaseAppService(IBaseService<TEntidade> baseService)
        {
            _baseService = baseService;
        }

        public void Adicionar(TEntidade entidade)
        {
            _baseService.Adicionar(entidade);
        }

        public void Excluir(TEntidade entidade)
        {
            _baseService.Excluir(entidade);
        }

        public TEntidade BuscarPorId(long id)
        {
            return _baseService.BuscarPorId(id);
        }
    }
}
