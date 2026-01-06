using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollegeApp.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmenttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "DepartmentName", "Description" },
                values: new object[,]
                {
                    { 1, "ECE", "ECE dept" },
                    { 2, "CSE", "CSE dept" }
                });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "DepartmentId", "Dob", "Email", "StudentName" },
                values: new object[] { "India", null, new DateTime(2022, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "venkat@gmail.com", "Venkat" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "DepartmentId", "Dob", "Email", "StudentName" },
                values: new object[] { "India", null, new DateTime(2022, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "nehanth@gmail.com", "Nehanth" });

            migrationBuilder.CreateIndex(
                name: "IX_Students_DepartmentId",
                table: "Students",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Department",
                table: "Students",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Department",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Students_DepartmentId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Students");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "Dob", "Email", "StudentName" },
                values: new object[] { "india", new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "sumit@gmail.com", "sumit" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "Dob", "Email", "StudentName" },
                values: new object[] { "india", new DateTime(2020, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "karan@gmail.com", "karan" });
        }
    }
}
