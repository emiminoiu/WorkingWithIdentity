using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkingWithIdentity.Data.Migrations
{
    public partial class AddingStundentGradez : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stundets_Grades_GradeId",
                table: "Stundets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stundets",
                table: "Stundets");

            migrationBuilder.RenameTable(
                name: "Stundets",
                newName: "Stundents");

            migrationBuilder.RenameIndex(
                name: "IX_Stundets_GradeId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stundents_Grades_GradeId",
                table: "Stundents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stundents",
                table: "Stundents");

            migrationBuilder.RenameTable(
                name: "Stundents",
                newName: "Stundets");

            migrationBuilder.RenameIndex(
                name: "IX_Stundents_GradeId",
                table: "Stundets",
                newName: "IX_Stundets_GradeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stundets",
                table: "Stundets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stundets_Grades_GradeId",
                table: "Stundets",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
