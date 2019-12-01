﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Clans.Api.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidature",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    UserId = table.Column<Guid>(),
                    ClanId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidature", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clan",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    Name = table.Column<string>(),
                    Summary = table.Column<string>(nullable: true),
                    OwnerId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invitation",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    UserId = table.Column<Guid>(),
                    ClanId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    UserId = table.Column<Guid>(),
                    ClanId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_Clan_ClanId",
                        column: x => x.ClanId,
                        principalTable: "Clan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Member_ClanId",
                table: "Member",
                column: "ClanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidature");

            migrationBuilder.DropTable(
                name: "Invitation");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Clan");
        }
    }
}
