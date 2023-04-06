using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingManagement.Persistence.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<string>(type: "varchar(900)", unicode: false, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    CVC = table.Column<short>(type: "smallint", nullable: false),
                    PIN = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrivateNumber = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Email = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(512)", unicode: false, maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrivateNumber = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Email = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(512)", unicode: false, maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    IBAN = table.Column<string>(type: "varchar(900)", unicode: false, nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Account_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_CardId",
                table: "Account",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_IBAN",
                table: "Account",
                column: "IBAN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_OwnerId",
                table: "Account",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_CardNumber",
                table: "Card",
                column: "CardNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_PrivateNumber",
                table: "User",
                column: "PrivateNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Operator");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}