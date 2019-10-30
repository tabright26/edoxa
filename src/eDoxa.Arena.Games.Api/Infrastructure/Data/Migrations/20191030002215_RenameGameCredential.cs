using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Arena.Games.Api.Infrastructure.Data.Migrations
{
    public partial class RenameGameCredential : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameCredential");

            migrationBuilder.CreateTable(
                name: "Credential",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Game = table.Column<int>(nullable: false),
                    PlayerId = table.Column<string>(nullable: false),
                    Timestamp = table.Column<long>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "GameCredential",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Game = table.Column<int>(nullable: false),
                    PlayerId = table.Column<string>(nullable: false),
                    Timestamp = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCredential", x => new { x.UserId, x.Game });
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameCredential_Game_PlayerId",
                table: "GameCredential",
                columns: new[] { "Game", "PlayerId" },
                unique: true);
        }
    }
}
