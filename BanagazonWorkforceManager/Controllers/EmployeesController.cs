using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BanagazonWorkforceManager.Models;
using BanagazonWorkforceManager.ViewModels;

namespace BanagazonWorkforceManager.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly BanagazonWorkforceManagerContext _context;

        public EmployeesController(BanagazonWorkforceManagerContext context)
        {
            _context = context;    
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var banagazonWorkforceManagerContext = _context.Employee.Include(e => e.Department);
            return View(await banagazonWorkforceManagerContext.ToListAsync());
        }

        // GET: Employees/Details/5
        //Creates an object with all the info we wish to display in the employee view
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //does an async get of employee info
            //includes employees department and computer
            var employee = await _context.Employee
                .Include(e => e.Department)
                .Include("EmployeeComputers.Computer")
                .Include("EmployeeTrainingPrograms.TrainingProgram")
                .SingleOrDefaultAsync(m => m.EmployeeID == id);

       

            if (employee == null)
            {
                return NotFound();
            }

            //passes the newly created employee object to our view
            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "DepartmentID", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeID,FirstName,LastName,StartDate,DepartmentID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "DepartmentID", "Name", employee.DepartmentID);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EmployeeEdit viewModel = new EmployeeEdit();
            viewModel.Employee = await _context.Employee
                .Include(e => e.Department)
                .Include("EmployeeComputers.Computer")
                .Include("EmployeeTrainingPrograms.TrainingProgram")
                .SingleOrDefaultAsync(m => m.EmployeeID == id);
            if (viewModel.Employee == null)
            {
                return NotFound();
            }
            PopulateTrainingProgramList(viewModel.Employee);
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "DepartmentID", "Name", viewModel.Employee.DepartmentID);
            return View(viewModel);
        }

        private void PopulateTrainingProgramList(Employee employee)
        {
            var allTrainingPrograms =  _context.TrainingProgram.Where(m => m.StartDate > DateTime.Today).ToList();
            var employeesTrainingPrograms = new HashSet<int>(employee.EmployeeTrainingPrograms.Select(c => c.TrainingID));
            var viewModel = new List<TrainingProgramList>();
            foreach (var tp in allTrainingPrograms)
            {
                viewModel.Add(new TrainingProgramList
                {
                    TrainingProgramID = tp.TrainingProgramID,
                    Name = tp.Name,
                    Attending = employeesTrainingPrograms.Contains(tp.TrainingProgramID)
                });
            }
            ViewData["TrainingPrograms"] = viewModel;
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedCourses)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeToUpdate = await _context.Employee
                .Include(e => e.Department)
                .Include("EmployeeComputers.Computer")
                .Include("EmployeeTrainingPrograms.TrainingProgram")
                .SingleOrDefaultAsync(m => m.EmployeeID == id);

            if (await TryUpdateModelAsync<Employee>(
                employeeToUpdate,
                "",
                i => i.FirstName, i => i.LastName, i => i.StartDate, i => i.DepartmentID));

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeID))
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
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "DepartmentID", "Name", employee.DepartmentID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Department)
                .SingleOrDefaultAsync(m => m.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.SingleOrDefaultAsync(m => m.EmployeeID == id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.EmployeeID == id);
        }
    }
}
