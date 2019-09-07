namespace PrevisaoTempo.Aplicacao.Interface
{
    public interface IBaseAppService<TEntidade>
        where TEntidade : class
    {
        void Adicionar(TEntidade entidade);

        void Excluir(TEntidade entidade);

        TEntidade BuscarPorId(long id);
    }
}
