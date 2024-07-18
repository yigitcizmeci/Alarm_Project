using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alarm_Project.Migrations
{
    /// <inheritdoc />
    public partial class _27_06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Alarm",
                columns: new[] { "AlarmId", "AlarmMessage", "AlarmType", "Time", "UserId" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), "Exception", "Warning", new DateTime(2024, 6, 27, 17, 2, 20, 855, DateTimeKind.Local).AddTicks(1814), new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.CreateIndex(
                name: "IX_Alarm_UserId",
                table: "Alarm",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alarm_Users_UserId",
                table: "Alarm",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlarmSettings_Alarm_AlarmId",
                table: "AlarmSettings",
                column: "AlarmId",
                principalTable: "Alarm",
                principalColumn: "AlarmId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alarm_Users_UserId",
                table: "Alarm");

            migrationBuilder.DropForeignKey(
                name: "FK_AlarmSettings_Alarm_AlarmId",
                table: "AlarmSettings");

            migrationBuilder.DropIndex(
                name: "IX_Alarm_UserId",
                table: "Alarm");

            migrationBuilder.DeleteData(
                table: "Alarm",
                keyColumn: "AlarmId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));
        }
    }
}
