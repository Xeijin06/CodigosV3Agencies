using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CopaAirlines.AgenciesService.DA.Migrations
{
    public partial class firstDBModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agencies",
                columns: table => new
                {
                    AgencyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    IATACode = table.Column<string>(maxLength: 8, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.AgencyID);
                });

            migrationBuilder.CreateTable(
                name: "AgencyUsers",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AgencyID = table.Column<int>(nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 20, nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyUsers", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_AgencyUsers_Agencies_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agencies",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgencyUsers_AgencyID",
                table: "AgencyUsers",
                column: "AgencyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgencyUsers");

            migrationBuilder.DropTable(
                name: "Agencies");
        }
    }
}
