using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BanagazonWorkforceManager.Models;

namespace BanagazonWorkforceManager.Controllers
{
    public class TrainingProgramsController : Controller
    {
        private readonly BanagazonWorkforceManagerContext _context;

        public TrainingProgramsController(BanagazonWorkforceManagerContext context)
        {
            _context = context;    
        }

        // GET: TrainingPrograms
        public async Task<IActionResult> Index()
        {
            //Gets a list of all Training Programs
            var trainingPrograms = await _context.TrainingProgram.ToListAsync();
            // Creates a instance of an empty list of Training Promgrams
            var futureTrainingPrograms = new List<TrainingProgram>();
            //Loops through all Training Programs
                foreach (var tp in trainingPrograms)
                {
                    // Checks to see if The Start Date is in the future
                    if (tp.StartDate.Date > DateTime.Now.Date)
                        {
                            // Adds all future training programs to a futureTrainingProgram list
                            futureTrainingPrograms.Add(tp);
                        }
                }
                // Returns the list of Future Training Programs to the view
            return View(futureTrainingPrograms);
        }

        // GET: TrainingPrograms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingProgram = await _context.TrainingProgram
                .Include("EmployeeTrainingPrograms.Employee")
                .SingleOrDefaultAsync(m => m.TrainingProgramID == id);

            
            if (trainingProgram == null)
            {
                return NotFound();
            }

            return View(trainingProgram);
        }

        // GET: TrainingPrograms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrainingPrograms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrainingProgramID,Name,Description,StartDate,EndDate,MaxAttendees")] TrainingProgram trainingProgram)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingProgram);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(trainingProgram);
        }

        // GET: TrainingPrograms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingProgram = await _context.TrainingProgram.SingleOrDefaultAsync(m => m.TrainingProgramID == id);
            if (trainingProgram == null)
            {
                return NotFound();
            }
            return View(trainingProgram);
        }

        // POST: TrainingPrograms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainingProgramID,Name,Description,StartDate,EndDate,MaxAttendees")] TrainingProgram trainingProgram)
        {
            if (id != trainingProgram.TrainingProgramID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingProgram);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingProgramExists(trainingProgram.TrainingProgramID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(trainingProgram);
        }

        // GET: TrainingPrograms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingProgram = await _context.TrainingProgram
                .SingleOrDefaultAsync(m => m.TrainingProgramID == id);
            if (trainingProgram == null)
            {
                return NotFound();
            }

            return View(trainingProgram);
        }

        // POST: TrainingPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingProgram = await _context.TrainingProgram.SingleOrDefaultAsync(m => m.TrainingProgramID == id);
            _context.TrainingProgram.Remove(trainingProgram);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TrainingProgramExists(int id)
        {
            return _context.TrainingProgram.Any(e => e.TrainingProgramID == id);
        }
    }
}
