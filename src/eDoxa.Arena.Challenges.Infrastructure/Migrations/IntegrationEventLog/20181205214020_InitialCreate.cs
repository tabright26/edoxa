// Filename: 20181205214020_InitialCreate.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Arena.Challenges.Infrastructure.Migrations.IntegrationEventLog
{
    internal partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema("dbo");

            migrationBuilder.CreateTable(
                "IntegrationEventLogs",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    Created = table.Column<DateTime>(),
                    TypeFullName = table.Column<string>(),
                    JsonObject = table.Column<string>(),
                    State = table.Column<int>(),
                    PublishAttempted = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationEventLogs", x => x.Id);
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("IntegrationEventLogs", "dbo");
        }
    }
}