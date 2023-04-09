using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlinkIt.Migrations
{
    public partial class Orderreceivedandplaced : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "ShoppingCartItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "ShoppingCart",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "ShoppingCart",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "ShoppingCart");
        }
    }
}
