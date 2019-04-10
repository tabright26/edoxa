// Filename: 20181214045223_InitialCreate.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Identity.Infrastructure.Migrations.Configuration
{
    internal partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema("idsrv");

            migrationBuilder.CreateTable(
                "ApiResources",
                schema: "idsrv",
                columns: table => new
                {
                    Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Enabled = table.Column<bool>(),
                    Name = table.Column<string>(maxLength: 200),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Created = table.Column<DateTime>(),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true),
                    NonEditable = table.Column<bool>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResources", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                "Clients",
                schema: "idsrv",
                columns: table => new
                {
                    Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Enabled = table.Column<bool>(),
                    ClientId = table.Column<string>(maxLength: 200),
                    ProtocolType = table.Column<string>(maxLength: 200),
                    RequireClientSecret = table.Column<bool>(),
                    ClientName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    ClientUri = table.Column<string>(maxLength: 2000, nullable: true),
                    LogoUri = table.Column<string>(maxLength: 2000, nullable: true),
                    RequireConsent = table.Column<bool>(),
                    AllowRememberConsent = table.Column<bool>(),
                    AlwaysIncludeUserClaimsInIdToken = table.Column<bool>(),
                    RequirePkce = table.Column<bool>(),
                    AllowPlainTextPkce = table.Column<bool>(),
                    AllowAccessTokensViaBrowser = table.Column<bool>(),
                    FrontChannelLogoutUri = table.Column<string>(maxLength: 2000, nullable: true),
                    FrontChannelLogoutSessionRequired = table.Column<bool>(),
                    BackChannelLogoutUri = table.Column<string>(maxLength: 2000, nullable: true),
                    BackChannelLogoutSessionRequired = table.Column<bool>(),
                    AllowOfflineAccess = table.Column<bool>(),
                    IdentityTokenLifetime = table.Column<int>(),
                    AccessTokenLifetime = table.Column<int>(),
                    AuthorizationCodeLifetime = table.Column<int>(),
                    ConsentLifetime = table.Column<int>(nullable: true),
                    AbsoluteRefreshTokenLifetime = table.Column<int>(),
                    SlidingRefreshTokenLifetime = table.Column<int>(),
                    RefreshTokenUsage = table.Column<int>(),
                    UpdateAccessTokenClaimsOnRefresh = table.Column<bool>(),
                    RefreshTokenExpiration = table.Column<int>(),
                    AccessTokenType = table.Column<int>(),
                    EnableLocalLogin = table.Column<bool>(),
                    IncludeJwtId = table.Column<bool>(),
                    AlwaysSendClientClaims = table.Column<bool>(),
                    ClientClaimsPrefix = table.Column<string>(maxLength: 200, nullable: true),
                    PairWiseSubjectSalt = table.Column<string>(maxLength: 200, nullable: true),
                    Created = table.Column<DateTime>(),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true),
                    UserSsoLifetime = table.Column<int>(nullable: true),
                    UserCodeType = table.Column<string>(maxLength: 100, nullable: true),
                    DeviceCodeLifetime = table.Column<int>(),
                    NonEditable = table.Column<bool>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                "IdentityResources",
                schema: "idsrv",
                columns: table => new
                {
                    Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Enabled = table.Column<bool>(),
                    Name = table.Column<string>(maxLength: 200),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Required = table.Column<bool>(),
                    Emphasize = table.Column<bool>(),
                    ShowInDiscoveryDocument = table.Column<bool>(),
                    Created = table.Column<DateTime>(),
                    Updated = table.Column<DateTime>(nullable: true),
                    NonEditable = table.Column<bool>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityResources", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                "ApiClaims",
                schema: "idsrv",
                columns: table => new
                {
                    Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 200),
                    ApiResourceId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiClaims", x => x.Id);

                    table.ForeignKey(
                        "FK_ApiClaims_ApiResources_ApiResourceId",
                        x => x.ApiResourceId,
                        principalSchema: "idsrv",
                        principalTable: "ApiResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "ApiProperties",
                schema: "idsrv",
                columns: table => new
                {
                    Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(maxLength: 250),
                    Value = table.Column<string>(maxLength: 2000),
                    ApiResourceId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiProperties", x => x.Id);

                    table.ForeignKey(
                        "FK_ApiProperties_ApiResources_ApiResourceId",
                        x => x.ApiResourceId,
                        principalSchema: "idsrv",
                        principalTable: "ApiResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "ApiScopes",
                schema: "idsrv",
                columns: table => new
                {
                    Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 200),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Required = table.Column<bool>(),
                    Emphasize = table.Column<bool>(),
                    ShowInDiscoveryDocument = table.Column<bool>(),
                    ApiResourceId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiScopes", x => x.Id);

                    table.ForeignKey(
                        "FK_ApiScopes_ApiResources_ApiResourceId",
                        x => x.ApiResourceId,
                        principalSchema: "idsrv",
                        principalTable: "ApiResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "ApiSecrets",
                schema: "idsrv",
                columns: table => new
                {
                    Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Value = table.Column<string>(maxLength: 4000),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(maxLength: 250),
                    Created = table.Column<DateTime>(),
                    ApiResourceId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiSecrets", x => x.Id);

                    table.ForeignKey(
                        "FK_ApiSecrets_ApiResources_ApiResourceId",
                        x => x.ApiResourceId,
                        principalSchema: "idsrv",
                        principalTable: "ApiResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "ClientClaims",
                schema: "idsrv",
                columns: table => new
                {
                    Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 250),
                    Value = table.Column<string>(maxLength: 250),
                    ClientId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientClaims", x => x.Id);

                    table.ForeignKey(
                        "FK_ClientClaims_Clients_ClientId",
                        x => x.ClientId,
                        principalSchema: "idsrv",
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "ClientCorsOrigins",
                schema: "idsrv",
                columns: table => new
                {
                    Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Origin = table.Column<string>(maxLength: 150),
                    ClientId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCorsOrigins", x => x.Id);

                    table.ForeignKey(
                        "FK_ClientCorsOrigins_Clients_ClientId",
                        x => x.ClientId,
                        principalSchema: "idsrv",
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "ClientGrantTypes",
                schema: "idsrv",
                columns: table => new
                {
                    Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GrantType = table.Column<string>(maxLength: 250),
                    ClientId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientGrantTypes", x => x.Id);

                    table.ForeignKey(
                        "FK_ClientGrantTypes_Clients_ClientId",
                        x => x.ClientId,
                        principalSchema: "idsrv",
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "ClientIdPRestrictions",
                schema: "idsrv",
                columns: table => new
                {
                    Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Provider = table.Column<string>(maxLength: 200),
                    ClientId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientIdPRestrictions", x => x.Id);

                    table.ForeignKey(
                        "FK_ClientIdPRestrictions_Clients_ClientId",
                        x => x.ClientId,
                        principalSchema: "idsrv",
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "ClientPostLogoutRedirectUris",
                schema: "idsrv",
                columns: table => new
                {
                    Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PostLogoutRedirectUri = table.Column<string>(maxLength: 2000),
                    ClientId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPostLogoutRedirectUris", x => x.Id);

                    table.ForeignKey(
                        "FK_ClientPostLogoutRedirectUris_Clients_ClientId",
                        x => x.ClientId,
                        principalSchema: "idsrv",
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "ClientProperties",
                schema: "idsrv",
                columns: table => new
                {
                    Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(maxLength: 250),
                    Value = table.Column<string>(maxLength: 2000),
                    ClientId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientProperties", x => x.Id);

                    table.ForeignKey(
                        "FK_ClientProperties_Clients_ClientId",
                        x => x.ClientId,
                        principalSchema: "idsrv",
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "ClientRedirectUris",
                schema: "idsrv",
                columns: table => new
                {
                    Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RedirectUri = table.Column<string>(maxLength: 2000),
                    ClientId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRedirectUris", x => x.Id);

                    table.ForeignKey(
                        "FK_ClientRedirectUris_Clients_ClientId",
                        x => x.ClientId,
                        principalSchema: "idsrv",
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "ClientScopes",
                schema: "idsrv",
                columns: table => new
                {
                    Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Scope = table.Column<string>(maxLength: 200),
                    ClientId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientScopes", x => x.Id);

                    table.ForeignKey(
                        "FK_ClientScopes_Clients_ClientId",
                        x => x.ClientId,
                        principalSchema: "idsrv",
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "ClientSecrets",
                schema: "idsrv",
                columns: table => new
                {
                    Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    Value = table.Column<string>(maxLength: 4000),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(maxLength: 250),
                    Created = table.Column<DateTime>(),
                    ClientId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSecrets", x => x.Id);

                    table.ForeignKey(
                        "FK_ClientSecrets_Clients_ClientId",
                        x => x.ClientId,
                        principalSchema: "idsrv",
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "IdentityClaims",
                schema: "idsrv",
                columns: table => new
                {
                    Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 200),
                    IdentityResourceId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityClaims", x => x.Id);

                    table.ForeignKey(
                        "FK_IdentityClaims_IdentityResources_IdentityResourceId",
                        x => x.IdentityResourceId,
                        principalSchema: "idsrv",
                        principalTable: "IdentityResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "IdentityProperties",
                schema: "idsrv",
                columns: table => new
                {
                    Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(maxLength: 250),
                    Value = table.Column<string>(maxLength: 2000),
                    IdentityResourceId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityProperties", x => x.Id);

                    table.ForeignKey(
                        "FK_IdentityProperties_IdentityResources_IdentityResourceId",
                        x => x.IdentityResourceId,
                        principalSchema: "idsrv",
                        principalTable: "IdentityResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "ApiScopeClaims",
                schema: "idsrv",
                columns: table => new
                {
                    Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 200),
                    ApiScopeId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiScopeClaims", x => x.Id);

                    table.ForeignKey(
                        "FK_ApiScopeClaims_ApiScopes_ApiScopeId",
                        x => x.ApiScopeId,
                        principalSchema: "idsrv",
                        principalTable: "ApiScopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex("IX_ApiClaims_ApiResourceId", schema: "idsrv", table: "ApiClaims", column: "ApiResourceId");

            migrationBuilder.CreateIndex("IX_ApiProperties_ApiResourceId", schema: "idsrv", table: "ApiProperties", column: "ApiResourceId");

            migrationBuilder.CreateIndex("IX_ApiResources_Name", schema: "idsrv", table: "ApiResources", column: "Name", unique: true);

            migrationBuilder.CreateIndex("IX_ApiScopeClaims_ApiScopeId", schema: "idsrv", table: "ApiScopeClaims", column: "ApiScopeId");

            migrationBuilder.CreateIndex("IX_ApiScopes_ApiResourceId", schema: "idsrv", table: "ApiScopes", column: "ApiResourceId");

            migrationBuilder.CreateIndex("IX_ApiScopes_Name", schema: "idsrv", table: "ApiScopes", column: "Name", unique: true);

            migrationBuilder.CreateIndex("IX_ApiSecrets_ApiResourceId", schema: "idsrv", table: "ApiSecrets", column: "ApiResourceId");

            migrationBuilder.CreateIndex("IX_ClientClaims_ClientId", schema: "idsrv", table: "ClientClaims", column: "ClientId");

            migrationBuilder.CreateIndex("IX_ClientCorsOrigins_ClientId", schema: "idsrv", table: "ClientCorsOrigins", column: "ClientId");

            migrationBuilder.CreateIndex("IX_ClientGrantTypes_ClientId", schema: "idsrv", table: "ClientGrantTypes", column: "ClientId");

            migrationBuilder.CreateIndex("IX_ClientIdPRestrictions_ClientId", schema: "idsrv", table: "ClientIdPRestrictions", column: "ClientId");

            migrationBuilder.CreateIndex(
                "IX_ClientPostLogoutRedirectUris_ClientId",
                schema: "idsrv",
                table: "ClientPostLogoutRedirectUris",
                column: "ClientId"
            );

            migrationBuilder.CreateIndex("IX_ClientProperties_ClientId", schema: "idsrv", table: "ClientProperties", column: "ClientId");

            migrationBuilder.CreateIndex("IX_ClientRedirectUris_ClientId", schema: "idsrv", table: "ClientRedirectUris", column: "ClientId");

            migrationBuilder.CreateIndex("IX_Clients_ClientId", schema: "idsrv", table: "Clients", column: "ClientId", unique: true);

            migrationBuilder.CreateIndex("IX_ClientScopes_ClientId", schema: "idsrv", table: "ClientScopes", column: "ClientId");

            migrationBuilder.CreateIndex("IX_ClientSecrets_ClientId", schema: "idsrv", table: "ClientSecrets", column: "ClientId");

            migrationBuilder.CreateIndex("IX_IdentityClaims_IdentityResourceId", schema: "idsrv", table: "IdentityClaims", column: "IdentityResourceId");

            migrationBuilder.CreateIndex(
                "IX_IdentityProperties_IdentityResourceId",
                schema: "idsrv",
                table: "IdentityProperties",
                column: "IdentityResourceId"
            );

            migrationBuilder.CreateIndex("IX_IdentityResources_Name", schema: "idsrv", table: "IdentityResources", column: "Name", unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("ApiClaims", "idsrv");

            migrationBuilder.DropTable("ApiProperties", "idsrv");

            migrationBuilder.DropTable("ApiScopeClaims", "idsrv");

            migrationBuilder.DropTable("ApiSecrets", "idsrv");

            migrationBuilder.DropTable("ClientClaims", "idsrv");

            migrationBuilder.DropTable("ClientCorsOrigins", "idsrv");

            migrationBuilder.DropTable("ClientGrantTypes", "idsrv");

            migrationBuilder.DropTable("ClientIdPRestrictions", "idsrv");

            migrationBuilder.DropTable("ClientPostLogoutRedirectUris", "idsrv");

            migrationBuilder.DropTable("ClientProperties", "idsrv");

            migrationBuilder.DropTable("ClientRedirectUris", "idsrv");

            migrationBuilder.DropTable("ClientScopes", "idsrv");

            migrationBuilder.DropTable("ClientSecrets", "idsrv");

            migrationBuilder.DropTable("IdentityClaims", "idsrv");

            migrationBuilder.DropTable("IdentityProperties", "idsrv");

            migrationBuilder.DropTable("ApiScopes", "idsrv");

            migrationBuilder.DropTable("Clients", "idsrv");

            migrationBuilder.DropTable("IdentityResources", "idsrv");

            migrationBuilder.DropTable("ApiResources", "idsrv");
        }
    }
}