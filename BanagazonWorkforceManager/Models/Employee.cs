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
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        public int DepartmentID { get; set; }

        public Department Department { get; set; }

        public Computer Computer { get; set; }

        [Display(Name = "Computer")]
        public virtual ICollection<EmployeeComputer> EmployeeComputers { get; set; }

        public virtual ICollection<EmployeeTraining> EmployeeTrainingPrograms { get; set; }
    }
}
