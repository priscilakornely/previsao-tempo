using PrevisaoTempo.Dominio.Interfaces.Repositorios;
using PrevisaoTempo.Infra.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PrevisaoTempo.Infra.Data.Repositorios
{
    public class BaseRepository<TEntidade> : IBaseRepository<TEntidade>
        where TEntidade : class
    {
        private readonly PrevisaoTempoContext _context;

        public BaseRepository(PrevisaoTempoContext context)
        {
            _context = context;
        }

        public void Adicionar(TEntidade entidade)
        {
            _context.Set<TEntidade>().Add(entidade);
            _context.SaveChanges();
        }

        public void Excluir(TEntidade entidade)
        {
            _context.Set<TEntidade>().Remove(entidade);
            _context.SaveChanges();
        }

        public TEntidade BuscarPorId(long id)
        {
            return _context.Set<TEntidade>().Find(id);
        }

        public IEnumerable<TEntidade> BuscarTodos()
        {
            return _context.Set<TEntidade>().ToList();
        }

        public IEnumerable<TEntidade> Buscar(Expression<Func<TEntidade, bool>> exp)
        {
            return _context.Set<TEntidade>().Where(exp);
        }
    }
}
