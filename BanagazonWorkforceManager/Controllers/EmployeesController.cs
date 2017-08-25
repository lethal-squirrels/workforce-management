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

            // Creates a new instance of the EmployeeEdit view model
            EmployeeEdit viewModel = new EmployeeEdit();
            // Grabs the Current Employee and Attatches Computer and TrainingProgram Tables
            viewModel.Employee = await _context.Employee
                .Include(e => e.Department)
                .Include("EmployeeComputers.Computer")
                .Include("EmployeeTrainingPrograms.TrainingProgram")
                .SingleOrDefaultAsync(m => m.EmployeeID == id);
            // Makes Sure the Employee is Actually There
            if (viewModel.Employee == null)
            {
                return NotFound();
            }
            // Calls the PopulateTrainingProgramList Method, The current Employee is passed in as an argument
            PopulateTrainingProgramList(viewModel.Employee);
            // Creates a new Select List that will Allow you to Select a different department in the view
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "DepartmentID", "Name", viewModel.Employee.DepartmentID);
            // Grabs all Computers and attatches EmployeeComputers Join Table
            var allComputers = await _context.Computer.Include("EmployeeComputers").ToListAsync();
            // Creates a new instance of a list of computers
            var availableComputers = new List<Computer>();
            // Loops Over all Computers and only adds the available computers that are unassagined
            foreach (var computer in allComputers)
            {
                if (!(computer.EmployeeComputers.Any(ec => ec.DateUnassigned == null)))
                {
                    availableComputers.Add(computer);
                }
            }
            // Grabs the current Employees Computer
            var CurrentCompAssignment = viewModel.Employee.EmployeeComputers.SingleOrDefault(m => m.DateUnassigned == null);
            // If The current Employee has a computer, it is added to the available computer list and the computer ID is attached to the view model
            if(CurrentCompAssignment != null)
            {
                availableComputers.Add(CurrentCompAssignment.Computer);
                viewModel.SelectedComputerID = CurrentCompAssignment.ComputerID;
            }
            // Creates a new select list with the available computers
            ViewData["Computers"] = new SelectList(availableComputers, "ComputerID", "Make", viewModel.SelectedComputerID);
            return View(viewModel);
        }

        // A method to populate the The Training Program List, takes an argument of the current Employee
        private void PopulateTrainingProgramList(Employee employee)
        {
            // Grabs all training programs that are in the future
            var allTrainingPrograms = _context.TrainingProgram.Where(m => m.StartDate > DateTime.Today).Include(m => m.EmployeeTrainingPrograms).ToList();
            // Creates a Hashset that contains all the future training program IDs
            var employeesTrainingPrograms = new HashSet<int>(employee.EmployeeTrainingPrograms.Select(c => c.TrainingProgramID));
            var viewModel = new List<TrainingProgramList>();
            // Loops over all training programs and adds the training program to a list if the max attendence had not been met or it has already been selected
            foreach (var tp in allTrainingPrograms)
            {
                if(!(tp.EmployeeTrainingPrograms.Count >= tp.MaxAttendees) || employeesTrainingPrograms.Contains(tp.TrainingProgramID))
                viewModel.Add(new TrainingProgramList
                {
                    TrainingProgramID = tp.TrainingProgramID,
                    Name = tp.Name,
                    Attending = employeesTrainingPrograms.Contains(tp.TrainingProgramID)
                });
            }
            // Attached the viewModel to the Scope
            ViewData["TrainingPrograms"] = viewModel;
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedTrainingPrograms, EmployeeEdit viewModel)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            { 
                try
                {
                    _context.Update(viewModel.Employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(viewModel.Employee.EmployeeID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            var employeeToUpdate = await _context.Employee
            .Include(e => e.Department)
            .Include("EmployeeComputers.Computer")
            .Include("EmployeeTrainingPrograms.TrainingProgram")
            .SingleOrDefaultAsync(m => m.EmployeeID == id);
            // Calls method to update current Employee training programs -- takes argument of selectedTrainingPrograms(checked in the view) and the curent employee
            UpdateEmployeeTrainingPrograms(selectedTrainingPrograms, employeeToUpdate);
            // Calls method to update current Emloyee Computer -- takes an argument of The selected ComputerID and the cuurent Employee
            UpdateEmployeeComputer(viewModel.SelectedComputerID, employeeToUpdate);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException /* ex */)
            {
                //log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "unable to save changes. " +
                    "try again, and if the problem persists, " +
                    "see your system administrator.");
            }
            return RedirectToAction("Index");
        }
        // Calls method to update current Emloyee Computer -- takes an argument of The selected ComputerID and the cuurent Employee
        private void UpdateEmployeeComputer(int SelectedComputerID, Employee employeeToUpdate)
        {
            // Grabs the current EmployeeComputer
            var CurrentCompAssignment = employeeToUpdate.EmployeeComputers.SingleOrDefault(m => m.DateUnassigned == null);
            // If the Employee has an Active Computer -->
            if (CurrentCompAssignment != null)
            {
                // If the newly selected ComputerID is not the current ComputerID Update the DateUnassigned and add a new row in the EmployeeComputer table
                if (SelectedComputerID != CurrentCompAssignment.Computer.ComputerID)
                {
                    CurrentCompAssignment.DateUnassigned = DateTime.Now;
                    var newAssignment = new EmployeeComputer() { EmployeeID = employeeToUpdate.EmployeeID, ComputerID = SelectedComputerID, DateAssigned = DateTime.Now};
                    _context.Update(CurrentCompAssignment);
                    _context.Add(newAssignment);
                }
            }
            // If the Employee does not have an active computer, add the new computer to the EmployeeComputer Table
            else
            {
                var newAssignment = new EmployeeComputer() { EmployeeID = employeeToUpdate.EmployeeID, ComputerID = SelectedComputerID, DateAssigned = DateTime.Now };
                _context.Add(newAssignment);
            }
        }

        // Method to update current Employee training programs -- takes argument of selectedTrainingPrograms(checked in the view) and the curent employee
        private void UpdateEmployeeTrainingPrograms(string[] selectedTrainingPrograms, Employee employeeToUpdate)
        {
            // creates a hashset of training program IDs that were selected in the view
            var selectedTrainingProgramsHS = new HashSet<string>(selectedTrainingPrograms);
            // creates a hashset of training program IDs that were previously attached to the current employee
            var employeeTrainingPrograms = new HashSet<int>
                (employeeToUpdate.EmployeeTrainingPrograms.Select(e => e.TrainingProgram.TrainingProgramID));
            // Loops over all training programs
            foreach (var tp in _context.TrainingProgram)
            {
                // If they selected a training program, but the employee is not currently signed up, Add the new training program to the EmployeeTraining Table
                if (selectedTrainingProgramsHS.Contains(tp.TrainingProgramID.ToString()))
                {
                    if (!employeeTrainingPrograms.Contains(tp.TrainingProgramID))
                    {
                        _context.Add(new EmployeeTraining() { EmployeeID = employeeToUpdate.EmployeeID, TrainingProgramID = tp.TrainingProgramID });
                    }
                }
                // If they unselected a training program that they were currently enrolled in, remove the row from the EmlpoyeeTraining Table
                else if (employeeTrainingPrograms.Contains(tp.TrainingProgramID))
                {
                    if (!selectedTrainingProgramsHS.Contains(tp.TrainingProgramID.ToString()))
                    {
                        foreach(var etp in employeeToUpdate.EmployeeTrainingPrograms)
                        {
                            if (etp.TrainingProgramID == tp.TrainingProgramID)
                            {
                                _context.Remove(etp);
                            }
                        }
                    }
                }
            }
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
