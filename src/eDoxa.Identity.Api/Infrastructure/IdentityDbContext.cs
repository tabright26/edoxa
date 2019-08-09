// Filename: IdentityDbContext.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Identity.Api.Infrastructure.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eDoxa.Identity.Api.Infrastructure
{
    public sealed class IdentityDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        public DbSet<UserGame> UserGames => this.Set<UserGame>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(
                builder =>
                {
                    builder.Property(user => user.Email).IsRequired();
                    builder.Property(user => user.NormalizedEmail).IsRequired();

                    builder.OwnsOne(
                        user => user.Doxatag,
                        userDoxatag =>
                        {
                            userDoxatag.Property(doxatag => doxatag!.Name);
                            userDoxatag.Property(doxatag => doxatag!.UniqueTag);
                            userDoxatag.ToTable("Doxatag");
                        }
                    );

                    builder.OwnsOne(
                        user => user.Profile,
                        userProfile =>
                        {
                            userProfile.Property(user => user!.FirstName).IsRequired(false);
                            userProfile.Property(user => user!.LastName).IsRequired(false);
                            userProfile.Property(user => user!.Gender).HasConversion(gender => gender != null ? (int?) gender.Value : null, gender => gender.HasValue ? Gender.FromValue(gender.Value) : null).IsRequired(false);
                            userProfile.Property(user => user!.BirthDate).IsRequired(false);
                            userProfile.ToTable("Profile");
                        }
                    );

                    builder.OwnsOne(
                        user => user.Address,
                        userAddress =>
                        {
                            userAddress.Property(address => address!.Street).IsRequired();
                            userAddress.Property(address => address!.City).IsRequired();
                            userAddress.Property(address => address!.PostalCode).IsRequired();
                            userAddress.Property(address => address!.Country).IsRequired();
                            userAddress.ToTable("Address");
                        }
                    );

                    builder.HasMany<UserGame>().WithOne().HasForeignKey(userGame => userGame.UserId).IsRequired();
                    builder.ToTable("User");
                }
            );

            modelBuilder.Entity<UserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<UserToken>().ToTable("UserToken");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaim");

            modelBuilder.Entity<UserGame>(
                builder =>
                {
                    builder.HasKey(
                        userGame => new
                        {
                            userGame.Value,
                            userGame.PlayerId
                        }
                    );

                    builder.ToTable("UserGame");
                }
            );
        }
    }
}
