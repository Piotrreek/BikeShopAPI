using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeShopAPI.Migrations
{
    public partial class UserContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Specifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "BikeShops",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Bikes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Bags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_CreatedById",
                table: "Specifications",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedById",
                table: "Products",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_BikeShops_CreatedById",
                table: "BikeShops",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Bikes_CreatedById",
                table: "Bikes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Bags_CreatedById",
                table: "Bags",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Bags_Users_CreatedById",
                table: "Bags",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bikes_Users_CreatedById",
                table: "Bikes",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BikeShops_Users_CreatedById",
                table: "BikeShops",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_CreatedById",
                table: "Products",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Specifications_Users_CreatedById",
                table: "Specifications",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bags_Users_CreatedById",
                table: "Bags");

            migrationBuilder.DropForeignKey(
                name: "FK_Bikes_Users_CreatedById",
                table: "Bikes");

            migrationBuilder.DropForeignKey(
                name: "FK_BikeShops_Users_CreatedById",
                table: "BikeShops");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_CreatedById",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Specifications_Users_CreatedById",
                table: "Specifications");

            migrationBuilder.DropIndex(
                name: "IX_Specifications_CreatedById",
                table: "Specifications");

            migrationBuilder.DropIndex(
                name: "IX_Products_CreatedById",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_BikeShops_CreatedById",
                table: "BikeShops");

            migrationBuilder.DropIndex(
                name: "IX_Bikes_CreatedById",
                table: "Bikes");

            migrationBuilder.DropIndex(
                name: "IX_Bags_CreatedById",
                table: "Bags");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "BikeShops");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Bikes");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Bags");
        }
    }
}
