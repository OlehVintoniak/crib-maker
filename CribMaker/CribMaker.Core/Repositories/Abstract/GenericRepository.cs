using CribMaker.Core.Data;
using CribMaker.Core.Data.Entities.Abstract;
using System;
using System.Data.Entity;
using System.Linq;

namespace CribMaker.Core.Repositories.Abstract
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        protected DbSet<TEntity> DbSet { get; }
        protected readonly ApplicationDbContext Context;

        protected GenericRepository(ApplicationDbContext сontext)
        {
            Context = сontext;
            if (Context != null)
            {
                DbSet = Context.Set<TEntity>();
            }
        }

        protected GenericRepository(ApplicationDbContext context, DbSet<TEntity> dbSet)
        {
            Context = context;
            DbSet = dbSet;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public virtual TEntity Find(object id)
        {
            return DbSet.Find(id);
        }

        public virtual TEntity Add(TEntity entity)
        {
            return DbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual TEntity Delete(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            return DbSet.Remove(entity);
        }

        public virtual TEntity DeleteById(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            return Delete(entityToDelete);
        }

        #region IDisposable

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            _disposed = true;
        }

        #endregion
    }
}
