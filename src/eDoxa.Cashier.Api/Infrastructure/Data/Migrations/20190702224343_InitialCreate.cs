// Filename: 20190702224343_InitialCreate.cs
// Date Created: 2019-07-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "User",
                table => new
                {
                    Id = table.Column<Guid>(),
                    ConnectAccountId = table.Column<string>(),
                    CustomerId = table.Column<string>(),
                    BankAccountId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                "Account",
                table => new
                {
                    Id = table.Column<Guid>(),
                    UserId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);

                    table.ForeignKey(
                        "FK_Account_User_UserId",
                        x => x.UserId,
                        "User",
                        "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "Transaction",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Timestamp = table.Column<DateTime>(),
                    Amount = table.Column<decimal>("decimal(10, 2)"),
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
                        "FK_Transaction_Account_AccountId",
                        x => x.AccountId,
                        "Account",
                        "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex("IX_Account_UserId", "Account", "UserId", unique: true);

            migrationBuilder.CreateIndex("IX_Transaction_AccountId", "Transaction", "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Transaction");

            migrationBuilder.DropTable("Account");

            migrationBuilder.DropTable("User");
        }
    }
}
