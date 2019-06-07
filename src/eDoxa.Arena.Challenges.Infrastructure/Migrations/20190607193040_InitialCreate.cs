﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Arena.Challenges.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "edoxa");

            migrationBuilder.CreateTable(
                name: "Challenge",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Game = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastSync = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participant",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    LastSync = table.Column<DateTime>(nullable: true),
                    ExternalAccount = table.Column<string>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ChallengeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participant_Challenge_ChallengeId",
                        column: x => x.ChallengeId,
                        principalSchema: "edoxa",
                        principalTable: "Challenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payout",
                schema: "edoxa",
                columns: table => new
                {
                    ChallengeId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    PrizeCurrency = table.Column<int>(nullable: false),
                    PrizeAmount = table.Column<decimal>(nullable: false),
                    Size = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payout", x => new { x.ChallengeId, x.Id });
                    table.ForeignKey(
                        name: "FK_Payout_Challenge_ChallengeId",
                        column: x => x.ChallengeId,
                        principalSchema: "edoxa",
                        principalTable: "Challenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scoring",
                schema: "edoxa",
                columns: table => new
                {
                    ChallengeId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Weighting = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scoring", x => new { x.ChallengeId, x.Id });
                    table.ForeignKey(
                        name: "FK_Scoring_Challenge_ChallengeId",
                        column: x => x.ChallengeId,
                        principalSchema: "edoxa",
                        principalTable: "Challenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Setup",
                schema: "edoxa",
                columns: table => new
                {
                    ChallengeId = table.Column<Guid>(nullable: false),
                    BestOf = table.Column<int>(nullable: false),
                    Entries = table.Column<int>(nullable: false),
                    EntryFeeCurrency = table.Column<int>(nullable: false),
                    EntryFeeAmount = table.Column<decimal>(nullable: false),
                    PrizePoolCurrency = table.Column<int>(nullable: false),
                    PrizePoolAmount = table.Column<decimal>(nullable: false),
                    PayoutEntries = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setup", x => x.ChallengeId);
                    table.ForeignKey(
                        name: "FK_Setup_Challenge_ChallengeId",
                        column: x => x.ChallengeId,
                        principalSchema: "edoxa",
                        principalTable: "Challenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestMode",
                schema: "edoxa",
                columns: table => new
                {
                    ChallengeId = table.Column<Guid>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    AverageBestOf = table.Column<int>(nullable: false),
                    ParticipantQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestMode", x => x.ChallengeId);
                    table.ForeignKey(
                        name: "FK_TestMode_Challenge_ChallengeId",
                        column: x => x.ChallengeId,
                        principalSchema: "edoxa",
                        principalTable: "Challenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Timeline",
                schema: "edoxa",
                columns: table => new
                {
                    ChallengeId = table.Column<Guid>(nullable: false),
                    Duration = table.Column<long>(nullable: false),
                    StartedAt = table.Column<DateTime>(nullable: true),
                    ClosedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timeline", x => x.ChallengeId);
                    table.ForeignKey(
                        name: "FK_Timeline_Challenge_ChallengeId",
                        column: x => x.ChallengeId,
                        principalSchema: "edoxa",
                        principalTable: "Challenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    MatchReference = table.Column<string>(nullable: false),
                    ParticipantId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Match_Participant_ParticipantId",
                        column: x => x.ParticipantId,
                        principalSchema: "edoxa",
                        principalTable: "Participant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stat",
                schema: "edoxa",
                columns: table => new
                {
                    MatchId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    Weighting = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stat", x => new { x.MatchId, x.Id });
                    table.ForeignKey(
                        name: "FK_Stat_Match_MatchId",
                        column: x => x.MatchId,
                        principalSchema: "edoxa",
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Match_ParticipantId",
                schema: "edoxa",
                table: "Match",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_ChallengeId",
                schema: "edoxa",
                table: "Participant",
                column: "ChallengeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payout",
                schema: "edoxa");

            migrationBuilder.DropTable(
                name: "Scoring",
                schema: "edoxa");

            migrationBuilder.DropTable(
                name: "Setup",
                schema: "edoxa");

            migrationBuilder.DropTable(
                name: "Stat",
                schema: "edoxa");

            migrationBuilder.DropTable(
                name: "TestMode",
                schema: "edoxa");

            migrationBuilder.DropTable(
                name: "Timeline",
                schema: "edoxa");

            migrationBuilder.DropTable(
                name: "Match",
                schema: "edoxa");

            migrationBuilder.DropTable(
                name: "Participant",
                schema: "edoxa");

            migrationBuilder.DropTable(
                name: "Challenge",
                schema: "edoxa");
        }
    }
}
