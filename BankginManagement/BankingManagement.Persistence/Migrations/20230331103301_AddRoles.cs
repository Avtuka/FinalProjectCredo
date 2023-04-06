using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingManagement.Persistence.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Operator",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Operator");
        }
    }
}