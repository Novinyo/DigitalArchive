using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddedStaffTypeSchoolId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "StaffTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolId",
                table: "StaffTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaffTypes_SchoolId_Code",
                table: "StaffTypes",
                columns: new[] { "SchoolId", "Code" },
                unique: true,
                filter: "[SchoolId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StaffTypes_SchoolId_Name",
                table: "StaffTypes",
                columns: new[] { "SchoolId", "Name" },
                unique: true,
                filter: "[SchoolId] IS NOT NULL AND [Name] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffTypes_Schools_SchoolId",
                table: "StaffTypes",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffTypes_Schools_SchoolId",
                table: "StaffTypes");

            migrationBuilder.DropIndex(
                name: "IX_StaffTypes_SchoolId_Code",
                table: "StaffTypes");

            migrationBuilder.DropIndex(
                name: "IX_StaffTypes_SchoolId_Name",
                table: "StaffTypes");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "StaffTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "StaffTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
