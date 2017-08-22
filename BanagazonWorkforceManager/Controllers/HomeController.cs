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
            var splash = new SplashView();

            var trainingPrograms = await _context.TrainingProgram.ToListAsync();
            var trainingProgramsWithinFour = new List<TrainingProgram>();


            foreach (var tp in trainingPrograms)
            {
                if(tp.StartDate <= DateTime.Now.AddDays(28) && tp.StartDate > DateTime.Now)
                {
                    trainingProgramsWithinFour.Add(tp);
                }
            }
            splash.TrainingPrograms = trainingProgramsWithinFour;

            splash.Employees = await _context.Employee.Include(e => e.Department).OrderByDescending(e => e.StartDate).Take(5).ToListAsync();

            

            return View(splash);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
