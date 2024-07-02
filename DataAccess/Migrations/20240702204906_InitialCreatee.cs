using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Food_category_Category_ID",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurant_City_City_ID",
                table: "Restaurant");

            migrationBuilder.RenameColumn(
                name: "City_ID",
                table: "Restaurant",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurant_City_ID",
                table: "Restaurant",
                newName: "IX_Restaurant_CityId");

            migrationBuilder.RenameColumn(
                name: "ZIP",
                table: "City",
                newName: "Zip");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Food",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<Guid>(
                name: "Category_ID",
                table: "Food",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "County",
                table: "City",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "City_code",
                table: "City",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Food_category_Category_ID",
                table: "Food",
                column: "Category_ID",
                principalTable: "Food_category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurant_City_CityId",
                table: "Restaurant",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Food_category_Category_ID",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurant_City_CityId",
                table: "Restaurant");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Restaurant",
                newName: "City_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurant_CityId",
                table: "Restaurant",
                newName: "IX_Restaurant_City_ID");

            migrationBuilder.RenameColumn(
                name: "Zip",
                table: "City",
                newName: "ZIP");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Food",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Category_ID",
                table: "Food",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "County",
                table: "City",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City_code",
                table: "City",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Food_category_Category_ID",
                table: "Food",
                column: "Category_ID",
                principalTable: "Food_category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurant_City_City_ID",
                table: "Restaurant",
                column: "City_ID",
                principalTable: "City",
                principalColumn: "Id");
        }
    }
}
