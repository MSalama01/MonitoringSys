using Microsoft.EntityFrameworkCore;
using MonitoringSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MonitoringSys.Repositories
{
   
    public interface IRepository<TEntity> where TEntity : class
    {

        Task<bool> Add(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<IEnumerable<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> SearchBy(Expression<Func<TEntity, bool>> searchBy, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;
        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<bool> Add(TEntity entity)
        {
            try
            {
                _unitOfWork.Context.Set<TEntity>().Add(entity);
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> Update(TEntity entity)
        {
            try
            {
                TEntity existing = _unitOfWork.Context.Set<TEntity>().Find(entity);
                if (existing is null)
                    return await Task.FromResult(false);

                _unitOfWork.Context.Entry(entity).State = EntityState.Modified;
                _unitOfWork.Context.Set<TEntity>().Attach(entity);

                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }

        }


        public virtual async Task<bool> Delete(TEntity entity)
        {
            try
            {
                TEntity existing = _unitOfWork.Context.Set<TEntity>().Find(entity);
                if (existing is null)
                    return await Task.FromResult(false);

                _unitOfWork.Context.Set<TEntity>().Remove(existing);
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }


        public virtual async Task<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var result = _unitOfWork.Context.Set<TEntity>().Where(predicate);

            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);


            return await result.FirstOrDefaultAsync();
        }

        

        public virtual async Task<IEnumerable<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            var result = _unitOfWork.Context.Set<TEntity>().Where(i => true);

            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);

            return await result.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> SearchBy(Expression<Func<TEntity, bool>> searchBy, params Expression<Func<TEntity, object>>[] includes)
        {
            var result = _unitOfWork.Context.Set<TEntity>().Where(searchBy);

            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);

            return await result.ToListAsync();
        }


        //
        //public IEnumerable<TEntity> Get()
        //{
        //    return _unitOfWork.Context.Set<TEntity>().AsEnumerable<TEntity>();
        //}

        //public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        //{
        //    return _unitOfWork.Context.Set<TEntity>().Where(predicate).AsEnumerable<TEntity>();
        //}

        //public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        //{
        //    return _unitOfWork.Context.Set<TEntity>().Where(predicate).AsQueryable<TEntity>();
        //}

        //public void Update(TEntity entity)
        //{
        //    _unitOfWork.Context.Entry(entity).State = EntityState.Modified;
        //    _unitOfWork.Context.Set<TEntity>().Attach(entity);
        //}
    }

    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }
        void Commit();
    }

    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }
        public void Commit()
        {
            try
            {
                Context.SaveChanges();
            }
            catch
            { }
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }

}
