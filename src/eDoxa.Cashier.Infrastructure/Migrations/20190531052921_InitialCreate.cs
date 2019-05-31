// Filename: 20190531052921_InitialCreate.cs
// Date Created: 2019-05-31
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

            migrationBuilder.CreateTable(
                "Users",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    ConnectAccountId = table.Column<string>(),
                    CustomerId = table.Column<string>(),
                    BankAccountId = table.Column<string>(nullable: true)
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
                    Id = table.Column<Guid>(),
                    UserId = table.Column<Guid>()
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
                "Transactions",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    Currency_Type = table.Column<int>(),
                    Currency_Amount = table.Column<decimal>(),
                    Timestamp = table.Column<DateTime>(),
                    Type = table.Column<int>(),
                    Status = table.Column<int>(),
                    Description = table.Column<string>(),
                    Failure = table.Column<string>(nullable: true),
                    AccountId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);

                    table.ForeignKey(
                        "FK_Transactions_Accounts_AccountId",
                        x => x.AccountId,
                        principalSchema: "edoxa",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                "IX_Accounts_UserId",
                schema: "edoxa",
                table: "Accounts",
                column: "UserId",
                unique: true
            );

            migrationBuilder.CreateIndex("IX_Transactions_AccountId", schema: "edoxa", table: "Transactions", column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Transactions", "edoxa");

            migrationBuilder.DropTable("Accounts", "edoxa");

            migrationBuilder.DropTable("Users", "edoxa");
        }
    }
}
