using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BanagazonWorkforceManager.Models
{
    public class EmployeeComputer
    {
        [Key]
        public int EmployeeComputerID { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        [Required]
        public int ComputerID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Computer Computer { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateAssigned { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateUnassigned { get; set; }

    }
}