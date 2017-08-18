<<<<<<< HEAD
﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BanagazonWorkforceManager.Models;

namespace BanagazonWorkforceManager.Migrations
{
    [DbContext(typeof(BanagazonWorkforceManagerContext))]
    partial class BanagazonWorkforceManagerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BanagazonWorkforceManager.Models.Computer", b =>
                {
                    b.Property<int>("ComputerID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatePurchased");

                    b.Property<string>("Make")
                        .IsRequired();

                    b.Property<string>("Manufacturer")
                        .IsRequired();

                    b.HasKey("ComputerID");

                    b.ToTable("Computer");
                });

            modelBuilder.Entity("BanagazonWorkforceManager.Models.Department", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("DepartmentID");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("BanagazonWorkforceManager.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DepartmentID");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<DateTime>("StartDate");

                    b.HasKey("EmployeeID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("BanagazonWorkforceManager.Models.EmployeeComputer", b =>
                {
                    b.Property<int>("EmployeeComputerID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ComputerID");

                    b.Property<DateTime>("DateAssigned");

                    b.Property<DateTime?>("DateUnassigned");

                    b.Property<int>("EmployeeID");

                    b.HasKey("EmployeeComputerID");

                    b.HasIndex("ComputerID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("EmployeeComputer");
                });

            modelBuilder.Entity("BanagazonWorkforceManager.Models.EmployeeTraining", b =>
                {
                    b.Property<int>("EmployeeTrainingID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EmployeeID");

                    b.Property<int>("TrainingID");

                    b.Property<int?>("TrainingProgramID");

                    b.HasKey("EmployeeTrainingID");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("TrainingProgramID");

                    b.ToTable("EmployeeTraining");
                });

            modelBuilder.Entity("BanagazonWorkforceManager.Models.TrainingProgram", b =>
                {
                    b.Property<int>("TrainingProgramID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("MaxAttendees");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<DateTime>("StartDate");

                    b.HasKey("TrainingProgramID");

                    b.ToTable("TrainingProgram");
                });

            modelBuilder.Entity("BanagazonWorkforceManager.Models.Employee", b =>
                {
                    b.HasOne("BanagazonWorkforceManager.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BanagazonWorkforceManager.Models.EmployeeComputer", b =>
                {
                    b.HasOne("BanagazonWorkforceManager.Models.Computer", "Computer")
                        .WithMany()
                        .HasForeignKey("ComputerID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BanagazonWorkforceManager.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BanagazonWorkforceManager.Models.EmployeeTraining", b =>
                {
                    b.HasOne("BanagazonWorkforceManager.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BanagazonWorkforceManager.Models.TrainingProgram", "TrainingProgram")
                        .WithMany()
                        .HasForeignKey("TrainingProgramID");
                });
        }
    }
}