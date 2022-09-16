using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RenamedStaffTypeToEntityType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffTypes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 514, DateTimeKind.Utc).AddTicks(4799),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 18, 41, 33, 749, DateTimeKind.Utc).AddTicks(840));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "StudentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 543, DateTimeKind.Utc).AddTicks(5890),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 18, 41, 33, 820, DateTimeKind.Utc).AddTicks(3270));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SchoolTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 541, DateTimeKind.Utc).AddTicks(9586),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 18, 41, 33, 811, DateTimeKind.Utc).AddTicks(7226));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Schools",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 540, DateTimeKind.Utc).AddTicks(3419),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 18, 41, 33, 804, DateTimeKind.Utc).AddTicks(4010));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DocumentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 530, DateTimeKind.Utc).AddTicks(8329),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 18, 41, 33, 798, DateTimeKind.Utc).AddTicks(3412));

            migrationBuilder.CreateTable(
                name: "EntityTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 537, DateTimeKind.Utc).AddTicks(9548)),
                    ModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityTypes_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityTypes_SchoolId_Code",
                table: "EntityTypes",
                columns: new[] { "SchoolId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityTypes_SchoolId_Name",
                table: "EntityTypes",
                columns: new[] { "SchoolId", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityTypes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 18, 41, 33, 749, DateTimeKind.Utc).AddTicks(840),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 514, DateTimeKind.Utc).AddTicks(4799));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "StudentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 18, 41, 33, 820, DateTimeKind.Utc).AddTicks(3270),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 543, DateTimeKind.Utc).AddTicks(5890));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SchoolTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 18, 41, 33, 811, DateTimeKind.Utc).AddTicks(7226),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 541, DateTimeKind.Utc).AddTicks(9586));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Schools",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 18, 41, 33, 804, DateTimeKind.Utc).AddTicks(4010),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 540, DateTimeKind.Utc).AddTicks(3419));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DocumentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 18, 41, 33, 798, DateTimeKind.Utc).AddTicks(3412),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 26, 7, 17, 31, 530, DateTimeKind.Utc).AddTicks(8329));

            migrationBuilder.CreateTable(
                name: "StaffTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 8, 24, 18, 41, 33, 815, DateTimeKind.Utc).AddTicks(787)),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
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
    }
}
