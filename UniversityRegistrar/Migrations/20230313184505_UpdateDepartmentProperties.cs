using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityRegistrar.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDepartmentProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Departments_DepartmentId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_DepartmentId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Enrollments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Enrollments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_DepartmentId",
                table: "Enrollments",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Departments_DepartmentId",
                table: "Enrollments",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
