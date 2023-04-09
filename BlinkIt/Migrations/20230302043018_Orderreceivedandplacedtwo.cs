using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlinkIt.Migrations
{
    public partial class Orderreceivedandplacedtwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdersPlaced",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<double>(type: "float", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersPlaced", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdersReceived",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<double>(type: "float", nullable: false),
                    OrderedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SellerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersReceived", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderPlacedItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    total = table.Column<double>(type: "float", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPlacedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPlacedItems_OrdersPlaced_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrdersPlaced",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPlacedItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdersReceiveditems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SellerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ROrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersReceiveditems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdersReceiveditems_OrdersReceived_ROrderId",
                        column: x => x.ROrderId,
                        principalTable: "OrdersReceived",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrdersReceiveditems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderPlacedItems_OrderId",
                table: "OrderPlacedItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPlacedItems_ProductId",
                table: "OrderPlacedItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersReceiveditems_ProductId",
                table: "OrdersReceiveditems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersReceiveditems_ROrderId",
                table: "OrdersReceiveditems",
                column: "ROrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderPlacedItems");

            migrationBuilder.DropTable(
                name: "OrdersReceiveditems");

            migrationBuilder.DropTable(
                name: "OrdersPlaced");

            migrationBuilder.DropTable(
                name: "OrdersReceived");
        }
    }
}
