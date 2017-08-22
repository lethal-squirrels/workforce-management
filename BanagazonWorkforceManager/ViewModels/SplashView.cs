using BanagazonWorkforceManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BanagazonWorkforceManager.ViewModels
{
    public class SplashView
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public IEnumerable<Employee> Employees { get; set; }

        public Department Department { get; set; }

        [Display(Name = "Training Programs")]
        public IEnumerable<TrainingProgram> TrainingPrograms { get; set; }

    }
}
