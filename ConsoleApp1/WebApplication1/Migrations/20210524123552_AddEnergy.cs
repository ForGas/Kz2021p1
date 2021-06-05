using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class AddEnergy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ElectricBills",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalDebt = table.Column<int>(type: "int", nullable: false),
                    Consumption = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectricBills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tariff",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Person = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariff", x => x.Id);
                });


            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalArea = table.Column<int>(type: "int", nullable: false),
                    ElectricBillId = table.Column<long>(type: "bigint", nullable: false),
                    DistrictId = table.Column<long>(type: "bigint", nullable: true),
                    AdressId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buildings_Adress_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Adress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Buildings_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Buildings_ElectricBills_ElectricBillId",
                        column: x => x.ElectricBillId,
                        principalTable: "ElectricBills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElectricityMeters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Consumption = table.Column<int>(type: "int", nullable: false),
                    Debt = table.Column<int>(type: "int", nullable: false),
                    ElectricBillId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectricityMeters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectricityMeters_ElectricBills_ElectricBillId",
                        column: x => x.ElectricBillId,
                        principalTable: "ElectricBills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonalAccounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    DateRegistration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TariffId = table.Column<long>(type: "bigint", nullable: false),
                    ElectricityMeterId = table.Column<long>(type: "bigint", nullable: false),
                    CitizenId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalAccounts_Citizens_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalAccounts_ElectricityMeters_ElectricityMeterId",
                        column: x => x.ElectricityMeterId,
                        principalTable: "ElectricityMeters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalAccounts_Tariff_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_AdressId",
                table: "Buildings",
                column: "AdressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_DistrictId",
                table: "Buildings",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_ElectricBillId",
                table: "Buildings",
                column: "ElectricBillId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ElectricityMeters_ElectricBillId",
                table: "ElectricityMeters",
                column: "ElectricBillId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectricityMeters_SerialNumber",
                table: "ElectricityMeters",
                column: "SerialNumber",
                unique: true,
                filter: "[SerialNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalAccounts_CitizenId",
                table: "PersonalAccounts",
                column: "CitizenId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalAccounts_ElectricityMeterId",
                table: "PersonalAccounts",
                column: "ElectricityMeterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalAccounts_TariffId",
                table: "PersonalAccounts",
                column: "TariffId",
                unique: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
