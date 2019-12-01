using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Games.Api.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Credential",
                columns: table => new
                {
                    UserId = table.Column<Guid>(),
                    Game = table.Column<int>(),
                    PlayerId = table.Column<string>(),
                    Timestamp = table.Column<long>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credential", x => new { x.UserId, x.Game });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Credential_Game_PlayerId",
                table: "Credential",
                columns: new[] { "Game", "PlayerId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Credential");
        }
    }
}
