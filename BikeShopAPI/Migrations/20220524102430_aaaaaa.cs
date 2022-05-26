using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeShopAPI.Migrations
{
    public partial class aaaaaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BikeShops_Addresses_AddressId",
                table: "BikeShops");

            migrationBuilder.DropIndex(
                name: "IX_BikeShops_AddressId",
                table: "BikeShops");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "BikeShops",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_BikeShops_AddressId",
                table: "BikeShops",
                column: "AddressId",
                unique: true,
                filter: "[AddressId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_BikeShops_Addresses_AddressId",
                table: "BikeShops",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BikeShops_Addresses_AddressId",
                table: "BikeShops");

            migrationBuilder.DropIndex(
                name: "IX_BikeShops_AddressId",
                table: "BikeShops");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "BikeShops",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BikeShops_AddressId",
                table: "BikeShops",
                column: "AddressId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BikeShops_Addresses_AddressId",
                table: "BikeShops",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
