using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace infrawebapp
{
    public class InfraDBContext : DbContext
    {
        public InfraDBContext(DbContextOptions<InfraDBContext> options)
            : base(options)
        {
        }

        public DbSet<DummyTable> DummyTables { get; set; }
    }

    public class DummyTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
