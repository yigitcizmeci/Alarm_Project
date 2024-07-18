using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alarm_Project.Migrations
{
    /// <inheritdoc />
    public partial class _23_06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Users_UserId",
                table: "Payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "PaymentId");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Password", "UserName" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "yigitcizmeci@hotmail.com", "cizmeci", "yigit" });

            migrationBuilder.InsertData(
                table: "AlarmSettings",
                columns: new[] { "AlarmId", "ReceiveEmail", "ReceiveReport", "ReceiveSlack", "UserId" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), false, false, false, new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "CVV", "CardNumber", "CardOwnerName", "Currency", "ExpireYear", "UserId" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), "000", "4355084355084358", "Test Kart", 2000, "2030", new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ProductDescripcion", "ProductName", "ProductPrice", "UserId" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), "Ateş seni çağırıyor", "Mangal", 700, new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Users_UserId",
                table: "Payments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Users_UserId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_UserId",
                table: "Payments");

            migrationBuilder.DeleteData(
                table: "AlarmSettings",
                keyColumn: "AlarmId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Users_UserId",
                table: "Payment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
