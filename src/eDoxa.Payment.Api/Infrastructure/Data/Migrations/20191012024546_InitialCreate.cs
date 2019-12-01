using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Payment.Api.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StripeReference",
                columns: table => new
                {
                    UserId = table.Column<Guid>(),
                    CustomerId = table.Column<string>(),
                    AccountId = table.Column<string>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeReference", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StripeReference");
        }
    }
}
