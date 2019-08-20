using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Identity.Api.Infrastructure.Data.Migrations
{
    public partial class UserDoxaTagHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoxaTag");

            migrationBuilder.CreateTable(
                name: "UserDoxaTag",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<int>(nullable: false),
                    Timestamp = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDoxaTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDoxaTag_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDoxaTag_UserId",
                table: "UserDoxaTag",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDoxaTag");

            migrationBuilder.CreateTable(
                name: "DoxaTag",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Code = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Timestamp = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoxaTag", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_DoxaTag_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
