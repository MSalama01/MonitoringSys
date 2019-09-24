using MonitoringSys.DATA;
using MonitoringSys.Models;
using System;

namespace MonitoringSys.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        MainDbContext _Context { get; }
        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseEntity;
        void Save();
    }
}
