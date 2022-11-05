using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class addedMediaclFieldToStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConditionRemarks",
                table: "Students",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Students",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HaveMedicalCondition",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConditionRemarks",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "HaveMedicalCondition",
                table: "Students");
        }
    }
}
