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
    public class ComputersController : Controller
    {
        private readonly BanagazonWorkforceManagerContext _context;

        public ComputersController(BanagazonWorkforceManagerContext context)
        {
            _context = context;    
        }

        // GET: Computers
        //Gets list of all computers in the Computers table
        public async Task<IActionResult> Index()
        {
            return View(await _context.Computer.ToListAsync());
        }

        // GET: Computers/Details/5
        //Gets computer based off ID and passes it to the details view 

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computer = await _context.Computer
                .SingleOrDefaultAsync(m => m.ComputerID == id);
            if (computer == null)
            {
                return NotFound();
            }

            return View(computer);
        }

        // GET: Computers/Create
        //Returns the view for the create form
        public IActionResult Create()
        {
            return View();
        }

        // POST: Computers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        //Method for posting a created computer. Binds info from form to a new instance of Computer and posts it to create a new row. 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComputerID,Make,Manufacturer,DatePurchased")] Computer computer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(computer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(computer);
        }

        // GET: Computers/Edit/5
        //Grabs info for a computer based on ID and passes to edit view
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computer = await _context.Computer.SingleOrDefaultAsync(m => m.ComputerID == id);
            if (computer == null)
            {
                return NotFound();
            }
            return View(computer);
        }

        // POST: Computers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Binds your edits to the computer instance and passes it in to be updated
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComputerID,Make,Manufacturer,DatePurchased")] Computer computer)
        {
            if (id != computer.ComputerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(computer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComputerExists(computer.ComputerID))
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
            return View(computer);
        }

        // GET: Computers/Delete/5
        //Gets computer based on ID to delete. 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computer = await _context.Computer
                .SingleOrDefaultAsync(m => m.ComputerID == id);
            if (computer == null)
            {
                return NotFound();
            }

            return View(computer);
        }

        // POST: Computers/Delete/5
        //If a computer has ever been used (assigned to an employee) you are redirected to a screen that informs you that it cannot be deleted. 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var computer = await _context.Computer
                .SingleOrDefaultAsync(m => m.ComputerID == id);

            var computerHasBeenAssigned = await _context.EmployeeComputer
              .AnyAsync(m => m.ComputerID == id);

            if (!computerHasBeenAssigned)
            {
                _context.Computer.Remove(computer);
                await _context.SaveChangesAsync();
            }
            else
            {
                return View("CantDeleteComputer");
            }
                await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }

        private bool ComputerExists(int id)
        {
            return _context.Computer.Any(e => e.ComputerID == id);
        }
    }
}
