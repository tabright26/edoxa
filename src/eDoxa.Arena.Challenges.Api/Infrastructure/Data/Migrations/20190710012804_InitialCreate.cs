// Filename: 20190710012804_InitialCreate.cs
// Date Created: 2019-07-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Challenge",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Name = table.Column<string>(nullable: true),
                    Game = table.Column<int>(),
                    State = table.Column<int>(),
                    SynchronizedAt = table.Column<DateTime>(nullable: true),
                    Setup_BestOf = table.Column<int>(),
                    Setup_Entries = table.Column<int>(),
                    Setup_PayoutEntries = table.Column<int>(),
                    Setup_EntryFeeCurrency = table.Column<int>(),
                    Setup_EntryFeeAmount = table.Column<decimal>("decimal(11, 2)"),
                    Timeline_CreatedAt = table.Column<DateTime>(),
                    Timeline_Duration = table.Column<long>(),
                    Timeline_StartedAt = table.Column<DateTime>(nullable: true),
                    Timeline_ClosedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenge", x => x.Id);
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

            migrationBuilder.CreateTable(
                "Participant",
                table => new
                {
                    Id = table.Column<Guid>(),
                    RegisteredAt = table.Column<DateTime>(),
                    GameAccountId = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(),
                    ChallengeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.Id);

                    table.ForeignKey(
                        "FK_Participant_Challenge_ChallengeId",
                        x => x.ChallengeId,
                        "Challenge",
                        "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "ScoringItem",
                table => new
                {
                    Id = table.Column<Guid>(),
                    ChallengeId = table.Column<Guid>(),
                    Name = table.Column<string>(nullable: true),
                    Weighting = table.Column<float>()
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_ScoringItem",
                        x => new
                        {
                            x.ChallengeId,
                            x.Id
                        }
                    );

                    table.ForeignKey(
                        "FK_ScoringItem_Challenge_ChallengeId",
                        x => x.ChallengeId,
                        "Challenge",
                        "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "Match",
                table => new
                {
                    Id = table.Column<Guid>(),
                    SynchronizedAt = table.Column<DateTime>(),
                    GameReference = table.Column<string>(nullable: true),
                    TotalScore = table.Column<decimal>("decimal(38, 20)"),
                    ParticipantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);

                    table.ForeignKey(
                        "FK_Match_Participant_ParticipantId",
                        x => x.ParticipantId,
                        "Participant",
                        "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "Stat",
                table => new
                {
                    MatchId = table.Column<Guid>(),
                    Id = table.Column<Guid>(),
                    Name = table.Column<string>(nullable: true),
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
                        "Match",
                        "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                "IX_Match_GameReference",
                "Match",
                "GameReference",
                unique: true,
                filter: "[GameReference] IS NOT NULL"
            );

            migrationBuilder.CreateIndex("IX_Match_ParticipantId", "Match", "ParticipantId");

            migrationBuilder.CreateIndex("IX_Participant_ChallengeId", "Participant", "ChallengeId");

            migrationBuilder.CreateIndex("IX_Participant_Id_UserId", "Participant", new[] {"Id", "UserId"}, unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Bucket");

            migrationBuilder.DropTable("ScoringItem");

            migrationBuilder.DropTable("Stat");

            migrationBuilder.DropTable("Match");

            migrationBuilder.DropTable("Participant");

            migrationBuilder.DropTable("Challenge");
        }
    }
}
