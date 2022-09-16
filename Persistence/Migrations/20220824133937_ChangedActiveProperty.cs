using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ChangedActiveProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 13, 39, 37, 426, DateTimeKind.Utc).AddTicks(1161),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 13, 33, 28, 840, DateTimeKind.Utc).AddTicks(1807));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "StaffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 13, 39, 37, 541, DateTimeKind.Utc).AddTicks(1163),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 13, 33, 28, 954, DateTimeKind.Utc).AddTicks(5725));

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "StaffTypes",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SchoolTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 13, 39, 37, 539, DateTimeKind.Utc).AddTicks(7980),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 13, 33, 28, 952, DateTimeKind.Utc).AddTicks(3356));

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "SchoolTypes",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "School",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 13, 39, 37, 538, DateTimeKind.Utc).AddTicks(4607),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 13, 33, 28, 950, DateTimeKind.Utc).AddTicks(9944));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DocumentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 13, 39, 37, 446, DateTimeKind.Utc).AddTicks(446),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 13, 33, 28, 864, DateTimeKind.Utc).AddTicks(8473));

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "DocumentTypes",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 13, 33, 28, 840, DateTimeKind.Utc).AddTicks(1807),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 13, 39, 37, 426, DateTimeKind.Utc).AddTicks(1161));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "StaffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 13, 33, 28, 954, DateTimeKind.Utc).AddTicks(5725),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 13, 39, 37, 541, DateTimeKind.Utc).AddTicks(1163));

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "StaffTypes",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SchoolTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 13, 33, 28, 952, DateTimeKind.Utc).AddTicks(3356),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 13, 39, 37, 539, DateTimeKind.Utc).AddTicks(7980));

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "SchoolTypes",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "School",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 13, 33, 28, 950, DateTimeKind.Utc).AddTicks(9944),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 13, 39, 37, 538, DateTimeKind.Utc).AddTicks(4607));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DocumentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 13, 33, 28, 864, DateTimeKind.Utc).AddTicks(8473),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 13, 39, 37, 446, DateTimeKind.Utc).AddTicks(446));

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "DocumentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
