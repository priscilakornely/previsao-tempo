﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PrevisaoTempo.Dominio.Interfaces.Repositorios
{
    public interface IBaseRepository<TEntidade>
        where TEntidade : class
    {
        void Adicionar(TEntidade entidade);

        void Excluir(TEntidade entidade);

        TEntidade BuscarPorId(long id);

        IEnumerable<TEntidade> BuscarTodos();

        IEnumerable<TEntidade> Buscar(Expression<Func<TEntidade, bool>> exp);
    }
}
