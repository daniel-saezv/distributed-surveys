﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexToUsername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                schema: "auth",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                schema: "auth",
                table: "Users");
        }
    }
}
