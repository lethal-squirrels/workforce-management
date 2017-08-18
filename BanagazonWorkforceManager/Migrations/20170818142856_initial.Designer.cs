using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BanagazonWorkforceManager.Models;

namespace BanagazonWorkforceManager.Migrations
{
    [DbContext(typeof(BanagazonWorkforceManagerContext))]
    [Migration("20170818142856_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
        }
    }
}
