using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Migrations
{
    /// <inheritdoc />
    public partial class initialCree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurant_Food_Food_FoodId",
                table: "Restaurant_Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurant_Food_Restaurant_RestaurantId",
                table: "Restaurant_Food");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restaurant_Food",
                table: "Restaurant_Food");

            migrationBuilder.DropIndex(
                name: "IX_Restaurant_Food_Food_ID",
                table: "Restaurant_Food");

            migrationBuilder.DropIndex(
                name: "IX_Restaurant_Food_FoodId",
                table: "Restaurant_Food");

            migrationBuilder.DropIndex(
                name: "IX_Restaurant_Food_RestaurantId",
                table: "Restaurant_Food");

            migrationBuilder.DropColumn(
                name: "FoodId",
                table: "Restaurant_Food");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Restaurant_Food");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restaurant_Food",
                table: "Restaurant_Food",
                columns: new[] { "Food_ID", "Restaurant_ID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Restaurant_Food",
                table: "Restaurant_Food");

            migrationBuilder.AddColumn<Guid>(
                name: "FoodId",
                table: "Restaurant_Food",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RestaurantId",
                table: "Restaurant_Food",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restaurant_Food",
                table: "Restaurant_Food",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_Food_Food_ID",
                table: "Restaurant_Food",
                column: "Food_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_Food_FoodId",
                table: "Restaurant_Food",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_Food_RestaurantId",
                table: "Restaurant_Food",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurant_Food_Food_FoodId",
                table: "Restaurant_Food",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurant_Food_Restaurant_RestaurantId",
                table: "Restaurant_Food",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id");
        }
    }
}
