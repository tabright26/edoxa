// Filename: 20190428020624_InitialCreate.cs
// Date Created: 2019-04-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Challenges.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                "edoxa");

            migrationBuilder.EnsureSchema(
                "dbo");

            migrationBuilder.CreateTable(
                "Logs",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
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
                constraints: table => { table.PrimaryKey("PK_Logs", x => x.Id); });

            migrationBuilder.CreateTable(
                "Challenges",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Game = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Scoring = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Challenges", x => x.Id); });

            migrationBuilder.CreateTable(
                "Users",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });

            migrationBuilder.CreateTable(
                "ChallengeSetups",
                schema: "edoxa",
                columns: table => new
                {
                    ChallengeId = table.Column<Guid>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Generated = table.Column<bool>(nullable: false),
                    BestOf = table.Column<int>(nullable: false),
                    Entries = table.Column<int>(nullable: false),
                    EntryFee = table.Column<decimal>("decimal(4,2)", nullable: false),
                    PayoutRatio = table.Column<float>(nullable: false),
                    ServiceChargeRatio = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeSetups", x => x.ChallengeId);

                    table.ForeignKey(
                        "FK_ChallengeSetups_Challenges_ChallengeId",
                        x => x.ChallengeId,
                        principalSchema: "edoxa",
                        principalTable: "Challenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "ChallengeTimelines",
                schema: "edoxa",
                columns: table => new
                {
                    ChallengeId = table.Column<Guid>(nullable: false),
                    PublishedAt = table.Column<DateTime>(nullable: true),
                    ClosedAt = table.Column<DateTime>(nullable: true),
                    RegistrationPeriod = table.Column<long>(nullable: true),
                    ExtensionPeriod = table.Column<long>(nullable: true),
                    LiveMode = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeTimelines", x => x.ChallengeId);

                    table.ForeignKey(
                        "FK_ChallengeTimelines_Challenges_ChallengeId",
                        x => x.ChallengeId,
                        principalSchema: "edoxa",
                        principalTable: "Challenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Participants",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    LinkedAccount = table.Column<string>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ChallengeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);

                    table.ForeignKey(
                        "FK_Participants_Challenges_ChallengeId",
                        x => x.ChallengeId,
                        principalSchema: "edoxa",
                        principalTable: "Challenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Matches",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    LinkedMatch = table.Column<string>(nullable: false),
                    ParticipantId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);

                    table.ForeignKey(
                        "FK_Matches_Participants_ParticipantId",
                        x => x.ParticipantId,
                        principalSchema: "edoxa",
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Stats",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MatchId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    Weighting = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.Id);

                    table.ForeignKey(
                        "FK_Stats_Matches_MatchId",
                        x => x.MatchId,
                        principalSchema: "edoxa",
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Logs_IdempotencyKey",
                schema: "dbo",
                table: "Logs",
                column: "IdempotencyKey",
                unique: true,
                filter: "[IdempotencyKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_Matches_ParticipantId",
                schema: "edoxa",
                table: "Matches",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                "IX_Participants_ChallengeId",
                schema: "edoxa",
                table: "Participants",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                "IX_Stats_MatchId",
                schema: "edoxa",
                table: "Stats",
                column: "MatchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Logs",
                "dbo");

            migrationBuilder.DropTable(
                "ChallengeSetups",
                "edoxa");

            migrationBuilder.DropTable(
                "ChallengeTimelines",
                "edoxa");

            migrationBuilder.DropTable(
                "Stats",
                "edoxa");

            migrationBuilder.DropTable(
                "Users",
                "edoxa");

            migrationBuilder.DropTable(
                "Matches",
                "edoxa");

            migrationBuilder.DropTable(
                "Participants",
                "edoxa");

            migrationBuilder.DropTable(
                "Challenges",
                "edoxa");
        }
    }
}