// Filename: 20190501014731_CreateIdentitySchema.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.IdentityServer.Migrations
{
    public partial class CreateIdentitySchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema("edoxa");

            migrationBuilder.CreateTable(
                "Roles",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Roles", x => x.Id); });

            migrationBuilder.CreateTable(
                "Users",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(),
                    TwoFactorEnabled = table.Column<bool>(),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(),
                    AccessFailedCount = table.Column<int>()
                },
                constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });

            migrationBuilder.CreateTable(
                "RoleClaims",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);

                    table.ForeignKey(
                        "FK_RoleClaims_Roles_RoleId",
                        x => x.RoleId,
                        principalSchema: "edoxa",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "UserClaims",
                schema: "edoxa",
                columns: table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);

                    table.ForeignKey(
                        "FK_UserClaims_Users_UserId",
                        x => x.UserId,
                        principalSchema: "edoxa",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "UserLogins",
                schema: "edoxa",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(),
                    ProviderKey = table.Column<string>(),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new {x.LoginProvider, x.ProviderKey});

                    table.ForeignKey(
                        "FK_UserLogins_Users_UserId",
                        x => x.UserId,
                        principalSchema: "edoxa",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "UserRoles",
                schema: "edoxa",
                columns: table => new
                {
                    UserId = table.Column<Guid>(),
                    RoleId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new {x.UserId, x.RoleId});

                    table.ForeignKey(
                        "FK_UserRoles_Roles_RoleId",
                        x => x.RoleId,
                        principalSchema: "edoxa",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        "FK_UserRoles_Users_UserId",
                        x => x.UserId,
                        principalSchema: "edoxa",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "UserTokens",
                schema: "edoxa",
                columns: table => new
                {
                    UserId = table.Column<Guid>(),
                    LoginProvider = table.Column<string>(),
                    Name = table.Column<string>(),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new {x.UserId, x.LoginProvider, x.Name});

                    table.ForeignKey(
                        "FK_UserTokens_Users_UserId",
                        x => x.UserId,
                        principalSchema: "edoxa",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_RoleClaims_RoleId",
                schema: "edoxa",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                "RoleNameIndex",
                schema: "edoxa",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_UserClaims_UserId",
                schema: "edoxa",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                "IX_UserLogins_UserId",
                schema: "edoxa",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                "IX_UserRoles_RoleId",
                schema: "edoxa",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                "EmailIndex",
                schema: "edoxa",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                "UserNameIndex",
                schema: "edoxa",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "RoleClaims",
                "edoxa");

            migrationBuilder.DropTable(
                "UserClaims",
                "edoxa");

            migrationBuilder.DropTable(
                "UserLogins",
                "edoxa");

            migrationBuilder.DropTable(
                "UserRoles",
                "edoxa");

            migrationBuilder.DropTable(
                "UserTokens",
                "edoxa");

            migrationBuilder.DropTable(
                "Roles",
                "edoxa");

            migrationBuilder.DropTable(
                "Users",
                "edoxa");
        }
    }
}