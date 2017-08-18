using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BanagazonWorkforceManager.Migrations
{
    public partial class employeecomputerupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Computer",
                columns: table => new
                {
                    ComputerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatePurchased = table.Column<DateTime>(nullable: false),
                    Make = table.Column<string>(nullable: false),
                    Manufacturer = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computer", x => x.ComputerID);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "TrainingProgram",
                columns: table => new
                {
                    TrainingProgramID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    MaxAttendees = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingProgram", x => x.TrainingProgramID);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ComputerID = table.Column<int>(nullable: true),
                    DepartmentID = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    LastName = table.Column<string>(maxLength: 40, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Employee_Computer_ComputerID",
                        column: x => x.ComputerID,
                        principalTable: "Computer",
                        principalColumn: "ComputerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_Department_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Department",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeComputer",
                columns: table => new
                {
                    EmployeeComputerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ComputerID = table.Column<int>(nullable: false),
                    DateAssigned = table.Column<DateTime>(nullable: false),
                    DateUnassigned = table.Column<DateTime>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeComputer", x => x.EmployeeComputerID);
                    table.ForeignKey(
                        name: "FK_EmployeeComputer_Computer_ComputerID",
                        column: x => x.ComputerID,
                        principalTable: "Computer",
                        principalColumn: "ComputerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeComputer_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employee",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTraining",
                columns: table => new
                {
                    EmployeeTrainingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeID = table.Column<int>(nullable: false),
                    TrainingID = table.Column<int>(nullable: false),
                    TrainingProgramID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTraining", x => x.EmployeeTrainingID);
                    table.ForeignKey(
                        name: "FK_EmployeeTraining_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employee",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeTraining_TrainingProgram_TrainingProgramID",
                        column: x => x.TrainingProgramID,
                        principalTable: "TrainingProgram",
                        principalColumn: "TrainingProgramID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ComputerID",
                table: "Employee",
                column: "ComputerID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentID",
                table: "Employee",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeComputer_ComputerID",
                table: "EmployeeComputer",
                column: "ComputerID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeComputer_EmployeeID",
                table: "EmployeeComputer",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTraining_EmployeeID",
                table: "EmployeeTraining",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTraining_TrainingProgramID",
                table: "EmployeeTraining",
                column: "TrainingProgramID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeComputer");

            migrationBuilder.DropTable(
                name: "EmployeeTraining");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "TrainingProgram");

            migrationBuilder.DropTable(
                name: "Computer");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
