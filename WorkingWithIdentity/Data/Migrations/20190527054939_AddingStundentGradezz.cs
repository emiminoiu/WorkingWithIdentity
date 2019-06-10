using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkingWithIdentity.Data.Migrations
{
    public partial class AddingStundentGradezz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stundents_Grades_GradeId",
                table: "Stundents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stundents",
                table: "Stundents");

            migrationBuilder.RenameTable(
                name: "Stundents",
                newName: "Students");

            migrationBuilder.RenameIndex(
                name: "IX_Stundents_GradeId",
                table: "Students",
                newName: "IX_Students_GradeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Grades_GradeId",
                table: "Students",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Grades_GradeId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Stundents");

            migrationBuilder.RenameIndex(
                name: "IX_Students_GradeId",
                table: "Stundents",
                newName: "IX_Stundents_GradeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stundents",
                table: "Stundents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stundents_Grades_GradeId",
                table: "Stundents",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
