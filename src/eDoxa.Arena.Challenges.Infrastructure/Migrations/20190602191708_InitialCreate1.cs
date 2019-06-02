using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Arena.Challenges.Infrastructure.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "edoxa",
                table: "Payout",
                nullable: false,
                defaultValue: new Guid("51d30870-b1a1-4042-92d3-e7400b2b27f9"),
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "edoxa",
                table: "Payout",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValue: new Guid("51d30870-b1a1-4042-92d3-e7400b2b27f9"));
        }
    }
}
