using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alarm_Project.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPreferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Alarm",
                table: "Alarm");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Alarm");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Alarm",
                newName: "AlarmType");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Alarm",
                newName: "AlarmMessage");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Alarm",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "AlarmId",
                table: "Alarm",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Alarm",
                table: "Alarm",
                column: "AlarmId");

            migrationBuilder.CreateTable(
                name: "AlarmSettings",
                columns: table => new
                {
                    AlarmId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiveReport = table.Column<bool>(type: "bit", nullable: false),
                    ReceiveEmail = table.Column<bool>(type: "bit", nullable: false),
                    ReceiveSlack = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmSettings", x => x.AlarmId);
                    table.ForeignKey(
                        name: "FK_AlarmSettings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardOwnerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpireYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CVV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Payment_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Products_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlarmSettings_UserId",
                table: "AlarmSettings",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmSettings");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Alarm",
                table: "Alarm");

            migrationBuilder.DropColumn(
                name: "AlarmId",
                table: "Alarm");

            migrationBuilder.RenameColumn(
                name: "AlarmType",
                table: "Alarm",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "AlarmMessage",
                table: "Alarm",
                newName: "Message");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Alarm",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Alarm",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Alarm",
                table: "Alarm",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserPreferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiveEmail = table.Column<bool>(type: "bit", nullable: false),
                    ReceiveReport = table.Column<bool>(type: "bit", nullable: false),
                    ReceiveSlack = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPreferences_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_UserId",
                table: "UserPreferences",
                column: "UserId",
                unique: true);
        }
    }
}
