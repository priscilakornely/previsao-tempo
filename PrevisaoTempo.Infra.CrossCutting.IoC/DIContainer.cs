using PrevisaoTempo.Aplicacao.Interface;
using PrevisaoTempo.Aplicacao.Servicos;
using PrevisaoTempo.Dominio.Interfaces.Repositorios;
using PrevisaoTempo.Dominio.Interfaces.Servicos;
using PrevisaoTempo.Dominio.Servicos;
using PrevisaoTempo.Infra.Data.Repositorios;
using SimpleInjector;

namespace PrevisaoTempo.Infra.CrossCutting.IoC
{
    public static class DIContainer
    {
        public static Container RegisterDependencies()
        {
            var container = new Container();

            container.Register(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            container.Register<ICidadeRepository, CidadeRepository>();

            container.Register(typeof(IBaseService<>), typeof(BaseService<>));
            container.Register<ICidadeService, CidadeService>();

            container.Register(typeof(IBaseAppService<>), typeof(BaseAppService<>));
            container.Register<ICidadeAppService, CidadeAppService>();

            container.Register<IPrevisaoTempoCidadeAppService, PrevisaoTempoCidadeAppService>();

            return container;
        }
    }
}
