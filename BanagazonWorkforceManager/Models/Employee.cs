using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BanagazonWorkforceManager.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(40)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        public int DepartmentID { get; set; }

        public Department Department { get; set; }

        public ICollection<EmployeeComputer> EmployeeComputers;

        public ICollection<EmployeeTraining> EmployeeTrainingPrograms;
    }
}
