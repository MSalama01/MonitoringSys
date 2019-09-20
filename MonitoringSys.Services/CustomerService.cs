using MonitoringSys.Models;
using MonitoringSys.Repositories;
using System;

namespace MonitoringSys.Services
{
    public interface IService
    {

    }

    public class CustomerService
    {
        private readonly IUnitOfWork _uow;

        public CustomerService(IUnitOfWork unit)
        {
            _uow = unit;
        }

        public void Add(Customer customer)
        {
            _uow.GetRepository<Customer>().Add(customer);
            _uow.Save();
        }
    }
}
