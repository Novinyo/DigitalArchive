using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddedCategoryToDocumentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "DocumentTypes",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DocumentTypes_Code",
                table: "DocumentTypes");

            migrationBuilder.DropIndex(
                name: "IX_DocumentTypes_Name",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "DocumentTypes");

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
                filter: "[SchoolId] IS NOT NULL AND [Name] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTypes_Schools_SchoolId",
                table: "DocumentTypes",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
