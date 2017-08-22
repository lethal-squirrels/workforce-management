using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BanagazonWorkforceManager.Migrations
{
    public partial class trainingprogramfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTraining_TrainingProgram_TrainingProgramID",
                table: "EmployeeTraining");

            migrationBuilder.DropColumn(
                name: "TrainingID",
                table: "EmployeeTraining");

            migrationBuilder.AlterColumn<int>(
                name: "TrainingProgramID",
                table: "EmployeeTraining",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTraining_TrainingProgram_TrainingProgramID",
                table: "EmployeeTraining",
                column: "TrainingProgramID",
                principalTable: "TrainingProgram",
                principalColumn: "TrainingProgramID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTraining_TrainingProgram_TrainingProgramID",
                table: "EmployeeTraining");

            migrationBuilder.AlterColumn<int>(
                name: "TrainingProgramID",
                table: "EmployeeTraining",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TrainingID",
                table: "EmployeeTraining",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTraining_TrainingProgram_TrainingProgramID",
                table: "EmployeeTraining",
                column: "TrainingProgramID",
                principalTable: "TrainingProgram",
                principalColumn: "TrainingProgramID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
