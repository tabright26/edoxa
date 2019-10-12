using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Identity.Api.Infrastructure.Data.Migrations
{
    public partial class UserCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "User",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "User");
        }
    }
}
