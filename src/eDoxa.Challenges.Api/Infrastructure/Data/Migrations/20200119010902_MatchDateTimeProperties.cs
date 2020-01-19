using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Challenges.Api.Infrastructure.Data.Migrations
{
    public partial class MatchDateTimeProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GameDuration",
                table: "Match",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "GameStartedAt",
                table: "Match",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SynchronizedAt",
                table: "Match",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameDuration",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "GameStartedAt",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "SynchronizedAt",
                table: "Match");
        }
    }
}
