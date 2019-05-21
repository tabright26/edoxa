using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Arena.Challenges.Infrastructure.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Payout",
                schema: "edoxa",
                table: "Challenges",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Payout",
                schema: "edoxa",
                table: "Challenges");
        }
    }
}
