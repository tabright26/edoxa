using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Clans.Api.Infrastructure.Data.Migrations
{
    public partial class ClanDivision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Division",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    ClanId = table.Column<Guid>(),
                    Name = table.Column<string>(),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Division", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Division_Clan_ClanId",
                        column: x => x.ClanId,
                        principalTable: "Clan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Division_ClanId",
                table: "Division",
                column: "ClanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Division");
        }
    }
}
