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
        public IEnumerable<TrainingProgram> SelectedTrainingPrograms { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Computer> Computers { get; set; }
        public IEnumerable<SelectListItem> TrainingPrograms { get; set; }
    }
}
