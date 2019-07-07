using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkingWithIdentity.Data.Migrations
{
    public partial class AddingBudgetToEachUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Budget",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoOfCourses",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Budget",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NoOfCourses",
                table: "AspNetUsers");
        }
    }
}
