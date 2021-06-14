using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class FixEnergyConfigureModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_ElectricBills_ElectricBillId",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalAccounts_ElectricityMeters_ElectricityMeterId",
                table: "PersonalAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalAccounts_Tariff_TariffId",
                table: "PersonalAccounts");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_ElectricBills_ElectricBillId",
                table: "Buildings",
                column: "ElectricBillId",
                principalTable: "ElectricBills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalAccounts_ElectricityMeters_ElectricityMeterId",
                table: "PersonalAccounts",
                column: "ElectricityMeterId",
                principalTable: "ElectricityMeters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalAccounts_Tariff_TariffId",
                table: "PersonalAccounts",
                column: "TariffId",
                principalTable: "Tariff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_ElectricBills_ElectricBillId",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalAccounts_ElectricityMeters_ElectricityMeterId",
                table: "PersonalAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalAccounts_Tariff_TariffId",
                table: "PersonalAccounts");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_ElectricBills_ElectricBillId",
                table: "Buildings",
                column: "ElectricBillId",
                principalTable: "ElectricBills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalAccounts_ElectricityMeters_ElectricityMeterId",
                table: "PersonalAccounts",
                column: "ElectricityMeterId",
                principalTable: "ElectricityMeters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalAccounts_Tariff_TariffId",
                table: "PersonalAccounts",
                column: "TariffId",
                principalTable: "Tariff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
