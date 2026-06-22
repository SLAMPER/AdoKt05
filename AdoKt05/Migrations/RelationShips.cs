using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdoKt05.Migrations
{
    /// <inheritdoc />
    public partial class _02_RelationShips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryEntityId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryEntityId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryEntityId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_FkCategoryId",
                table: "Products",
                column: "FkCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_FkCategoryId",
                table: "Products",
                column: "FkCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_FkCategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_FkCategoryId",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryEntityId",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryEntityId",
                table: "Products",
                column: "CategoryEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryEntityId",
                table: "Products",
                column: "CategoryEntityId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
