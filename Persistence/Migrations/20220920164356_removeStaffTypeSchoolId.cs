using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class removeStaffTypeSchoolId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_StaffTypes_SchoolId_Code",
                table: "StaffTypes",
                columns: new[] { "SchoolId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaffTypes_SchoolId_Name",
                table: "StaffTypes",
                columns: new[] { "SchoolId", "Name" },
                unique: true);
        }
    }
}
