using Demo.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Generics
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        void Add(TEntity TEntity);
        IEnumerable<TEntity> GetAll(bool withTracking = false);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity ,bool>> predicate);

        TEntity? GetById(int id);
        void Remove(TEntity TEntity);
        void Update(TEntity TEntity);
    }
}
