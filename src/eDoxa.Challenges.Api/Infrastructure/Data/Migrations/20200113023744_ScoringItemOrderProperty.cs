using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Challenges.Api.Infrastructure.Data.Migrations
{
    public partial class ScoringItemOrderProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "ScoringItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "ScoringItem");
        }
    }
}
