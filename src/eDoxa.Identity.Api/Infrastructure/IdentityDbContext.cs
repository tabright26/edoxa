// Filename: IdentityDbContext.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Identity.Api.Models;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eDoxa.Identity.Api.Infrastructure
{
    public sealed class IdentityDbContext : IdentityDbContext<UserModel, RoleModel, Guid, UserClaimModel, UserRoleModel, UserLoginModel, RoleClaimModel,
        UserTokenModel>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        public DbSet<UserGameProviderModel> Games => this.Set<UserGameProviderModel>();

        protected override void OnModelCreating([NotNull] ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>(
                builder =>
                {
                    builder.HasMany<UserGameProviderModel>().WithOne().HasForeignKey(userGame => userGame.UserId).IsRequired();
                    builder.ToTable("User");
                }
            );

            modelBuilder.Entity<UserClaimModel>().ToTable("UserClaim");
            modelBuilder.Entity<UserLoginModel>().ToTable("UserLogin");
            modelBuilder.Entity<UserTokenModel>().ToTable("UserToken");
            modelBuilder.Entity<UserRoleModel>().ToTable("UserRole");
            modelBuilder.Entity<RoleModel>().ToTable("Role");
            modelBuilder.Entity<RoleClaimModel>().ToTable("RoleClaim");

            modelBuilder.Entity<UserGameProviderModel>(
                builder =>
                {
                    builder.HasKey(
                        userGame => new
                        {
                            userGame.GameProvider,
                            userGame.ProviderKey
                        }
                    );

                    builder.ToTable("UserGameProvider");
                }
            );
        }
    }
}
