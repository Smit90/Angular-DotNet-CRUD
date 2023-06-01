using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestAngularAPI.Models;

namespace TestAngularAPI.Data
{
    public class TestCRUDDbContext : DbContext
    {
        public TestCRUDDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

    }
}