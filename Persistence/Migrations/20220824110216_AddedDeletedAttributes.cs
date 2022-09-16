using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddedDeletedAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 11, 2, 15, 532, DateTimeKind.Utc).AddTicks(72),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 23, 15, 23, 41, 756, DateTimeKind.Utc).AddTicks(3416));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "StaffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 11, 2, 15, 557, DateTimeKind.Utc).AddTicks(4140),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 23, 15, 23, 41, 777, DateTimeKind.Utc).AddTicks(3853));

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "StaffTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "StaffTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "StaffTypes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SchoolTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 11, 2, 15, 556, DateTimeKind.Utc).AddTicks(1075),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 23, 15, 23, 41, 776, DateTimeKind.Utc).AddTicks(2548));

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "SchoolTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "SchoolTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "SchoolTypes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DocumentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 11, 2, 15, 554, DateTimeKind.Utc).AddTicks(7247),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 23, 15, 23, 41, 775, DateTimeKind.Utc).AddTicks(452));

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "DocumentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "DocumentTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "DocumentTypes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "StaffTypes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "StaffTypes");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "StaffTypes");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "SchoolTypes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "SchoolTypes");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "SchoolTypes");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "DocumentTypes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 23, 15, 23, 41, 756, DateTimeKind.Utc).AddTicks(3416),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 11, 2, 15, 532, DateTimeKind.Utc).AddTicks(72));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "StaffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 23, 15, 23, 41, 777, DateTimeKind.Utc).AddTicks(3853),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 11, 2, 15, 557, DateTimeKind.Utc).AddTicks(4140));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SchoolTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 23, 15, 23, 41, 776, DateTimeKind.Utc).AddTicks(2548),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 11, 2, 15, 556, DateTimeKind.Utc).AddTicks(1075));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DocumentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 23, 15, 23, 41, 775, DateTimeKind.Utc).AddTicks(452),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 11, 2, 15, 554, DateTimeKind.Utc).AddTicks(7247));
        }
    }
}
