using BanagazonWorkforceManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanagazonWorkforceManager.ViewModels
{
    public class EmployeeDetail
    {
        public Employee Employee { get; set; }
        public IEnumerable<EmployeeComputer> EmployeeComputers { get; set; } 
        public IEnumerable<Computer> Computer { get; set; }
    }
}
