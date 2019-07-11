// Filename: 20190711194145_InitialCreate.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                }
            );

            migrationBuilder.CreateTable(
                "Challenge",
                table => new
                {
                    Id = table.Column<Guid>(),
                    EntryFeeCurrency = table.Column<int>(),
                    EntryFeeAmount = table.Column<decimal>("decimal(11, 2)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenge", x => x.Id);
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

            migrationBuilder.CreateTable(
                "Bucket",
                table => new
                {
                    Id = table.Column<Guid>(),
                    ChallengeId = table.Column<Guid>(),
                    Size = table.Column<int>(),
                    PrizeCurrency = table.Column<int>(),
                    PrizeAmount = table.Column<decimal>("decimal(11, 2)")
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_Bucket",
                        x => new
                        {
                            x.ChallengeId,
                            x.Id
                        }
                    );

                    table.ForeignKey(
                        "FK_Bucket_Challenge_ChallengeId",
                        x => x.ChallengeId,
                        "Challenge",
                        "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex("IX_Transaction_AccountId", "Transaction", "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Bucket");

            migrationBuilder.DropTable("Transaction");

            migrationBuilder.DropTable("Challenge");

            migrationBuilder.DropTable("Account");
        }
    }
}
