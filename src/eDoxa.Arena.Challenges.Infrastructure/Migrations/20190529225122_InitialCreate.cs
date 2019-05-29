using System;
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
                name: "Challenges",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Game = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Setup_BestOf = table.Column<int>(nullable: false),
                    Setup_Entries = table.Column<int>(nullable: false),
                    Setup_EntryFee_Amount = table.Column<decimal>(nullable: false),
                    Setup_EntryFee_Currency = table.Column<int>(nullable: false),
                    Setup_PayoutRatio = table.Column<float>(nullable: false),
                    Setup_ServiceChargeRatio = table.Column<float>(nullable: false),
                    Timeline_Duration = table.Column<long>(nullable: false),
                    Timeline_StartedAt = table.Column<DateTime>(nullable: true),
                    Timeline_ClosedAt = table.Column<DateTime>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    TestMode = table.Column<bool>(nullable: false),
                    Scoring = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    ExternalAccount = table.Column<string>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ChallengeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participants_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalSchema: "edoxa",
                        principalTable: "Challenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    MatchExternalId = table.Column<string>(nullable: false),
                    ParticipantId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalSchema: "edoxa",
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stats",
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
                        name: "FK_Stats_Matches_MatchId",
                        column: x => x.MatchId,
                        principalSchema: "edoxa",
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_ParticipantId",
                schema: "edoxa",
                table: "Matches",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_ChallengeId",
                schema: "edoxa",
                table: "Participants",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_Stats_MatchId",
                schema: "edoxa",
                table: "Stats",
                column: "MatchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stats",
                schema: "edoxa");

            migrationBuilder.DropTable(
                name: "Matches",
                schema: "edoxa");

            migrationBuilder.DropTable(
                name: "Participants",
                schema: "edoxa");

            migrationBuilder.DropTable(
                name: "Challenges",
                schema: "edoxa");
        }
    }
}
