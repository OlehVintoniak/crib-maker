﻿using System;
using System.Linq;
using CribMaker.Core.Data.Entities.Abstract;

namespace CribMaker.Core.Repositories.Abstract
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : IEntity
    {
        IQueryable<TEntity> GetAll();
        TEntity Find(object id);
        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        TEntity Delete(TEntity entity);
        TEntity DeleteById(object id);
    }
}