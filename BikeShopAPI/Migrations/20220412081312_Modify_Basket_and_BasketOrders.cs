using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeShopAPI.Migrations
{
    public partial class Modify_Basket_and_BasketOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Baskets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EMail",
                table: "Baskets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HouseNumber",
                table: "Baskets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Baskets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Baskets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Baskets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "BasketOrders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_UserId",
                table: "Baskets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketOrders_UserId",
                table: "BasketOrders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketOrders_Users_UserId",
                table: "BasketOrders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Users_UserId",
                table: "Baskets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketOrders_Users_UserId",
                table: "BasketOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Users_UserId",
                table: "Baskets");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_UserId",
                table: "Baskets");

            migrationBuilder.DropIndex(
                name: "IX_BasketOrders_UserId",
                table: "BasketOrders");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "EMail",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "HouseNumber",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BasketOrders");
        }
    }
}
