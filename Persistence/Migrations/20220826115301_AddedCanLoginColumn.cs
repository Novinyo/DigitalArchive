using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddedCanLoginColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 514, DateTimeKind.Utc).AddTicks(4799));

            migrationBuilder.AddColumn<bool>(
                name: "CanLogin",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "StudentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 26, 11, 53, 0, 479, DateTimeKind.Utc).AddTicks(8539),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 543, DateTimeKind.Utc).AddTicks(5890));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SchoolTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 26, 11, 53, 0, 478, DateTimeKind.Utc).AddTicks(4308),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 541, DateTimeKind.Utc).AddTicks(9586));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Schools",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 26, 11, 53, 0, 476, DateTimeKind.Utc).AddTicks(9619),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 540, DateTimeKind.Utc).AddTicks(3419));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "EntityTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 26, 11, 53, 0, 474, DateTimeKind.Utc).AddTicks(7633),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 537, DateTimeKind.Utc).AddTicks(9548));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DocumentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 26, 11, 53, 0, 465, DateTimeKind.Utc).AddTicks(4759),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 530, DateTimeKind.Utc).AddTicks(8329));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanLogin",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 514, DateTimeKind.Utc).AddTicks(4799),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "StudentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 543, DateTimeKind.Utc).AddTicks(5890),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 26, 11, 53, 0, 479, DateTimeKind.Utc).AddTicks(8539));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SchoolTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 541, DateTimeKind.Utc).AddTicks(9586),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 26, 11, 53, 0, 478, DateTimeKind.Utc).AddTicks(4308));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Schools",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 540, DateTimeKind.Utc).AddTicks(3419),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 26, 11, 53, 0, 476, DateTimeKind.Utc).AddTicks(9619));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "EntityTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 537, DateTimeKind.Utc).AddTicks(9548),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 26, 11, 53, 0, 474, DateTimeKind.Utc).AddTicks(7633));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DocumentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 530, DateTimeKind.Utc).AddTicks(8329),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 26, 11, 53, 0, 465, DateTimeKind.Utc).AddTicks(4759));
        }
    }
}
