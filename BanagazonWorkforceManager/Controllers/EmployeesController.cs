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

            var neverAssignedComps = await _context.Computer.Where(c => !_context.EmployeeComputer.Select(ec => ec.ComputerID ).Contains(c.ComputerID)).ToListAsync();
            var unAssignedComps = await _context.Computer.Where(c => c.EmployeeComputers.Any(ec => ec.DateUnassigned != null)).ToListAsync();
            var employeeCurrentComputer = await _context.Computer.Where(c => c.ComputerID == id).ToListAsync();
            var availableComps = neverAssignedComps.Union(unAssignedComps);
            var employeeComputerList = _context.EmployeeComputer.ToList();
            foreach(var computer in employeeComputerList)
            {
                if (computer.DateUnassigned == null && computer.EmployeeID == id)
                {
                    viewModel.ComputerID = computer.ComputerID;
                    viewModel.OldComputerID = computer.ComputerID;
                    viewModel.OldDateAssigned = computer.DateAssigned;
                    viewModel.OldEmployeeComputerID = computer.EmployeeComputerID;
                }
            }
            availableComps = availableComps.Concat(employeeCurrentComputer);
            
            // grab selected computer
            ViewData["ComputerID"] = new SelectList(availableComps, "ComputerID", "Make", viewModel.ComputerID);


            return View(viewModel);
        }

        private void PopulateTrainingProgramList(Employee employee)
        {
            var allTrainingPrograms =  _context.TrainingProgram.Where(m => m.StartDate > DateTime.Today).ToList();
            var employeesTrainingPrograms = new HashSet<int>(employee.EmployeeTrainingPrograms.Select(c => c.TrainingProgramID));
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeEdit employeeEdit)
        {
            EmployeeComputer newEmployeeComputer = new EmployeeComputer()
            {
                ComputerID = employeeEdit.ComputerID,
                EmployeeID = id,
                DateAssigned = DateTime.Now
            };

            EmployeeComputer oldEmployeeComputer = new EmployeeComputer()
            {
                EmployeeComputerID = employeeEdit.OldEmployeeComputerID,
                ComputerID = employeeEdit.OldComputerID,
                EmployeeID = id,
                DateAssigned = employeeEdit.OldDateAssigned,
                DateUnassigned = DateTime.Now
            };



            if (id != employeeEdit.Employee.EmployeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeEdit.Employee);
                    await _context.SaveChangesAsync();
                    _context.EmployeeComputer.Add(newEmployeeComputer);
                    await _context.SaveChangesAsync();
                    _context.EmployeeComputer.Update(oldEmployeeComputer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employeeEdit.Employee.EmployeeID))
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
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "DepartmentID", "Name", employeeEdit.Employee.DepartmentID);
            return View(employeeEdit.Employee);
        }
        //// POST: Employees/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int? id, string[] selectedTrainingPrograms, [Bind("Employee.Computer")] EmployeeComputer eComputer)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var employeeToUpdate = await _context.Employee
        //        .Include(e => e.Department)
        //        .Include("EmployeeComputers.Computer")
        //        .Include("EmployeeTrainingPrograms.TrainingProgram")
        //        .SingleOrDefaultAsync(m => m.EmployeeID == id);

        //    if (await TryUpdateModelAsync<Employee>(
        //        employeeToUpdate,
        //        "",
        //        i => i.FirstName, i => i.LastName, i => i.StartDate, i => i.DepartmentID))
        //    {
        //        UpdateEmployeeTrainingPrograms(selectedTrainingPrograms, employeeToUpdate);
        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateException /* ex */)
        //        {
        //            //Log the error (uncomment ex variable name and write a log.)
        //            ModelState.AddModelError("", "Unable to save changes. " +
        //                "Try again, and if the problem persists, " +
        //                "see your system administrator.");
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    UpdateEmployeeTrainingPrograms(selectedTrainingPrograms, employeeToUpdate);
        //    PopulateTrainingProgramList(employeeToUpdate);

        //    ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "DepartmentID", "Name", employeeToUpdate.DepartmentID);

        //    //_context.Update<EmployeeComputer>();
        //    //await _context.SaveChangesAsync();

        //    return View(employeeToUpdate);
        //}

        //private void UpdateEmployeeTrainingPrograms(string[] selectedTrainingPrograms, Employee employeeToUpdate)
        //{
        //    if (selectedTrainingPrograms == null)
        //    {
        //        employeeToUpdate.EmployeeTrainingPrograms = new List<EmployeeTraining>();
        //        return;
        //    }

        //    var selectedTrainingProgramsHS = new HashSet<string>(selectedTrainingPrograms);
        //    var employeeTrainingPrograms = new HashSet<int>
        //        (employeeToUpdate.EmployeeTrainingPrograms.Select(e => e.TrainingProgram.TrainingProgramID));
        //    foreach (var tp in _context.TrainingProgram)
        //    {
        //        if (selectedTrainingProgramsHS.Contains(tp.TrainingProgramID.ToString()))
        //        {
        //            if (!employeeTrainingPrograms.Contains(tp.TrainingProgramID))
        //            {
        //                _context.Add(new EmployeeTraining() { EmployeeID = employeeToUpdate.EmployeeID, TrainingProgramID = tp.TrainingProgramID });
        //            }
        //        }
        //        else if (employeeTrainingPrograms.Contains(tp.TrainingProgramID))
        //        {
        //            if (!selectedTrainingProgramsHS.Contains(tp.TrainingProgramID.ToString()))
        //            {
        //                foreach(var etp in employeeToUpdate.EmployeeTrainingPrograms)
        //                {
        //                    if (etp.TrainingProgramID == tp.TrainingProgramID)
        //                    {
        //                        _context.Remove(etp);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

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
