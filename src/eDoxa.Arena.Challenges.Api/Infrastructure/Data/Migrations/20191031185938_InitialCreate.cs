using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Challenge",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Game = table.Column<int>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    BestOf = table.Column<int>(nullable: false),
                    Entries = table.Column<int>(nullable: false),
                    SynchronizedAt = table.Column<DateTime>(nullable: true),
                    Timeline_CreatedAt = table.Column<DateTime>(nullable: false),
                    Timeline_Duration = table.Column<long>(nullable: false),
                    Timeline_StartedAt = table.Column<DateTime>(nullable: true),
                    Timeline_ClosedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegisteredAt = table.Column<DateTime>(nullable: false),
                    PlayerId = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    ChallengeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participant_Challenge_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoringItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChallengeId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Weighting = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoringItem", x => new { x.ChallengeId, x.Id });
                    table.ForeignKey(
                        name: "FK_ScoringItem_Challenge_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SynchronizedAt = table.Column<DateTime>(nullable: false),
                    GameReference = table.Column<string>(nullable: true),
                    ParticipantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Match_Participant_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stat",
                columns: table => new
                {
                    MatchId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<double>(nullable: false),
                    Weighting = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stat", x => new { x.MatchId, x.Id });
                    table.ForeignKey(
                        name: "FK_Stat_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Match_GameReference",
                table: "Match",
                column: "GameReference",
                unique: true,
                filter: "[GameReference] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Match_ParticipantId",
                table: "Match",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_ChallengeId",
                table: "Participant",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_Id_UserId",
                table: "Participant",
                columns: new[] { "Id", "UserId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScoringItem");

            migrationBuilder.DropTable(
                name: "Stat");

            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "Participant");

            migrationBuilder.DropTable(
                name: "Challenge");
        }
    }
}
