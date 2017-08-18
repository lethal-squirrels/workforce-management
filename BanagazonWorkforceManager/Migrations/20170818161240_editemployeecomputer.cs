using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BanagazonWorkforceManager.Migrations
{
    public partial class editemployeecomputer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComputerID",
                table: "Employee",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ComputerID",
                table: "Employee",
                column: "ComputerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Computer_ComputerID",
                table: "Employee",
                column: "ComputerID",
                principalTable: "Computer",
                principalColumn: "ComputerID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Computer_ComputerID",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_ComputerID",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "ComputerID",
                table: "Employee");
        }
    }
}
