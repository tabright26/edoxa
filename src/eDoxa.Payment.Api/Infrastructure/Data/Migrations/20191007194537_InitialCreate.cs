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
                    UserId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<string>(nullable: false),
                    ConnectAccountId = table.Column<string>(nullable: false)
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
