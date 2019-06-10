using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkingWithIdentity.Data.Migrations
{
    public partial class AddingStudentAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentAddress",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    StudentId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAddress_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentAddress_StudentId",
                table: "StudentAddress",
                column: "StudentId",
                unique: true,
                filter: "[StudentId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentAddress");
        }
    }
}
