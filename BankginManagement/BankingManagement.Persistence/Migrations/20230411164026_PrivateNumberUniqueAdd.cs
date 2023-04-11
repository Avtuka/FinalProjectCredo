using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingManagement.Persistence.Migrations
{
    public partial class PrivateNumberUniqueAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Operator_PrivateNumber",
                table: "Operator",
                column: "PrivateNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Operator_PrivateNumber",
                table: "Operator");
        }
    }
}
