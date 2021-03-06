﻿using Microsoft.EntityFrameworkCore;
using MonitoringSys.DATA;
using MonitoringSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MonitoringSys.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly MainDbContext _dbContext;
        public BaseRepository(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<bool> Add(TEntity entity)
        {
            try
            {
                _dbContext.Set<TEntity>().Add(entity);
                return await _dbContext.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception Ex)
            {
                //Log Exception Here
                Console.WriteLine(Ex);
                return await Task.FromResult(false);
            }
        }

        public virtual async Task<bool> Update(TEntity entity)
        {
            try
            {
                _dbContext.Set<TEntity>().Attach(entity);
                var entry = _dbContext.Entry(entity);
                entry.State = EntityState.Modified;
                return await _dbContext.SaveChangesAsync() > 0 ? true : false;


                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.Set<TEntity>().Attach(entity);
                return await _dbContext.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception Ex)
            {
                return await Task.FromResult(false);
            }

        }


        public virtual async Task<bool> Delete(TEntity entity)
        {
            try
            {
                TEntity existing = _dbContext.Set<TEntity>().Find(entity);
                if (existing is null)
                    return await Task.FromResult(false);

                _dbContext.Set<TEntity>().Remove(existing);
                return await _dbContext.SaveChangesAsync() > 0 ? true : false;
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<TEntity> Get(
                object Id,
                params Expression<Func<TEntity, object>>[] includes)
        {
            var result = _dbContext.Set<TEntity>().Where(a => true).AsQueryable();

            foreach (var includeExpression in includes)
                result = result.Include(includeExpression).AsQueryable();

            return await _dbContext.Set<TEntity>().FindAsync(Id);
        }

        public async Task<TEntity> Get(
            Expression<Func<TEntity, bool>> searchBy,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var result = _dbContext.Set<TEntity>().Where(searchBy).AsQueryable();

            foreach (var includeExpression in includes)
                result = result.Include(includeExpression).AsQueryable();

            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync();
        }

        public virtual IQueryable<TEntity> GetAll(
            params Expression<Func<TEntity, object>>[] includes)
        {
            var result = _dbContext.Set<TEntity>().Where(i => true).AsQueryable();

            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);

            return result;
        }

        public virtual IQueryable<TEntity> GetAll(
                Expression<Func<TEntity, bool>> searchBy,
                params Expression<Func<TEntity, object>>[] includes)
        {
            var result = _dbContext.Set<TEntity>().Where(searchBy);

            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);

            return result;
        }


    }
}