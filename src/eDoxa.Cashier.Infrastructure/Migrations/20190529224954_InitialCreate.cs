using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Cashier.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "edoxa");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccountId = table.Column<string>(nullable: false),
                    CustomerId = table.Column<string>(nullable: false),
                    BankAccountId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoneyAccounts",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    LastDeposit = table.Column<DateTime>(nullable: true),
                    LastWithdraw = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoneyAccounts_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "edoxa",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TokenAccounts",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    LastDeposit = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokenAccounts_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "edoxa",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoneyTransactions",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Failure = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoneyTransactions_MoneyAccounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "edoxa",
                        principalTable: "MoneyAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TokenTransactions",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Failure = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokenTransactions_TokenAccounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "edoxa",
                        principalTable: "TokenAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoneyAccounts_UserId",
                schema: "edoxa",
                table: "MoneyAccounts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoneyTransactions_AccountId",
                schema: "edoxa",
                table: "MoneyTransactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TokenAccounts_UserId",
                schema: "edoxa",
                table: "TokenAccounts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TokenTransactions_AccountId",
                schema: "edoxa",
                table: "TokenTransactions",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoneyTransactions",
                schema: "edoxa");

            migrationBuilder.DropTable(
                name: "TokenTransactions",
                schema: "edoxa");

            migrationBuilder.DropTable(
                name: "MoneyAccounts",
                schema: "edoxa");

            migrationBuilder.DropTable(
                name: "TokenAccounts",
                schema: "edoxa");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "edoxa");
        }
    }
}
