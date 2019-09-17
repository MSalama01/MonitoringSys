using System;

namespace MonitoringSys.Models
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}