using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CopaAirlines.RequestsService.DA.Migrations
{
    public partial class FirstDBModelFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NameRequestRegisters");

            migrationBuilder.CreateTable(
                name: "NameCorrectionRequests",
                columns: table => new
                {
                    NameCorrectionRequestID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AgencyID = table.Column<int>(nullable: false),
                    PNR = table.Column<int>(nullable: false),
                    CountryCode = table.Column<string>(maxLength: 2, nullable: false),
                    ServiceLanguage = table.Column<string>(maxLength: 2, nullable: false),
                    ContactPhoneNumber = table.Column<string>(maxLength: 20, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    RegisteredDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NameCorrectionRequests", x => x.NameCorrectionRequestID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NameCorrectionRequests");

            migrationBuilder.CreateTable(
                name: "NameRequestRegisters",
                columns: table => new
                {
                    NameRequestRegisterID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AgencyID = table.Column<int>(nullable: false),
                    ContactPhoneNumber = table.Column<string>(maxLength: 20, nullable: false),
                    CountryCode = table.Column<string>(maxLength: 2, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    PNR = table.Column<int>(nullable: false),
                    RegisteredDate = table.Column<DateTime>(nullable: false),
                    ServiceLanguage = table.Column<string>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NameRequestRegisters", x => x.NameRequestRegisterID);
                });
        }
    }
}
