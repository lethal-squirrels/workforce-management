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
        //public IEnumerable<SelectListItem> TrainingPrograms { get; set; }
        public int SelectedComputerID { get; set; }
    }
}
