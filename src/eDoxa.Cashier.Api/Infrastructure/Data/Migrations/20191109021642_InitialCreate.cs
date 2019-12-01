using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    UserId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Challenge",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    EntryFeeCurrency = table.Column<int>(),
                    EntryFeeAmount = table.Column<decimal>(type: "decimal(11, 2)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    Timestamp = table.Column<DateTime>(),
                    Amount = table.Column<decimal>(type: "decimal(10, 2)"),
                    Currency = table.Column<int>(),
                    Type = table.Column<int>(),
                    Status = table.Column<int>(),
                    Description = table.Column<string>(),
                    AccountId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bucket",
                columns: table => new
                {
                    ChallengeId = table.Column<Guid>(),
                    Id = table.Column<Guid>(),
                    Size = table.Column<int>(),
                    PrizeCurrency = table.Column<int>(),
                    PrizeAmount = table.Column<decimal>(type: "decimal(11, 2)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bucket", x => new { x.ChallengeId, x.Id });
                    table.ForeignKey(
                        name: "FK_Bucket_Challenge_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionMetadata",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(),
                    Id = table.Column<Guid>(),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionMetadata", x => new { x.TransactionId, x.Id });
                    table.ForeignKey(
                        name: "FK_TransactionMetadata_Transaction_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_AccountId",
                table: "Transaction",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bucket");

            migrationBuilder.DropTable(
                name: "TransactionMetadata");

            migrationBuilder.DropTable(
                name: "Challenge");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
