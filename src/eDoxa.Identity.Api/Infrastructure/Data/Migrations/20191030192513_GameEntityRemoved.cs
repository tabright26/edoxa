using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Identity.Api.Infrastructure.Data.Migrations
{
    public partial class GameEntityRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGame");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserGame",
                columns: table => new
                {
                    Value = table.Column<int>(nullable: false),
                    PlayerId = table.Column<string>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGame", x => new { x.Value, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_UserGame_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGame_UserId",
                table: "UserGame",
                column: "UserId");
        }
    }
}
