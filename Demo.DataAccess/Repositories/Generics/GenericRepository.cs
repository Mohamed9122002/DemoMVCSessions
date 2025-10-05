using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Generics
{
    public class GenericRepository<TEntity>(ApplicationDbContext dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        // get All 
        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if (withTracking)
                return _dbContext.Set<TEntity>().Where(E=>E.IsDeleted !=true).ToList();
            else
                return _dbContext.Set<TEntity>().Where(E => E.IsDeleted != true).AsNoTracking().ToList();
        }

        public TEntity? GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }
        // Updated 
        public void Update(TEntity  entity)
        { 
            _dbContext.Set<TEntity>().Update(entity);

        }
        // Delete 
        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);

        }
        // Insert 
        public void Add(TEntity  entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
           return _dbContext.Set<TEntity>().Where(predicate).ToList();
        }
    }
}
