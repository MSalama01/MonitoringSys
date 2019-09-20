using MonitoringSys.DATA;
using System;

namespace MonitoringSys.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        MainDbContext Context { get; }
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void Save();
    }
}
