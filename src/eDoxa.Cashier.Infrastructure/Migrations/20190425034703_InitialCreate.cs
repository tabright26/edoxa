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

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "RequestLogs",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Method = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    LocalIpAddress = table.Column<string>(nullable: true),
                    RemoteIpAddress = table.Column<string>(nullable: true),
                    Version = table.Column<string>(nullable: true),
                    Origin = table.Column<string>(nullable: true),
                    IdempotencyKey = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<string>(nullable: false)
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
                    Pending = table.Column<decimal>(nullable: false)
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
                    Pending = table.Column<long>(nullable: false)
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
                name: "IX_RequestLogs_IdempotencyKey",
                schema: "dbo",
                table: "RequestLogs",
                column: "IdempotencyKey",
                unique: true,
                filter: "[IdempotencyKey] IS NOT NULL");

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
                name: "RequestLogs",
                schema: "dbo");

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
