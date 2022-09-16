using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddedSchoolEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 13, 33, 28, 840, DateTimeKind.Utc).AddTicks(1807),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 11, 2, 15, 532, DateTimeKind.Utc).AddTicks(72));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "StaffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 13, 33, 28, 954, DateTimeKind.Utc).AddTicks(5725),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 11, 2, 15, 557, DateTimeKind.Utc).AddTicks(4140));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SchoolTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 13, 33, 28, 952, DateTimeKind.Utc).AddTicks(3356),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 11, 2, 15, 556, DateTimeKind.Utc).AddTicks(1075));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DocumentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 13, 33, 28, 864, DateTimeKind.Utc).AddTicks(8473),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 11, 2, 15, 554, DateTimeKind.Utc).AddTicks(7247));

            migrationBuilder.CreateTable(
                name: "School",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SchoolTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 8, 24, 13, 33, 28, 950, DateTimeKind.Utc).AddTicks(9944)),
                    ModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => x.Id);
                    table.ForeignKey(
                        name: "FK_School_SchoolTypes_SchoolTypeId",
                        column: x => x.SchoolTypeId,
                        principalTable: "SchoolTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_School_Code",
                table: "School",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_School_Name",
                table: "School",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_School_SchoolTypeId",
                table: "School",
                column: "SchoolTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "School");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 11, 2, 15, 532, DateTimeKind.Utc).AddTicks(72),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 13, 33, 28, 840, DateTimeKind.Utc).AddTicks(1807));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "StaffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 11, 2, 15, 557, DateTimeKind.Utc).AddTicks(4140),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 13, 33, 28, 954, DateTimeKind.Utc).AddTicks(5725));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SchoolTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 11, 2, 15, 556, DateTimeKind.Utc).AddTicks(1075),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 13, 33, 28, 952, DateTimeKind.Utc).AddTicks(3356));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DocumentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 24, 11, 2, 15, 554, DateTimeKind.Utc).AddTicks(7247),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 24, 13, 33, 28, 864, DateTimeKind.Utc).AddTicks(8473));
        }
    }
}
