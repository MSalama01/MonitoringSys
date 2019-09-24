using MonitoringSys.DATA;
using MonitoringSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonitoringSys.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public MainDbContext _Context { get; }
        private readonly Dictionary<Type, object> _Repositories;
        private bool _disposed;


        public UnitOfWork(MainDbContext context)
        {
            _Context = context;
            _Repositories = new Dictionary<Type, object>();
            _disposed = false;
        }

        public void Save()
        {
            try
            {
                _Context.SaveChanges();
            }
            catch(Exception Ex)
            {
                //Log Exception Here ...
                Console.WriteLine(Ex);
            }
        }
        public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class,IBaseEntity
        {
            if (_Repositories.Keys.Contains(typeof(TEntity)))
                return _Repositories[typeof(TEntity)] as IBaseRepository<TEntity>;

            var repository = new BaseRepository<TEntity>(_Context);
            _Repositories.Add(typeof(TEntity), repository);
            return repository;
        }




        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            _Context.Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _Context.Dispose();
                }

                this._disposed = true;
            }
        }

       
    }
}
