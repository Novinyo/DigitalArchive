using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddedDocumentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DocumentTypes_Code",
                table: "DocumentTypes");

            migrationBuilder.DropIndex(
                name: "IX_DocumentTypes_Name",
                table: "DocumentTypes");

            migrationBuilder.DropIndex(
                name: "IX_DocumentTypes_SchoolId_Name",
                table: "DocumentTypes");

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DocumentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerType = table.Column<int>(type: "int", nullable: false),
                    DocumentURL = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_SchoolId_Name",
                table: "DocumentTypes",
                columns: new[] { "SchoolId", "Name" },
                unique: true,
                filter: "[SchoolId] IS NOT NULL AND [Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_Code",
                table: "Documents",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentTypeId",
                table: "Documents",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_Name",
                table: "Documents",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_DocumentTypes_SchoolId_Name",
                table: "DocumentTypes");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_Code",
                table: "DocumentTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_Name",
                table: "DocumentTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_SchoolId_Name",
                table: "DocumentTypes",
                columns: new[] { "SchoolId", "Name" },
                unique: true,
                filter: "[SchoolId] IS NOT NULL");
        }
    }
}
