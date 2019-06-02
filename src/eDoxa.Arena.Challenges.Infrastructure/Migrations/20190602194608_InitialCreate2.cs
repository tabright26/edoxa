using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Arena.Challenges.Infrastructure.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Scoring",
                schema: "edoxa",
                table: "Scoring");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "edoxa",
                table: "Challenges",
                newName: "Name_Value");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "edoxa",
                table: "Scoring",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "edoxa",
                table: "Scoring",
                nullable: false,
                defaultValue: new Guid("211dfed2-d7ad-47e7-a761-2c39fb7d3c4b"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "edoxa",
                table: "Payout",
                nullable: false,
                defaultValue: new Guid("01fe4fce-9518-4008-ba06-a84fe4b96f37"),
                oldClrType: typeof(Guid),
                oldDefaultValue: new Guid("51d30870-b1a1-4042-92d3-e7400b2b27f9"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scoring",
                schema: "edoxa",
                table: "Scoring",
                columns: new[] { "ChallengeId", "Id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Scoring",
                schema: "edoxa",
                table: "Scoring");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "edoxa",
                table: "Scoring");

            migrationBuilder.RenameColumn(
                name: "Name_Value",
                schema: "edoxa",
                table: "Challenges",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "edoxa",
                table: "Scoring",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "edoxa",
                table: "Payout",
                nullable: false,
                defaultValue: new Guid("51d30870-b1a1-4042-92d3-e7400b2b27f9"),
                oldClrType: typeof(Guid),
                oldDefaultValue: new Guid("01fe4fce-9518-4008-ba06-a84fe4b96f37"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scoring",
                schema: "edoxa",
                table: "Scoring",
                columns: new[] { "ChallengeId", "Name", "Weighting" });
        }
    }
}
