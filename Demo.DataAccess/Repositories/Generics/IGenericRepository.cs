using Demo.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Generics
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        int Add(TEntity TEntity);
        IEnumerable<TEntity> GetAll(bool withTracking = false);
        TEntity? GetById(int id);
        int Remove(TEntity TEntity);
        int Update(TEntity TEntity);
    }
}
