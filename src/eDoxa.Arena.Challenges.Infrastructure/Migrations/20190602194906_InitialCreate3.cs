using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Arena.Challenges.Infrastructure.Migrations
{
    public partial class InitialCreate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "edoxa",
                table: "Scoring",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValue: new Guid("211dfed2-d7ad-47e7-a761-2c39fb7d3c4b"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "edoxa",
                table: "Payout",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValue: new Guid("01fe4fce-9518-4008-ba06-a84fe4b96f37"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "edoxa",
                table: "Scoring",
                nullable: false,
                defaultValue: new Guid("211dfed2-d7ad-47e7-a761-2c39fb7d3c4b"),
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "edoxa",
                table: "Payout",
                nullable: false,
                defaultValue: new Guid("01fe4fce-9518-4008-ba06-a84fe4b96f37"),
                oldClrType: typeof(Guid));
        }
    }
}
