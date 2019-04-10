// Filename: 20180824205911_PresistedGrantSchemaNameChange.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Identity.Infrastructure.Migrations.PresistedGrant
{
    internal partial class PresistedGrantSchemaNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema("idsrv");

            migrationBuilder.RenameTable("PersistedGrants", newName: "PersistedGrants", newSchema: "idsrv");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable("PersistedGrants", "idsrv", "PersistedGrants");
        }
    }
}