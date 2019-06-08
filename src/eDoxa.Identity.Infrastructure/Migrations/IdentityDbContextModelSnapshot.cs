// Filename: IdentityDbContextModelSnapshot.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eDoxa.Identity.Infrastructure.Migrations
{
    [DbContext(typeof(IdentityDbContext))]
    internal class IdentityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasDefaultSchema("edoxa")
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity(
                "eDoxa.Identity.Domain.AggregateModels.RoleAggregate.Role",
                b =>
                {
                    b.Property<Guid>("Id").ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp").IsConcurrencyToken();

                    b.Property<string>("Name").HasMaxLength(256);

                    b.Property<string>("NormalizedName").HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName").IsUnique().HasName("RoleNameIndex").HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles");
                }
            );

            modelBuilder.Entity(
                "eDoxa.Identity.Domain.AggregateModels.RoleAggregate.RoleClaim",
                b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                }
            );

            modelBuilder.Entity(
                "eDoxa.Identity.Domain.AggregateModels.UserAggregate.User",
                b =>
                {
                    b.Property<Guid>("Id").ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp").IsConcurrencyToken();

                    b.Property<string>("Email").HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail").HasMaxLength(256);

                    b.Property<string>("NormalizedUserName").HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName").HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail").HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName").IsUnique().HasName("UserNameIndex").HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users");
                }
            );

            modelBuilder.Entity(
                "eDoxa.Identity.Domain.AggregateModels.UserAggregate.UserClaim",
                b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");
                }
            );

            modelBuilder.Entity(
                "eDoxa.Identity.Domain.AggregateModels.UserAggregate.UserLogin",
                b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                }
            );

            modelBuilder.Entity(
                "eDoxa.Identity.Domain.AggregateModels.UserAggregate.UserRole",
                b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                }
            );

            modelBuilder.Entity(
                "eDoxa.Identity.Domain.AggregateModels.UserAggregate.UserToken",
                b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                }
            );

            modelBuilder.Entity(
                "eDoxa.Identity.Domain.AggregateModels.RoleAggregate.RoleClaim",
                b =>
                {
                    b.HasOne("eDoxa.Identity.Domain.AggregateModels.RoleAggregate.Role").WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.Cascade);
                }
            );

            modelBuilder.Entity(
                "eDoxa.Identity.Domain.AggregateModels.UserAggregate.UserClaim",
                b =>
                {
                    b.HasOne("eDoxa.Identity.Domain.AggregateModels.UserAggregate.User").WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade);
                }
            );

            modelBuilder.Entity(
                "eDoxa.Identity.Domain.AggregateModels.UserAggregate.UserLogin",
                b =>
                {
                    b.HasOne("eDoxa.Identity.Domain.AggregateModels.UserAggregate.User").WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade);
                }
            );

            modelBuilder.Entity(
                "eDoxa.Identity.Domain.AggregateModels.UserAggregate.UserRole",
                b =>
                {
                    b.HasOne("eDoxa.Identity.Domain.AggregateModels.RoleAggregate.Role").WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eDoxa.Identity.Domain.AggregateModels.UserAggregate.User").WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade);
                }
            );

            modelBuilder.Entity(
                "eDoxa.Identity.Domain.AggregateModels.UserAggregate.UserToken",
                b =>
                {
                    b.HasOne("eDoxa.Identity.Domain.AggregateModels.UserAggregate.User").WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade);
                }
            );
#pragma warning restore 612, 618
        }
    }
}
