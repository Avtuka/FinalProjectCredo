using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingManagement.Persistence.Migrations
{
    public partial class NamesChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Card_CardId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Account_User_OwnerId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_User_RecieverId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_User_SenderId",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Operator",
                table: "Operator");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Card",
                table: "Card");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Transaction",
                newName: "Transactions");

            migrationBuilder.RenameTable(
                name: "Operator",
                newName: "Operators");

            migrationBuilder.RenameTable(
                name: "Card",
                newName: "Cards");

            migrationBuilder.RenameTable(
                name: "Account",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_User_PrivateNumber",
                table: "Users",
                newName: "IX_Users_PrivateNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_ToIBAN",
                table: "Transactions",
                newName: "IX_Transactions_ToIBAN");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_SenderId",
                table: "Transactions",
                newName: "IX_Transactions_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_RecieverId",
                table: "Transactions",
                newName: "IX_Transactions_RecieverId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_FromIBAN",
                table: "Transactions",
                newName: "IX_Transactions_FromIBAN");

            migrationBuilder.RenameIndex(
                name: "IX_Operator_PrivateNumber",
                table: "Operators",
                newName: "IX_Operators_PrivateNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Card_CardNumber",
                table: "Cards",
                newName: "IX_Cards_CardNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Account_OwnerId",
                table: "Accounts",
                newName: "IX_Accounts_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Account_IBAN",
                table: "Accounts",
                newName: "IX_Accounts_IBAN");

            migrationBuilder.RenameIndex(
                name: "IX_Account_CardId",
                table: "Accounts",
                newName: "IX_Accounts_CardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Operators",
                table: "Operators",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cards",
                table: "Cards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Cards_CardId",
                table: "Accounts",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_OwnerId",
                table: "Accounts",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_RecieverId",
                table: "Transactions",
                column: "RecieverId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_SenderId",
                table: "Transactions",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Cards_CardId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_OwnerId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_RecieverId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_SenderId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Operators",
                table: "Operators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cards",
                table: "Cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transaction");

            migrationBuilder.RenameTable(
                name: "Operators",
                newName: "Operator");

            migrationBuilder.RenameTable(
                name: "Cards",
                newName: "Card");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Account");

            migrationBuilder.RenameIndex(
                name: "IX_Users_PrivateNumber",
                table: "User",
                newName: "IX_User_PrivateNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_ToIBAN",
                table: "Transaction",
                newName: "IX_Transaction_ToIBAN");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_SenderId",
                table: "Transaction",
                newName: "IX_Transaction_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_RecieverId",
                table: "Transaction",
                newName: "IX_Transaction_RecieverId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_FromIBAN",
                table: "Transaction",
                newName: "IX_Transaction_FromIBAN");

            migrationBuilder.RenameIndex(
                name: "IX_Operators_PrivateNumber",
                table: "Operator",
                newName: "IX_Operator_PrivateNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_CardNumber",
                table: "Card",
                newName: "IX_Card_CardNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_OwnerId",
                table: "Account",
                newName: "IX_Account_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_IBAN",
                table: "Account",
                newName: "IX_Account_IBAN");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_CardId",
                table: "Account",
                newName: "IX_Account_CardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Operator",
                table: "Operator",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Card",
                table: "Card",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Card_CardId",
                table: "Account",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Account_User_OwnerId",
                table: "Account",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_User_RecieverId",
                table: "Transaction",
                column: "RecieverId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_User_SenderId",
                table: "Transaction",
                column: "SenderId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}