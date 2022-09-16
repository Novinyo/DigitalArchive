using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddedStudentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 18, 41, 33, 749, DateTimeKind.Utc).AddTicks(840),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 14, 6, 41, 723, DateTimeKind.Utc).AddTicks(4795));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "StaffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 18, 41, 33, 815, DateTimeKind.Utc).AddTicks(787),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 14, 6, 41, 760, DateTimeKind.Utc).AddTicks(3310));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SchoolTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 18, 41, 33, 811, DateTimeKind.Utc).AddTicks(7226),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 14, 6, 41, 756, DateTimeKind.Utc).AddTicks(4261));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Schools",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Schools",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 18, 41, 33, 804, DateTimeKind.Utc).AddTicks(4010),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 14, 6, 41, 753, DateTimeKind.Utc).AddTicks(6853));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DocumentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 18, 41, 33, 798, DateTimeKind.Utc).AddTicks(3412),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 14, 6, 41, 749, DateTimeKind.Utc).AddTicks(3816));

            migrationBuilder.CreateTable(
                name: "StudentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 8, 24, 18, 41, 33, 820, DateTimeKind.Utc).AddTicks(3270)),
                    ModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentTypes_Code",
                table: "StudentTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentTypes_Name",
                table: "StudentTypes",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentTypes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 14, 6, 41, 723, DateTimeKind.Utc).AddTicks(4795),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 18, 41, 33, 749, DateTimeKind.Utc).AddTicks(840));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "StaffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 14, 6, 41, 760, DateTimeKind.Utc).AddTicks(3310),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 18, 41, 33, 815, DateTimeKind.Utc).AddTicks(787));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SchoolTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 14, 6, 41, 756, DateTimeKind.Utc).AddTicks(4261),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 18, 41, 33, 811, DateTimeKind.Utc).AddTicks(7226));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Schools",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Schools",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 14, 6, 41, 753, DateTimeKind.Utc).AddTicks(6853),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 18, 41, 33, 804, DateTimeKind.Utc).AddTicks(4010));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DocumentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 14, 6, 41, 749, DateTimeKind.Utc).AddTicks(3816),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 18, 41, 33, 798, DateTimeKind.Utc).AddTicks(3412));
        }
    }
}
