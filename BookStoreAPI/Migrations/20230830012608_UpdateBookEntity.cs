using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Orders_Orderid",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Orderid",
                table: "Books",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_Orderid",
                table: "Books",
                newName: "IX_Books_OrderId");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "Books",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Orders_OrderId",
                table: "Books",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Orders_OrderId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Books",
                newName: "Orderid");

            migrationBuilder.RenameIndex(
                name: "IX_Books_OrderId",
                table: "Books",
                newName: "IX_Books_Orderid");

            migrationBuilder.AlterColumn<Guid>(
                name: "Orderid",
                table: "Books",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Orders_Orderid",
                table: "Books",
                column: "Orderid",
                principalTable: "Orders",
                principalColumn: "id");
        }
    }
}
