using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class UpdatedDocumentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "StudentTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "StudentTypes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 26, 11, 53, 0, 479, DateTimeKind.Utc).AddTicks(8539));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "StudentTypes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SchoolTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SchoolTypes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 26, 11, 53, 0, 478, DateTimeKind.Utc).AddTicks(4308));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "SchoolTypes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Schools",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 26, 11, 53, 0, 476, DateTimeKind.Utc).AddTicks(9619));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Schools",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "EntityTypes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 26, 11, 53, 0, 474, DateTimeKind.Utc).AddTicks(7633));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DocumentTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DocumentTypes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 26, 11, 53, 0, 465, DateTimeKind.Utc).AddTicks(4759));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "DocumentTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolId",
                table: "DocumentTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_SchoolId_Code",
                table: "DocumentTypes",
                columns: new[] { "SchoolId", "Code" },
                unique: true,
                filter: "[SchoolId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_SchoolId_Name",
                table: "DocumentTypes",
                columns: new[] { "SchoolId", "Name" },
                unique: true,
                filter: "[SchoolId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTypes_Schools_SchoolId",
                table: "DocumentTypes",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTypes_Schools_SchoolId",
                table: "DocumentTypes");

            migrationBuilder.DropIndex(
                name: "IX_DocumentTypes_SchoolId_Code",
                table: "DocumentTypes");

            migrationBuilder.DropIndex(
                name: "IX_DocumentTypes_SchoolId_Name",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "DocumentTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "StudentTypes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "StudentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 26, 11, 53, 0, 479, DateTimeKind.Utc).AddTicks(8539),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "StudentTypes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SchoolTypes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SchoolTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 26, 11, 53, 0, 478, DateTimeKind.Utc).AddTicks(4308),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "SchoolTypes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Schools",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 26, 11, 53, 0, 476, DateTimeKind.Utc).AddTicks(9619),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Schools",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "EntityTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 26, 11, 53, 0, 474, DateTimeKind.Utc).AddTicks(7633),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DocumentTypes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DocumentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 26, 11, 53, 0, 465, DateTimeKind.Utc).AddTicks(4759),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "DocumentTypes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
