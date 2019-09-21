using MonitoringSys.DATA;
using MonitoringSys.Models;
using System;

namespace MonitoringSys.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        MainDbContext Context { get; }
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseEntity;
        void Save();
    }
}
