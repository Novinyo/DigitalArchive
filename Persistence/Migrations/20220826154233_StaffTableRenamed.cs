using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class StaffTableRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Staff_StaffId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_EntityTypes_StaffTypeId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Schools_SchoolId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Users_UserId",
                table: "Staff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staff",
                table: "Staff");

            migrationBuilder.RenameTable(
                name: "Staff",
                newName: "Staffs");

            migrationBuilder.RenameIndex(
                name: "IX_Staff_UserId",
                table: "Staffs",
                newName: "IX_Staffs_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Staff_StaffTypeId",
                table: "Staffs",
                newName: "IX_Staffs_StaffTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Staff_SchoolId",
                table: "Staffs",
                newName: "IX_Staffs_SchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_Staff_Code_SchoolId",
                table: "Staffs",
                newName: "IX_Staffs_Code_SchoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staffs",
                table: "Staffs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Staffs_StaffId",
                table: "Documents",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_EntityTypes_StaffTypeId",
                table: "Staffs",
                column: "StaffTypeId",
                principalTable: "EntityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Schools_SchoolId",
                table: "Staffs",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Users_UserId",
                table: "Staffs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Staffs_StaffId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_EntityTypes_StaffTypeId",
                table: "Staffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Schools_SchoolId",
                table: "Staffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Users_UserId",
                table: "Staffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staffs",
                table: "Staffs");

            migrationBuilder.RenameTable(
                name: "Staffs",
                newName: "Staff");

            migrationBuilder.RenameIndex(
                name: "IX_Staffs_UserId",
                table: "Staff",
                newName: "IX_Staff_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Staffs_StaffTypeId",
                table: "Staff",
                newName: "IX_Staff_StaffTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Staffs_SchoolId",
                table: "Staff",
                newName: "IX_Staff_SchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_Staffs_Code_SchoolId",
                table: "Staff",
                newName: "IX_Staff_Code_SchoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staff",
                table: "Staff",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Staff_StaffId",
                table: "Documents",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_EntityTypes_StaffTypeId",
                table: "Staff",
                column: "StaffTypeId",
                principalTable: "EntityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Schools_SchoolId",
                table: "Staff",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Users_UserId",
                table: "Staff",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
