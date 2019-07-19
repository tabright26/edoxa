// Filename: 20190718224150_InitialCreate.cs
// Date Created: 2019-07-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Identity.Api.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Role",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                "User",
                table => new
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
                    AccessFailedCount = table.Column<int>(),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                "RoleClaim",
                table => new
                {
                    Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);

                    table.ForeignKey(
                        "FK_RoleClaim_Role_RoleId",
                        x => x.RoleId,
                        "Role",
                        "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "UserClaim",
                table => new
                {
                    Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);

                    table.ForeignKey(
                        "FK_UserClaim_User_UserId",
                        x => x.UserId,
                        "User",
                        "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "UserGameProvider",
                table => new
                {
                    GameProvider = table.Column<int>(),
                    ProviderKey = table.Column<string>(),
                    UserId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_UserGameProvider",
                        x => new
                        {
                            x.GameProvider,
                            x.ProviderKey
                        }
                    );

                    table.ForeignKey(
                        "FK_UserGameProvider_User_UserId",
                        x => x.UserId,
                        "User",
                        "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "UserLogin",
                table => new
                {
                    LoginProvider = table.Column<string>(),
                    ProviderKey = table.Column<string>(),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_UserLogin",
                        x => new
                        {
                            x.LoginProvider,
                            x.ProviderKey
                        }
                    );

                    table.ForeignKey(
                        "FK_UserLogin_User_UserId",
                        x => x.UserId,
                        "User",
                        "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "UserRole",
                table => new
                {
                    UserId = table.Column<Guid>(),
                    RoleId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_UserRole",
                        x => new
                        {
                            x.UserId,
                            x.RoleId
                        }
                    );

                    table.ForeignKey(
                        "FK_UserRole_Role_RoleId",
                        x => x.RoleId,
                        "Role",
                        "Id",
                        onDelete: ReferentialAction.Cascade
                    );

                    table.ForeignKey(
                        "FK_UserRole_User_UserId",
                        x => x.UserId,
                        "User",
                        "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                "UserToken",
                table => new
                {
                    UserId = table.Column<Guid>(),
                    LoginProvider = table.Column<string>(),
                    Name = table.Column<string>(),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_UserToken",
                        x => new
                        {
                            x.UserId,
                            x.LoginProvider,
                            x.Name
                        }
                    );

                    table.ForeignKey(
                        "FK_UserToken_User_UserId",
                        x => x.UserId,
                        "User",
                        "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                "RoleNameIndex",
                "Role",
                "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL"
            );

            migrationBuilder.CreateIndex("IX_RoleClaim_RoleId", "RoleClaim", "RoleId");

            migrationBuilder.CreateIndex("EmailIndex", "User", "NormalizedEmail");

            migrationBuilder.CreateIndex(
                "UserNameIndex",
                "User",
                "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL"
            );

            migrationBuilder.CreateIndex("IX_UserClaim_UserId", "UserClaim", "UserId");

            migrationBuilder.CreateIndex("IX_UserGameProvider_UserId", "UserGameProvider", "UserId");

            migrationBuilder.CreateIndex("IX_UserLogin_UserId", "UserLogin", "UserId");

            migrationBuilder.CreateIndex("IX_UserRole_RoleId", "UserRole", "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("RoleClaim");

            migrationBuilder.DropTable("UserClaim");

            migrationBuilder.DropTable("UserGameProvider");

            migrationBuilder.DropTable("UserLogin");

            migrationBuilder.DropTable("UserRole");

            migrationBuilder.DropTable("UserToken");

            migrationBuilder.DropTable("Role");

            migrationBuilder.DropTable("User");
        }
    }
}
