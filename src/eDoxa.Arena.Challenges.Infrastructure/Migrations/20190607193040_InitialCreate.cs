﻿// Filename: 20190607193040_InitialCreate.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Arena.Challenges.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema("edoxa");

            migrationBuilder.CreateTable(
                "Challenge",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    Game = table.Column<int>(),
                    Name = table.Column<string>(),
                    CreatedAt = table.Column<DateTime>(),
                    LastSync = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenge", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                "Participant",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    Timestamp = table.Column<DateTime>(),
                    LastSync = table.Column<DateTime>(nullable: true),
                    ExternalAccount = table.Column<string>(),
                    UserId = table.Column<Guid>(),
                    ChallengeId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.Id);

                    table.ForeignKey(
                        "FK_Participant_Challenge_ChallengeId",
                        x => x.ChallengeId,
                        principalSchema: "edoxa",
                        principalTable: "Challenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "Payout",
                schema: "edoxa",
                columns: table => new
                {
                    ChallengeId = table.Column<Guid>(),
                    Id = table.Column<Guid>(),
                    PrizeCurrency = table.Column<int>(),
                    PrizeAmount = table.Column<decimal>(),
                    Size = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_Payout",
                        x => new
                        {
                            x.ChallengeId,
                            x.Id
                        }
                    );

                    table.ForeignKey(
                        "FK_Payout_Challenge_ChallengeId",
                        x => x.ChallengeId,
                        principalSchema: "edoxa",
                        principalTable: "Challenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "Scoring",
                schema: "edoxa",
                columns: table => new
                {
                    ChallengeId = table.Column<Guid>(),
                    Id = table.Column<Guid>(),
                    Name = table.Column<string>(),
                    Weighting = table.Column<float>()
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_Scoring",
                        x => new
                        {
                            x.ChallengeId,
                            x.Id
                        }
                    );

                    table.ForeignKey(
                        "FK_Scoring_Challenge_ChallengeId",
                        x => x.ChallengeId,
                        principalSchema: "edoxa",
                        principalTable: "Challenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "Setup",
                schema: "edoxa",
                columns: table => new
                {
                    ChallengeId = table.Column<Guid>(),
                    BestOf = table.Column<int>(),
                    Entries = table.Column<int>(),
                    EntryFeeCurrency = table.Column<int>(),
                    EntryFeeAmount = table.Column<decimal>(),
                    PrizePoolCurrency = table.Column<int>(),
                    PrizePoolAmount = table.Column<decimal>(),
                    PayoutEntries = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setup", x => x.ChallengeId);

                    table.ForeignKey(
                        "FK_Setup_Challenge_ChallengeId",
                        x => x.ChallengeId,
                        principalSchema: "edoxa",
                        principalTable: "Challenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "TestMode",
                schema: "edoxa",
                columns: table => new
                {
                    ChallengeId = table.Column<Guid>(),
                    State = table.Column<int>(),
                    AverageBestOf = table.Column<int>(),
                    ParticipantQuantity = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestMode", x => x.ChallengeId);

                    table.ForeignKey(
                        "FK_TestMode_Challenge_ChallengeId",
                        x => x.ChallengeId,
                        principalSchema: "edoxa",
                        principalTable: "Challenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "Timeline",
                schema: "edoxa",
                columns: table => new
                {
                    ChallengeId = table.Column<Guid>(),
                    Duration = table.Column<long>(),
                    StartedAt = table.Column<DateTime>(nullable: true),
                    ClosedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timeline", x => x.ChallengeId);

                    table.ForeignKey(
                        "FK_Timeline_Challenge_ChallengeId",
                        x => x.ChallengeId,
                        principalSchema: "edoxa",
                        principalTable: "Challenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "Match",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    Timestamp = table.Column<DateTime>(),
                    MatchReference = table.Column<string>(),
                    ParticipantId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);

                    table.ForeignKey(
                        "FK_Match_Participant_ParticipantId",
                        x => x.ParticipantId,
                        principalSchema: "edoxa",
                        principalTable: "Participant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "Stat",
                schema: "edoxa",
                columns: table => new
                {
                    MatchId = table.Column<Guid>(),
                    Id = table.Column<Guid>(),
                    Name = table.Column<string>(),
                    Value = table.Column<double>(),
                    Weighting = table.Column<float>()
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_Stat",
                        x => new
                        {
                            x.MatchId,
                            x.Id
                        }
                    );

                    table.ForeignKey(
                        "FK_Stat_Match_MatchId",
                        x => x.MatchId,
                        principalSchema: "edoxa",
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex("IX_Match_ParticipantId", schema: "edoxa", table: "Match", column: "ParticipantId");

            migrationBuilder.CreateIndex("IX_Participant_ChallengeId", schema: "edoxa", table: "Participant", column: "ChallengeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Payout", "edoxa");

            migrationBuilder.DropTable("Scoring", "edoxa");

            migrationBuilder.DropTable("Setup", "edoxa");

            migrationBuilder.DropTable("Stat", "edoxa");

            migrationBuilder.DropTable("TestMode", "edoxa");

            migrationBuilder.DropTable("Timeline", "edoxa");

            migrationBuilder.DropTable("Match", "edoxa");

            migrationBuilder.DropTable("Participant", "edoxa");

            migrationBuilder.DropTable("Challenge", "edoxa");
        }
    }
}
