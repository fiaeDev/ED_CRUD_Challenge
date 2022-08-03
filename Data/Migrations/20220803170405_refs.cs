using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class refs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Devices_HealthId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_TypeId",
                table: "Devices");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_HealthId",
                table: "Devices",
                column: "HealthId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_TypeId",
                table: "Devices",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Devices_HealthId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_TypeId",
                table: "Devices");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_HealthId",
                table: "Devices",
                column: "HealthId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_TypeId",
                table: "Devices",
                column: "TypeId",
                unique: true);
        }
    }
}
