using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Identity.Api.Infrastructure.Data.Migrations
{
    public partial class DoxaTagTimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DoxaTag",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Timestamp",
                table: "DoxaTag",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "DoxaTag");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DoxaTag",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
