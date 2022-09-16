using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class StaffTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StaffId",
                table: "Documents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateJoined = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateLeft = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StaffTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PostalAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StreetAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    HaveMedicalCondition = table.Column<bool>(type: "bit", nullable: false),
                    ConditionRemarks = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staff_EntityTypes_StaffTypeId",
                        column: x => x.StaffTypeId,
                        principalTable: "EntityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staff_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Staff_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_StaffId",
                table: "Documents",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_Code_SchoolId",
                table: "Staff",
                columns: new[] { "Code", "SchoolId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staff_SchoolId",
                table: "Staff",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_StaffTypeId",
                table: "Staff",
                column: "StaffTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_UserId",
                table: "Staff",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Staff_StaffId",
                table: "Documents",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Staff_StaffId",
                table: "Documents");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Documents_StaffId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "Documents");
        }
    }
}
