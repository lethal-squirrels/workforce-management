using Microsoft.EntityFrameworkCore;

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
        public DbSet<BanagazonWorkforceManager.Models.EmployeeComputer> EmployeeComputer { get; set; }
        public DbSet<BanagazonWorkforceManager.Models.EmployeeTraining> EmployeeTraining { get; set; }
    }
}
