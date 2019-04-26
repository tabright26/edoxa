// Filename: 20190426171328_InitialCreate.cs
// Date Created: 2019-04-26
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
                "RequestLogs",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    Time = table.Column<DateTime>(),
                    Type = table.Column<int>(),
                    Method = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    LocalIpAddress = table.Column<string>(nullable: true),
                    RemoteIpAddress = table.Column<string>(nullable: true),
                    Version = table.Column<string>(nullable: true),
                    Origin = table.Column<string>(nullable: true),
                    IdempotencyKey = table.Column<Guid>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_RequestLogs", x => x.Id); });

            migrationBuilder.CreateTable(
                "Users",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    CustomerId = table.Column<string>()
                },
                constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });

            migrationBuilder.CreateTable(
                "MoneyAccounts",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    UserId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyAccounts", x => x.Id);

                    table.ForeignKey(
                        "FK_MoneyAccounts_Users_UserId",
                        x => x.UserId,
                        principalSchema: "edoxa",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "TokenAccounts",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    UserId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenAccounts", x => x.Id);

                    table.ForeignKey(
                        "FK_TokenAccounts_Users_UserId",
                        x => x.UserId,
                        principalSchema: "edoxa",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "MoneyTransactions",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    Timestamp = table.Column<DateTime>(),
                    Amount = table.Column<decimal>(),
                    ActivityId = table.Column<string>(nullable: true),
                    Pending = table.Column<bool>(),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "TokenTransactions",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    Timestamp = table.Column<DateTime>(),
                    Amount = table.Column<long>(),
                    ActivityId = table.Column<string>(nullable: true),
                    Pending = table.Column<bool>(),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_RequestLogs_IdempotencyKey",
                schema: "dbo",
                table: "RequestLogs",
                column: "IdempotencyKey",
                unique: true,
                filter: "[IdempotencyKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_MoneyAccounts_UserId",
                schema: "edoxa",
                table: "MoneyAccounts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_MoneyTransactions_AccountId",
                schema: "edoxa",
                table: "MoneyTransactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                "IX_TokenAccounts_UserId",
                schema: "edoxa",
                table: "TokenAccounts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_TokenTransactions_AccountId",
                schema: "edoxa",
                table: "TokenTransactions",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "RequestLogs",
                "dbo");

            migrationBuilder.DropTable(
                "MoneyTransactions",
                "edoxa");

            migrationBuilder.DropTable(
                "TokenTransactions",
                "edoxa");

            migrationBuilder.DropTable(
                "MoneyAccounts",
                "edoxa");

            migrationBuilder.DropTable(
                "TokenAccounts",
                "edoxa");

            migrationBuilder.DropTable(
                "Users",
                "edoxa");
        }
    }
}