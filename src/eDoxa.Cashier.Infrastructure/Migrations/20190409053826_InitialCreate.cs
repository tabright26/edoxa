// Filename: 20190409053826_InitialCreate.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
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
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestLogs", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                "Users",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(), CustomerId = table.Column<string>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                "Accounts",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(), UserId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);

                    table.ForeignKey(
                        "FK_Accounts_Users_UserId",
                        x => x.UserId,
                        principalSchema: "edoxa",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "Funds",
                schema: "edoxa",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(), Balance = table.Column<decimal>(), Pending = table.Column<decimal>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funds", x => x.AccountId);

                    table.ForeignKey(
                        "FK_Funds_Accounts_AccountId",
                        x => x.AccountId,
                        principalSchema: "edoxa",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "Tokens",
                schema: "edoxa",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(), Balance = table.Column<decimal>(), Pending = table.Column<decimal>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.AccountId);

                    table.ForeignKey(
                        "FK_Tokens_Accounts_AccountId",
                        x => x.AccountId,
                        principalSchema: "edoxa",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                "IX_RequestLogs_IdempotencyKey",
                schema: "dbo",
                table: "RequestLogs",
                column: "IdempotencyKey",
                unique: true,
                filter: "[IdempotencyKey] IS NOT NULL"
            );

            migrationBuilder.CreateIndex("IX_Accounts_UserId", schema: "edoxa", table: "Accounts", column: "UserId", unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("RequestLogs", "dbo");

            migrationBuilder.DropTable("Funds", "edoxa");

            migrationBuilder.DropTable("Tokens", "edoxa");

            migrationBuilder.DropTable("Accounts", "edoxa");

            migrationBuilder.DropTable("Users", "edoxa");
        }
    }
}