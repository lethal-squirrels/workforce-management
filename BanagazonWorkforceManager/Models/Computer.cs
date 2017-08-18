using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BanagazonWorkforceManager.Models
{
    public class Computer
    {
        [Key]
        public int ComputerID { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DatePurchased { get; set; }

        public virtual ICollection<EmployeeComputer> EmployeeComputers { get; set; }
    }
}