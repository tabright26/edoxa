// Filename: 20191027211813_InitialCreate.cs
// Date Created: 2019-10-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Arena.Games.Api.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "GameCredential",
                table => new
                {
                    UserId = table.Column<Guid>(),
                    Game = table.Column<int>(),
                    PlayerId = table.Column<string>(),
                    Timestamp = table.Column<long>()
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_GameCredential",
                        x => new
                        {
                            x.UserId,
                            x.Game
                        });
                });

            migrationBuilder.CreateIndex(
                "IX_GameCredential_Game_PlayerId",
                "GameCredential",
                new[] {"Game", "PlayerId"},
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("GameCredential");
        }
    }
}
