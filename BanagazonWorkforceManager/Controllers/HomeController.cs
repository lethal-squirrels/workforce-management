using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BanagazonWorkforceManager.Models;
using Microsoft.EntityFrameworkCore;
using BanagazonWorkforceManager.ViewModels;

namespace BanagazonWorkforceManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly BanagazonWorkforceManagerContext _context;

        public HomeController(BanagazonWorkforceManagerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Creates an instance of the SplashView View Model
            var splash = new SplashView();
            // Grabs all Training Programs and puts them in a list
            var trainingPrograms = await _context.TrainingProgram.ToListAsync();
            // Creates an intance to hold only training programs that are within 4 weeks of Datetime Now
            var trainingProgramsWithinFourWeeks = new List<TrainingProgram>();

            // Loops through all training programs in the database
            foreach (var tp in trainingPrograms)
            {
                // Selects only training programs that start between Dattime Now and 28 days from Datetime Now
                if(tp.StartDate <= DateTime.Now.AddDays(28) && tp.StartDate > DateTime.Now)
                {
                    trainingProgramsWithinFourWeeks.Add(tp);
                }
            }
            // Sets the ViewModel object equal to the Selected List of Training Programs
            splash.TrainingPrograms = trainingProgramsWithinFourWeeks;
            // Grabs all the Employees from the database, Includes their department Orders by the Last added and takes only 5
            splash.Employees = await _context.Employee.Include(e => e.Department).OrderByDescending(e => e.StartDate).Take(5).ToListAsync();
            // Returns the View Model object to the razor template
            return View(splash);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
