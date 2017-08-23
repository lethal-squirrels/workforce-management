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
        [Display(Name = "Computer Make")]
        public string Make { get; set; }

        [Required]
        [Display(Name = "Computer Manufacturer")]
        public string Manufacturer { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Purchased")]
        public DateTime DatePurchased { get; set; }

        //for info from join tables - need virtual icollection WITH a {get; set;}
        public virtual ICollection<EmployeeComputer> EmployeeComputers { get; set; }
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            