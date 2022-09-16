using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class renamedSchoolTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_School_SchoolTypes_SchoolTypeId",
                table: "School");

            migrationBuilder.DropPrimaryKey(
                name: "PK_School",
                table: "School");

            migrationBuilder.RenameTable(
                name: "School",
                newName: "Schools");

            migrationBuilder.RenameIndex(
                name: "IX_School_SchoolTypeId",
                table: "Schools",
                newName: "IX_Schools_SchoolTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_School_Name",
                table: "Schools",
                newName: "IX_Schools_Name");

            migrationBuilder.RenameIndex(
                name: "IX_School_Code",
                table: "Schools",
                newName: "IX_Schools_Code");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 14, 6, 41, 723, DateTimeKind.Utc).AddTicks(4795),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 13, 39, 37, 426, DateTimeKind.Utc).AddTicks(1161));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "StaffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 14, 6, 41, 760, DateTimeKind.Utc).AddTicks(3310),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 13, 39, 37, 541, DateTimeKind.Utc).AddTicks(1163));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SchoolTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 14, 6, 41, 756, DateTimeKind.Utc).AddTicks(4261),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 13, 39, 37, 539, DateTimeKind.Utc).AddTicks(7980));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DocumentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 14, 6, 41, 749, DateTimeKind.Utc).AddTicks(3816),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 13, 39, 37, 446, DateTimeKind.Utc).AddTicks(446));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Schools",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 14, 6, 41, 753, DateTimeKind.Utc).AddTicks(6853),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 13, 39, 37, 538, DateTimeKind.Utc).AddTicks(4607));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schools",
                table: "Schools",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_SchoolTypes_SchoolTypeId",
                table: "Schools",
                column: "SchoolTypeId",
                principalTable: "SchoolTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schools_SchoolTypes_SchoolTypeId",
                table: "Schools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schools",
                table: "Schools");

            migrationBuilder.RenameTable(
                name: "Schools",
                newName: "School");

            migrationBuilder.RenameIndex(
                name: "IX_Schools_SchoolTypeId",
                table: "School",
                newName: "IX_School_SchoolTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Schools_Name",
                table: "School",
                newName: "IX_School_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Schools_Code",
                table: "School",
                newName: "IX_School_Code");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 13, 39, 37, 426, DateTimeKind.Utc).AddTicks(1161),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 14, 6, 41, 723, DateTimeKind.Utc).AddTicks(4795));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "StaffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 13, 39, 37, 541, DateTimeKind.Utc).AddTicks(1163),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 14, 6, 41, 760, DateTimeKind.Utc).AddTicks(3310));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SchoolTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 13, 39, 37, 539, DateTimeKind.Utc).AddTicks(7980),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 14, 6, 41, 756, DateTimeKind.Utc).AddTicks(4261));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DocumentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 13, 39, 37, 446, DateTimeKind.Utc).AddTicks(446),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 14, 6, 41, 749, DateTimeKind.Utc).AddTicks(3816));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "School",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 13, 39, 37, 538, DateTimeKind.Utc).AddTicks(4607),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 14, 6, 41, 753, DateTimeKind.Utc).AddTicks(6853));

            migrationBuilder.AddPrimaryKey(
                name: "PK_School",
                table: "School",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_School_SchoolTypes_SchoolTypeId",
                table: "School",
                column: "SchoolTypeId",
                principalTable: "SchoolTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
