// Filename: 20190518221430_InitialCreate.cs
// Date Created: 2019-05-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Cashier.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema("edoxa");

            migrationBuilder.EnsureSchema("dbo");

            migrationBuilder.CreateTable(
                "Logs",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    Date = table.Column<DateTime>(),
                    Version = table.Column<string>(nullable: true),
                    Origin = table.Column<string>(nullable: true),
                    Method = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    LocalIpAddress = table.Column<string>(nullable: true),
                    RemoteIpAddress = table.Column<string>(nullable: true),
                    RequestBody = table.Column<string>(nullable: true),
                    RequestType = table.Column<string>(nullable: true),
                    ResponseBody = table.Column<string>(nullable: true),
                    ResponseType = table.Column<string>(nullable: true),
                    IdempotencyKey = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                "MoneyAccounts",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    UserId = table.Column<Guid>(),
                    LastDeposit = table.Column<DateTime>(nullable: true),
                    LastWithdraw = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyAccounts", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                "TokenAccounts",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    UserId = table.Column<Guid>(),
                    LastDeposit = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenAccounts", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                "MoneyTransactions",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    Timestamp = table.Column<DateTime>(),
                    Amount = table.Column<decimal>(),
                    Description = table.Column<string>(),
                    Failure = table.Column<string>(nullable: true),
                    Type = table.Column<int>(),
                    Status = table.Column<int>(),
                    AccountId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyTransactions", x => x.Id);

                    table.ForeignKey(
                        "FK_MoneyTransactions_MoneyAccounts_AccountId",
                        x => x.AccountId,
                        principalSchema: "edoxa",
                        principalTable: "MoneyAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "TokenTransactions",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    Timestamp = table.Column<DateTime>(),
                    Amount = table.Column<long>(),
                    Description = table.Column<string>(),
                    Failure = table.Column<string>(nullable: true),
                    Type = table.Column<int>(),
                    Status = table.Column<int>(),
                    AccountId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenTransactions", x => x.Id);

                    table.ForeignKey(
                        "FK_TokenTransactions_TokenAccounts_AccountId",
                        x => x.AccountId,
                        principalSchema: "edoxa",
                        principalTable: "TokenAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                "IX_Logs_IdempotencyKey",
                schema: "dbo",
                table: "Logs",
                column: "IdempotencyKey",
                unique: true,
                filter: "[IdempotencyKey] IS NOT NULL"
            );

            migrationBuilder.CreateIndex("IX_MoneyTransactions_AccountId", schema: "edoxa", table: "MoneyTransactions", column: "AccountId");

            migrationBuilder.CreateIndex("IX_TokenTransactions_AccountId", schema: "edoxa", table: "TokenTransactions", column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Logs", "dbo");

            migrationBuilder.DropTable("MoneyTransactions", "edoxa");

            migrationBuilder.DropTable("TokenTransactions", "edoxa");

            migrationBuilder.DropTable("MoneyAccounts", "edoxa");

            migrationBuilder.DropTable("TokenAccounts", "edoxa");
        }
    }
}
