using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BanagazonWorkforceManager.Models;

namespace BanagazonWorkforceManager.Data
{
    // Class to seed our database with data for testing purposes.
    public static class DbInitializer
    {
        // Method runs on startup to initialize dummy data.
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BanagazonWorkforceManagerContext(serviceProvider.GetRequiredService<DbContextOptions<BanagazonWorkforceManagerContext>>()))
            {
                // Look for any Employees.
                if (context.EmployeeTraining.Any())
                {
                    return;   // DB has been seeded, the rest of this method doesn't need to run.
                }

                // Creating new instances of Department
                var Departments = new Department[]
                {
                    new Department {
                        Name = "Marketing"
                    },
                    new Department {
                        Name = "Sales"
                    },
                    new Department {
                        Name = "Tech Support"
                    },
                    new Department {
                        Name = "Human Resources"
                    }
                };
                // Adds each new Department into the context
                foreach (Department i in Departments)
                {
                    context.Department.Add(i);
                }
                // Saves the Departments to the database
                context.SaveChanges();

                // Creating new instances of Employee
                var Employees = new Employee[]
                {
                    new Employee {
                        FirstName = "Svetlana",
                        LastName = "Smith",
                        StartDate = new DateTime(1989, 8, 20),
                        DepartmentID = 1
                    },
                    new Employee {
                        FirstName = "Nigel",
                        LastName = "Thornberry",
                        StartDate = new DateTime(1974, 6, 20),
                        DepartmentID = 1
                    },
                    new Employee {
                        FirstName = "George",
                        LastName = "Washington",
                        StartDate = new DateTime(1776, 7, 4),
                        DepartmentID = 1
                    },
                    new Employee {
                        FirstName = "Tom",
                        LastName = "Jerry",
                        StartDate = new DateTime(1991, 9, 20),
                        DepartmentID = 2
                    },
                    new Employee {
                        FirstName = "Boris",
                        LastName = "Johnson",
                        StartDate = new DateTime(2015, 2, 4),
                        DepartmentID = 2
                    },
                    new Employee {
                        FirstName = "Mark",
                        LastName = "Hamill",
                        StartDate = new DateTime(1984, 12, 14),
                        DepartmentID = 2
                    },
                    new Employee {
                        FirstName = "George",
                        LastName = "Dickel",
                        StartDate = new DateTime(1919, 1, 3),
                        DepartmentID = 3
                    },
                    new Employee {
                        FirstName = "Jose",
                        LastName = "Cuervo",
                        StartDate = new DateTime(2013, 2, 28),
                        DepartmentID = 3
                    },
                    new Employee {
                        FirstName = "Alexander",
                        LastName = "Hamilton",
                        StartDate = new DateTime(1776, 7, 4),
                        DepartmentID = 3
                    },
                    new Employee {
                        FirstName = "Margaery",
                        LastName = "Tyrell",
                        StartDate = new DateTime(1991, 2, 23),
                        DepartmentID = 4
                    },
                    new Employee {
                        FirstName = "Podrick",
                        LastName = "Payne",
                        StartDate = new DateTime(2017, 6, 13),
                        DepartmentID = 4
                    },
                    new Employee {
                        FirstName = "Rickon",
                        LastName = "Stark",
                        StartDate = new DateTime(2004, 4, 14),
                        DepartmentID = 4
                    }
                };
                // Adds each new Employee into the context
                foreach (Employee i in Employees)
                {
                    context.Employee.Add(i);
                }
                // Saves the Employees to the database
                context.SaveChanges();

                // Creating new instances of TrainingProgram
                var TrainingPrograms = new TrainingProgram[]
                {
                    new TrainingProgram {
                        Name = "Scrum System: The Basics",
                        Description = "Learn how to be productive during a sprint.",
                        StartDate = new DateTime(2017, 12, 14),
                        EndDate = new DateTime(2017, 12, 15),
                        MaxAttendees = 4
                    },
                    new TrainingProgram {
                        Name = "Mentor Partnership",
                        Description = "Come and coach or be coached by others in the company.",
                        StartDate = new DateTime(2017, 11, 12),
                        EndDate = new DateTime(2017, 11, 14),
                        MaxAttendees = 20
                    },
                    new TrainingProgram {
                        Name = "2018 Orientation",
                        Description = "We will discuss new company policies going into effect in 2018.",
                        StartDate = new DateTime(2018, 1, 4),
                        EndDate = new DateTime(2018, 1, 7),
                        MaxAttendees = 8
                    },
                    new TrainingProgram {
                        Name = "Modern Hiring Practices",
                        Description = "How to conduct interviews in the modern era.",
                        StartDate = new DateTime(2015, 2, 28),
                        EndDate = new DateTime(2015, 3, 2),
                        MaxAttendees = 120
                    },
                    new TrainingProgram {
                        Name = "CPR Class",
                        Description = "Best to be prepared.",
                        StartDate = new DateTime(2016, 7, 12),
                        EndDate = new DateTime(2016, 7, 18),
                        MaxAttendees = 50
                    },
                    new TrainingProgram {
                        Name = "Public Speaking Workshop",
                        Description = "Always a handy skill to have.",
                        StartDate = new DateTime(2017, 4, 25),
                        EndDate = new DateTime(2017, 4, 28),
                        MaxAttendees = 15
                    }
                };

                foreach (TrainingProgram i in TrainingPrograms)
                {
                    context.TrainingProgram.Add(i);
                }
                // Saves the TrainingPrograms to the database
                context.SaveChanges();

                // Creating new instances of Computer
                var Computers = new Computer[]
                {
                    new Computer {
                        Make = "Macbook Pro 2015",
                        Manufacturer = "Apple",
                        DatePurchased = new DateTime(2015, 4, 13)
                    },
                    new Computer {
                        Make = "Macbook Pro 2016",
                        Manufacturer = "Apple",
                        DatePurchased = new DateTime(2016, 4, 1)
                    },
                    new Computer {
                        Make = "Macbook Pro 2017",
                        Manufacturer = "Apple",
                        DatePurchased = new DateTime(2017, 4, 15)
                    },
                    new Computer {
                        Make = "Thinkpad",
                        Manufacturer = "Lenovo",
                        DatePurchased = new DateTime(2016, 4, 15)
                    },
                    new Computer {
                        Make = "Yoga",
                        Manufacturer = "Lenovo",
                        DatePurchased = new DateTime(2016, 4, 15)
                    },
                    new Computer {
                        Make = "Inspiron",
                        Manufacturer = "Dell",
                        DatePurchased = new DateTime(2016, 4, 15)
                    },
                    new Computer {
                        Make = "Aspire",
                        Manufacturer = "Acer",
                        DatePurchased = new DateTime(2016, 4, 15)
                    },
                    new Computer {
                        Make = "Pavilion",
                        Manufacturer = "HP",
                        DatePurchased = new DateTime(2016, 4, 15)
                    },
                    new Computer {
                        Make = "Sense",
                        Manufacturer = "Samsung",
                        DatePurchased = new DateTime(2016, 4, 15)
                    },
                    new Computer {
                        Make = "Zenbook",
                        Manufacturer = "Asus",
                        DatePurchased = new DateTime(2016, 4, 15)
                    },
                    new Computer {
                        Make = "Travelmate",
                        Manufacturer = "Acer",
                        DatePurchased = new DateTime(2016, 4, 15)
                    },
                    new Computer {
                        Make = "Elitebook",
                        Manufacturer = "HP",
                        DatePurchased = new DateTime(2016, 4, 15)
                    },
                };
                // Adds each new Computer into the context
                foreach (Computer i in Computers)
                {
                    context.Computer.Add(i);
                }
                // Saves the Computers to the database
                context.SaveChanges();

                var EmployeeComputers = new EmployeeComputer[]
                {
                    new EmployeeComputer {
                        EmployeeID = 1,
                        ComputerID = 1,
                        DateAssigned = new DateTime(2017, 4, 15),
                        DateUnassigned = new DateTime(2017, 4, 16)
                    },
                    new EmployeeComputer {
                        EmployeeID = 2,
                        ComputerID = 1,
                        DateAssigned = new DateTime(2017, 4, 17),
                        DateUnassigned = new DateTime(2017, 4, 18)
                    },
                    new EmployeeComputer {
                        EmployeeID = 3,
                        ComputerID = 1,
                        DateAssigned = new DateTime(2017, 4, 19),
                        DateUnassigned = new DateTime(2017, 4, 20)
                    },
                    new EmployeeComputer {
                        EmployeeID = 4,
                        ComputerID = 1,
                        DateAssigned = new DateTime(2017, 4, 21),
                        DateUnassigned = new DateTime(2017, 4, 22)
                    },
                    new EmployeeComputer {
                        EmployeeID = 5,
                        ComputerID = 1,
                        DateAssigned = new DateTime(2017, 4, 23),
                        DateUnassigned = new DateTime(2017, 4, 24)
                    },
                    new EmployeeComputer {
                        EmployeeID = 6,
                        ComputerID = 1,
                        DateAssigned = new DateTime(2017, 4, 25),
                        DateUnassigned = new DateTime(2017, 4, 26)
                    },
                    new EmployeeComputer {
                        EmployeeID = 7,
                        ComputerID = 1,
                        DateAssigned = new DateTime(2017, 4, 27),
                        DateUnassigned = new DateTime(2017, 4, 28)
                    },
                    new EmployeeComputer {
                        EmployeeID = 8,
                        ComputerID = 1,
                        DateAssigned = new DateTime(2017, 4, 29),
                        DateUnassigned = new DateTime(2017, 4, 30)
                    },
                    new EmployeeComputer {
                        EmployeeID = 9,
                        ComputerID = 1,
                        DateAssigned = new DateTime(2017, 4, 1),
                        DateUnassigned = new DateTime(2017, 4, 2)
                    },
                    new EmployeeComputer {
                        EmployeeID = 10,
                        ComputerID = 1,
                        DateAssigned = new DateTime(2017, 4, 3),
                        DateUnassigned = new DateTime(2017, 4, 4)
                    },
                    new EmployeeComputer {
                        EmployeeID = 11,
                        ComputerID = 1,
                        DateAssigned = new DateTime(2017, 4, 5),
                        DateUnassigned = new DateTime(2017, 4, 6)
                    },
                    new EmployeeComputer {
                        EmployeeID = 12,
                        ComputerID = 1,
                        DateAssigned = new DateTime(2017, 4, 7),
                        DateUnassigned = new DateTime(2017, 4, 8)
                    },
                    new EmployeeComputer {
                        EmployeeID = 1,
                        ComputerID = 1,
                        DateAssigned = new DateTime(2017, 5, 01)
                    },
                    new EmployeeComputer {
                        EmployeeID = 2,
                        ComputerID = 2,
                        DateAssigned = new DateTime(2017, 5, 01)
                    },
                    new EmployeeComputer {
                        EmployeeID = 3,
                        ComputerID = 3,
                        DateAssigned = new DateTime(2017, 5, 01)
                    },
                    new EmployeeComputer {
                        EmployeeID = 4,
                        ComputerID = 4,
                        DateAssigned = new DateTime(2017, 5, 01)
                    },
                     new EmployeeComputer {
                        EmployeeID = 5,
                        ComputerID = 5,
                        DateAssigned = new DateTime(2017, 5, 01)
                    },
                     new EmployeeComputer {
                        EmployeeID = 6,
                        ComputerID = 6,
                        DateAssigned = new DateTime(2017, 5, 01)
                    },
                     new EmployeeComputer {
                        EmployeeID = 7,
                        ComputerID = 7,
                        DateAssigned = new DateTime(2017, 5, 01)
                    },
                     new EmployeeComputer {
                        EmployeeID = 8,
                        ComputerID = 8,
                        DateAssigned = new DateTime(2017, 5, 01)
                    }
                };
                // Adds each new EmployeeComputer into the context
                foreach (EmployeeComputer i in EmployeeComputers)
                {
                    context.EmployeeComputer.Add(i);
                }
                // Saves the Computers to the database
                context.SaveChanges();

                // Creating new instances of EmployeeTraining
                var EmployeeTrainings = new EmployeeTraining[]
                {
                    new EmployeeTraining {
                        EmployeeID = 1,
                        TrainingProgramID = 1
                    },
                    new EmployeeTraining {
                        EmployeeID = 2,
                        TrainingProgramID = 1
                    },
                    new EmployeeTraining {
                        EmployeeID = 3,
                        TrainingProgramID = 1
                    },
                    new EmployeeTraining {
                        EmployeeID = 4,
                        TrainingProgramID = 2
                    },
                    new EmployeeTraining {
                        EmployeeID = 5,
                        TrainingProgramID = 2
                    },
                    new EmployeeTraining {
                        EmployeeID = 6,
                        TrainingProgramID = 2
                    },
                    new EmployeeTraining {
                        EmployeeID = 7,
                         TrainingProgramID = 4
                    },
                    new EmployeeTraining {
                        EmployeeID = 8,
                        TrainingProgramID = 4
                    },
                    new EmployeeTraining {
                        EmployeeID = 9,
                        TrainingProgramID = 4
                    },
                    new EmployeeTraining {
                        EmployeeID = 7,
                        TrainingProgramID = 5
                    },
                    new EmployeeTraining {
                        EmployeeID = 8,
                        TrainingProgramID = 5
                    },
                    new EmployeeTraining {
                        EmployeeID = 9,
                        TrainingProgramID = 5
                    },
                    new EmployeeTraining {
                        EmployeeID = 7,
                        TrainingProgramID = 6
                    },
                    new EmployeeTraining {
                        EmployeeID = 9,
                        TrainingProgramID = 6
                    },
                    new EmployeeTraining {
                        EmployeeID = 9,
                        TrainingProgramID = 2
                    },
                    new EmployeeTraining {
                        EmployeeID = 9,
                        TrainingProgramID = 3
                    },
                    new EmployeeTraining {
                        EmployeeID = 10,
                        TrainingProgramID = 6
                    },
                    new EmployeeTraining {
                        EmployeeID = 8,
                        TrainingProgramID = 6
                    },
                    new EmployeeTraining {
                        EmployeeID = 11,
                        TrainingProgramID = 4
                    }
                };
                // Adds each new EmployeeTraining into the context
                foreach (EmployeeTraining i in EmployeeTrainings)
                {
                    context.EmployeeTraining.Add(i);
                }
                // Saves the EmployeeTraining to the database
                context.SaveChanges();
            }
        }
    }
}