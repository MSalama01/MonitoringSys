using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MonitoringSys.Services
{
    public interface IService<TEntity> where TEntity : class
    {
        Task<bool> Add(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<TEntity> Get(object id);
        bool Any(object id);

        Task<TEntity> Get(Expression<Func<TEntity, bool>> SearchBy);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includes);
        //Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> SearchBy);
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> SearchBy, params Expression<Func<TEntity, object>>[] includes);
        long Count(Expression<Func<TEntity, bool>> SearchBy);
        long Count();
    }
}
