using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Rekrutacjaa.Models;

namespace Rekrutacjaa
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        private readonly LibraryDBContext Context;
        public Repository(LibraryDBContext context)
        {
            Context = context;
        }
        public TEntity Get(Guid id)
        {
            return Context.Set<TEntity>().Find(id);
        }
        public IList<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public IList<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return Context.Set<TEntity>().Where(expression).ToList();
        }

        public void Insert(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }
        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }


    }
}
