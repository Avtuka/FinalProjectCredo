﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingManagement.Persistence.Migrations
{
    public partial class EmailConfirmedAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "User");
        }
    }
}