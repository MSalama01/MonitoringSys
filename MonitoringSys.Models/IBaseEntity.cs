using System;

namespace MonitoringSys.Models
{
    public interface IBaseEntity
    {

    }
    public interface IBaseEntity<T> : IBaseEntity
    {
        public T Id { get; set; }
    }

}