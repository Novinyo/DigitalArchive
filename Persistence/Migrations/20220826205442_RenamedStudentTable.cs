using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RenamedStudentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Contacts_ContactId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_EntityTypes_StudentTypeId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Schools_SchoolId",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.RenameTable(
                name: "Student",
                newName: "Students");

            migrationBuilder.RenameIndex(
                name: "IX_Student_StudentTypeId",
                table: "Students",
                newName: "IX_Students_StudentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_SchoolId",
                table: "Students",
                newName: "IX_Students_SchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_ContactId",
                table: "Students",
                newName: "IX_Students_ContactId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_Code_SchoolId",
                table: "Students",
                newName: "IX_Students_Code_SchoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Contacts_ContactId",
                table: "Students",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_EntityTypes_StudentTypeId",
                table: "Students",
                column: "StudentTypeId",
                principalTable: "EntityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Schools_SchoolId",
                table: "Students",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Contacts_ContactId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_EntityTypes_StudentTypeId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Schools_SchoolId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Student");

            migrationBuilder.RenameIndex(
                name: "IX_Students_StudentTypeId",
                table: "Student",
                newName: "IX_Student_StudentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_SchoolId",
                table: "Student",
                newName: "IX_Student_SchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_ContactId",
                table: "Student",
                newName: "IX_Student_ContactId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_Code_SchoolId",
                table: "Student",
                newName: "IX_Student_Code_SchoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Contacts_ContactId",
                table: "Student",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_EntityTypes_StudentTypeId",
                table: "Student",
                column: "StudentTypeId",
                principalTable: "EntityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Schools_SchoolId",
                table: "Student",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
