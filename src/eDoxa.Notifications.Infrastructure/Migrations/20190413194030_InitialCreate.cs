// Filename: 20190413194030_InitialCreate.cs
// Date Created: 2019-04-13
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Notifications.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema("edoxa");

            migrationBuilder.EnsureSchema("dbo");

            migrationBuilder.CreateTable(
                "RequestLogs",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    Time = table.Column<DateTime>(),
                    Type = table.Column<int>(),
                    Method = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    LocalIpAddress = table.Column<string>(nullable: true),
                    RemoteIpAddress = table.Column<string>(nullable: true),
                    Version = table.Column<string>(nullable: true),
                    Origin = table.Column<string>(nullable: true),
                    IdempotencyKey = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestLogs", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                "Users",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                "Notifications",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    Timestamp = table.Column<DateTime>(),
                    Title = table.Column<string>(),
                    Message = table.Column<string>(),
                    IsRead = table.Column<bool>(),
                    RedirectUrl = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);

                    table.ForeignKey(
                        "FK_Notifications_Users_UserId",
                        x => x.UserId,
                        principalSchema: "edoxa",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict
                    );
                }
            );

            migrationBuilder.CreateIndex(
                "IX_RequestLogs_IdempotencyKey",
                schema: "dbo",
                table: "RequestLogs",
                column: "IdempotencyKey",
                unique: true,
                filter: "[IdempotencyKey] IS NOT NULL"
            );

            migrationBuilder.CreateIndex("IX_Notifications_UserId", schema: "edoxa", table: "Notifications", column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("RequestLogs", "dbo");

            migrationBuilder.DropTable("Notifications", "edoxa");

            migrationBuilder.DropTable("Users", "edoxa");
        }
    }
}