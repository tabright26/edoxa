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
    public sealed class IdentityDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        public DbSet<UserGameProvider> Games => this.Set<UserGameProvider>();

        protected override void OnModelCreating([NotNull] ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(
                builder =>
                {
                    builder.Property(user => user.Email).IsRequired();
                    builder.Property(user => user.NormalizedEmail).IsRequired();
                    builder.Property(user => user.FirstName).IsRequired(false);
                    builder.Property(user => user.LastName).IsRequired(false);
                    builder.Property(user => user.BirthDate).IsRequired(false);
                    builder.HasMany<UserGameProvider>().WithOne().HasForeignKey(userGame => userGame.UserId).IsRequired();
                    builder.ToTable("User");
                }
            );

            modelBuilder.Entity<UserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<UserToken>().ToTable("UserToken");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaim");

            modelBuilder.Entity<UserGameProvider>(
                builder =>
                {
                    builder.HasKey(
                        gameProvider => new
                        {
                            gameProvider.Game,
                            gameProvider.PlayerId
                        }
                    );

                    builder.ToTable("UserGameProvider");
                }
            );
        }
    }
}
