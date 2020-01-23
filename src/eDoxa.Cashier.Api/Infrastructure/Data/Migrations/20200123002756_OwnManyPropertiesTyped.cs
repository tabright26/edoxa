using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Migrations
{
    public partial class OwnManyPropertiesTyped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bucket_Challenge_ChallengeId",
                table: "Bucket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Challenge",
                table: "Challenge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bucket",
                table: "Bucket");

            migrationBuilder.RenameTable(
                name: "Challenge",
                newName: "ChallengePayout");

            migrationBuilder.RenameTable(
                name: "Bucket",
                newName: "ChallengePayoutBucket");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "TransactionMetadata",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "TransactionMetadata",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PromotionalCode",
                table: "Promotion",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChallengePayout",
                table: "ChallengePayout",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChallengePayoutBucket",
                table: "ChallengePayoutBucket",
                columns: new[] { "ChallengeId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_ChallengePayoutBucket_ChallengePayout_ChallengeId",
                table: "ChallengePayoutBucket",
                column: "ChallengeId",
                principalTable: "ChallengePayout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChallengePayoutBucket_ChallengePayout_ChallengeId",
                table: "ChallengePayoutBucket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChallengePayoutBucket",
                table: "ChallengePayoutBucket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChallengePayout",
                table: "ChallengePayout");

            migrationBuilder.RenameTable(
                name: "ChallengePayoutBucket",
                newName: "Bucket");

            migrationBuilder.RenameTable(
                name: "ChallengePayout",
                newName: "Challenge");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "TransactionMetadata",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "TransactionMetadata",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "PromotionalCode",
                table: "Promotion",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bucket",
                table: "Bucket",
                columns: new[] { "ChallengeId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Challenge",
                table: "Challenge",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bucket_Challenge_ChallengeId",
                table: "Bucket",
                column: "ChallengeId",
                principalTable: "Challenge",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
