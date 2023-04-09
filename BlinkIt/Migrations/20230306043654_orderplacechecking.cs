using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlinkIt.Migrations
{
    public partial class orderplacechecking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersReceiveditems_OrdersReceived_ROrderId",
                table: "OrdersReceiveditems");

            migrationBuilder.AlterColumn<int>(
                name: "ROrderId",
                table: "OrdersReceiveditems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "OrdersReceiveditems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersReceiveditems_OrdersReceived_ROrderId",
                table: "OrdersReceiveditems",
                column: "ROrderId",
                principalTable: "OrdersReceived",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersReceiveditems_OrdersReceived_ROrderId",
                table: "OrdersReceiveditems");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "OrdersReceiveditems");

            migrationBuilder.AlterColumn<int>(
                name: "ROrderId",
                table: "OrdersReceiveditems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersReceiveditems_OrdersReceived_ROrderId",
                table: "OrdersReceiveditems",
                column: "ROrderId",
                principalTable: "OrdersReceived",
                principalColumn: "Id");
        }
    }
}
