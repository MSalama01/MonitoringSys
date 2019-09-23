using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MonitoringSys.Services
{   
    public interface IBaseService<TEntity>  where TEntity : class
    {
        Task<bool> Add(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<TEntity> Get(object id, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> SearchBy, params Expression<Func<TEntity, object>>[] includes);
        bool Any(object id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> SearchBy, params Expression<Func<TEntity, object>>[] includes);
        long Count(Expression<Func<TEntity, bool>> SearchBy);
        long Count();
    }
}
