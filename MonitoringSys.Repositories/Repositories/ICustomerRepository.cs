using Microsoft.EntityFrameworkCore;
using MonitoringSys.DATA;
using MonitoringSys.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringSys.Repositories
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
    }
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository 
    {
        public CustomerRepository(MainDbContext dbContext) : base(dbContext)
        {
        }
    }
}