using MonitoringSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MonitoringSys.Repositories
{

    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<bool> Add(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<TEntity> Get(
                object Id,
                params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> Get(
             Expression<Func<TEntity, bool>> searchBy,
             params Expression<Func<TEntity, object>>[] includes);

        IQueryable<TEntity> GetAll(
                params Expression<Func<TEntity, object>>[] includes);

        //Task<IQueryable<TEntity>> GetAll(
        //        Expression<Func<TEntity, bool>> predicate,
        //        params Expression<Func<TEntity, object>>[] includes);


        IQueryable<TEntity> GetAll(
                Expression<Func<TEntity, bool>> searchBy,
                params Expression<Func<TEntity, object>>[] includes);
    }
}
