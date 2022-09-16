using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddedStaffType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SchoolType",
                table: "SchoolType");

            migrationBuilder.RenameTable(
                name: "SchoolType",
                newName: "SchoolTypes");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolType_Name",
                table: "SchoolTypes",
                newName: "IX_SchoolTypes_Name");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolType_Code",
                table: "SchoolTypes",
                newName: "IX_SchoolTypes_Code");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 23, 15, 23, 41, 756, DateTimeKind.Utc).AddTicks(3416),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 23, 15, 19, 15, 572, DateTimeKind.Utc).AddTicks(4627));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DocumentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 23, 15, 23, 41, 775, DateTimeKind.Utc).AddTicks(452),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 23, 15, 19, 15, 591, DateTimeKind.Utc).AddTicks(2180));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SchoolTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 23, 15, 23, 41, 776, DateTimeKind.Utc).AddTicks(2548),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 23, 15, 19, 15, 593, DateTimeKind.Utc).AddTicks(9205));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchoolTypes",
                table: "SchoolTypes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "StaffTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 8, 23, 15, 23, 41, 777, DateTimeKind.Utc).AddTicks(3853)),
                    ModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StaffTypes_Code",
                table: "StaffTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaffTypes_Name",
                table: "StaffTypes",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SchoolTypes",
                table: "SchoolTypes");

            migrationBuilder.RenameTable(
                name: "SchoolTypes",
                newName: "SchoolType");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolTypes_Name",
                table: "SchoolType",
                newName: "IX_SchoolType_Name");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolTypes_Code",
                table: "SchoolType",
                newName: "IX_SchoolType_Code");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 23, 15, 19, 15, 572, DateTimeKind.Utc).AddTicks(4627),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 23, 15, 23, 41, 756, DateTimeKind.Utc).AddTicks(3416));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DocumentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 23, 15, 19, 15, 591, DateTimeKind.Utc).AddTicks(2180),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 23, 15, 23, 41, 775, DateTimeKind.Utc).AddTicks(452));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SchoolType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 23, 15, 19, 15, 593, DateTimeKind.Utc).AddTicks(9205),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 23, 15, 23, 41, 776, DateTimeKind.Utc).AddTicks(2548));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchoolType",
                table: "SchoolType",
                column: "Id");
        }
    }
}
