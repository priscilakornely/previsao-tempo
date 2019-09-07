using PrevisaoTempo.Dominio.Interfaces.Repositorios;
using PrevisaoTempo.Dominio.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PrevisaoTempo.Dominio.Servicos
{
    public class BaseService<TEntidade> : IBaseService<TEntidade>
        where TEntidade : class
    {
        private readonly IBaseRepository<TEntidade> _repository;

        public BaseService(IBaseRepository<TEntidade> repository)
        {
            _repository = repository;
        }

        public virtual void Adicionar(TEntidade entidade)
        {
            _repository.Adicionar(entidade);
        }

        public void Excluir(TEntidade entidade)
        {
            _repository.Excluir(entidade);
        }

        public TEntidade BuscarPorId(long id)
        {
            return _repository.BuscarPorId(id);
        }

        public IEnumerable<TEntidade> BuscarTodos()
        {
            return _repository.BuscarTodos();
        }

        public IEnumerable<TEntidade> Buscar(Expression<Func<TEntidade, bool>> exp)
        {
            return _repository.Buscar(exp);
        }
    }
}
