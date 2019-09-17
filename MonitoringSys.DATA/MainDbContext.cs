using Microsoft.EntityFrameworkCore;
using MonitoringSys.Models;
using System;

namespace MonitoringSys.DATA
{
    public class MainDbContext : DbContext
    {
        public MainDbContext() : base()
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Contact> Contacts { get; set; }


    }
}
