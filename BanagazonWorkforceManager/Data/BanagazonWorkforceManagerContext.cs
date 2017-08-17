using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BanagazonWorkforceManager.Models;

namespace BanagazonWorkforceManager.Models
{
    public class BanagazonWorkforceManagerContext : DbContext
    {
        public BanagazonWorkforceManagerContext (DbContextOptions<BanagazonWorkforceManagerContext> options)
            : base(options)
        {
        }

        public DbSet<BanagazonWorkforceManager.Models.Employee> Employee { get; set; }

        public DbSet<BanagazonWorkforceManager.Models.Computer> Computer { get; set; }

        public DbSet<BanagazonWorkforceManager.Models.TrainingProgram> TrainingProgram { get; set; }

        public DbSet<BanagazonWorkforceManager.Models.Department> Department { get; set; }
    }
}
