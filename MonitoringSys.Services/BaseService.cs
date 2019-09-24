using Microsoft.EntityFrameworkCore;
using MonitoringSys.Models;
using MonitoringSys.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MonitoringSys.Services
{

    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly IBaseRepository<TEntity> _repository;
  
        public BaseService(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<TEntity>();
        }


        public virtual async Task<bool> Add(TEntity entity)
        {
            return await _repository.Add(entity);
        }
        public virtual async Task<bool> Update(TEntity entity)
        {
            return await _repository.Update(entity);
        }
        public virtual async Task<bool> Delete(TEntity entity)
        {
            return await _repository.Delete(entity);
        }
        public virtual async Task<TEntity> Get(object id, params Expression<Func<TEntity, object>>[] includes)
        {
            return await _repository.Get(id, includes);
        }
        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> SearchBy, params Expression<Func<TEntity, object>>[] includes)
        {
            return await _repository.Get(SearchBy, includes);
        }
        public bool Any(object id)
        {
            return _repository.Get(id).Result is null ? false : true;
        }
        public virtual long Count(Expression<Func<TEntity, bool>> searchBy)
        {
            return _repository.GetAll(searchBy).Count();
        }
        public virtual long Count()
        {
            return _repository.GetAll().Count();
        }
        public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> searchBy)
        {
            return await _repository.Get(searchBy);
        }
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _repository.GetAll().ToListAsync();
        }
        public virtual async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> searchBy)
        {
            return await _repository.GetAll(searchBy).ToListAsync();
        }
        public virtual async Task<IEnumerable<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            return await _repository.GetAll(includes).ToListAsync();
        }
        public virtual async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> searchBy, params Expression<Func<TEntity, object>>[] includes)
        {
            return await _repository.GetAll(searchBy, includes).ToListAsync();
        }

       
    }
}