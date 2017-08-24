using BanagazonWorkforceManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanagazonWorkforceManager.ViewModels
{
    public class EmployeeEdit
    {
        public Employee Employee { get; set; }
        
        public int ComputerID { get; set; }

        public int OldComputerID { get; set; }

        public int OldEmployeeComputerID { get; set; }

        public DateTime OldDateAssigned { get; set; }

        public int TrainingProgramID { get; set; }
    }
}
