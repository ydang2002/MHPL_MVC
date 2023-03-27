using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DCT1205.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddColumEmployeeNotoEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeNo",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeNo",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Employee");
        }
    }
}
