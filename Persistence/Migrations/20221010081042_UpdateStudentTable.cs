using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class UpdateStudentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_StaffTypes_StudentTypeId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_StudentTypeId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_Code_Email",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_Email",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "MotherName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentTypeId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "RelationshipType",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Contacts",
                newName: "MotherPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Contacts",
                newName: "FatherLastName");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Contacts",
                newName: "MotherLastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Contacts",
                newName: "MotherFirstName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Contacts",
                newName: "MotherEmail");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "Documents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContact",
                table: "Contacts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FatherEmail",
                table: "Contacts",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FatherFirstName",
                table: "Contacts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FatherPhoneNumber",
                table: "Contacts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_StudentId",
                table: "Documents",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Students_StudentId",
                table: "Documents",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Students_StudentId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_StudentId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "EmergencyContact",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "FatherEmail",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "FatherFirstName",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "FatherPhoneNumber",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "MotherPhoneNumber",
                table: "Contacts",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "MotherLastName",
                table: "Contacts",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "MotherFirstName",
                table: "Contacts",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "MotherEmail",
                table: "Contacts",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "FatherLastName",
                table: "Contacts",
                newName: "MiddleName");

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "Students",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherName",
                table: "Students",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentTypeId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "RelationshipType",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentTypeId",
                table: "Students",
                column: "StudentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Code_Email",
                table: "Contacts",
                columns: new[] { "Code", "Email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Email",
                table: "Contacts",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_StaffTypes_StudentTypeId",
                table: "Students",
                column: "StudentTypeId",
                principalTable: "StaffTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
