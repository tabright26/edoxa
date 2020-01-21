using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Migrations
{
    public partial class PromotionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PromotionalCode = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    Currency = table.Column<int>(nullable: false),
                    Duration = table.Column<long>(nullable: false),
                    ExpiredAt = table.Column<DateTime>(nullable: false),
                    CanceledAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PromotionRecipient",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    PromotionId = table.Column<Guid>(nullable: false),
                    RedeemedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionRecipient", x => new { x.PromotionId, x.UserId });
                    table.ForeignKey(
                        name: "FK_PromotionRecipient_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromotionRecipient");

            migrationBuilder.DropTable(
                name: "Promotion");
        }
    }
}
