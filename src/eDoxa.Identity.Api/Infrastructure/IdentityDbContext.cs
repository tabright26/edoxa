﻿// Filename: IdentityDbContext.cs
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
                        user => user.PersonalInfo,
                        userPersonalInfo =>
                        {
                            userPersonalInfo.Property(personalInfo => personalInfo!.FirstName).IsRequired(false);
                            userPersonalInfo.Property(personalInfo => personalInfo!.LastName).IsRequired(false);

                            userPersonalInfo.Property(personalInfo => personalInfo!.Gender)
                                .HasConversion(
                                    gender => gender != null ? (int?) gender.Value : null,
                                    gender => gender.HasValue ? Gender.FromValue(gender.Value) : null
                                )
                                .IsRequired(false);

                            userPersonalInfo.Property(personalInfo => personalInfo!.BirthDate).IsRequired(false);
                            userPersonalInfo.ToTable("PersonalInfo");
                        }
                    );

                    builder.OwnsOne(
                        user => user.DoxaTag,
                        userDoxaTag =>
                        {
                            userDoxaTag.Property(doxaTag => doxaTag!.Name);
                            userDoxaTag.Property(doxaTag => doxaTag!.Code);
                            userDoxaTag.ToTable("DoxaTag");
                        }
                    );

                    builder.HasMany(user => user.AddressBook).WithOne().HasForeignKey(address => address.UserId).IsRequired();
                    builder.HasMany<UserGame>().WithOne().HasForeignKey(userGame => userGame.UserId).IsRequired();
                    builder.ToTable("User");
                }
            );

            modelBuilder.Entity<UserAddress>(
                builder =>
                {
                    builder.HasKey(address => address.Id);
                    builder.Property(address => address.Id).IsRequired();
                    builder.Property(address => address.Type).HasConversion(type => type.Value, type => UserAddressType.FromValue(type)).IsRequired();
                    builder.Property(address => address.Country).IsRequired();
                    builder.Property(address => address.Line1).IsRequired();
                    builder.Property(address => address.Line2).IsRequired(false);
                    builder.Property(address => address.City).IsRequired();
                    builder.Property(address => address.State).IsRequired(false);
                    builder.Property(address => address.PostalCode).IsRequired();
                    builder.ToTable("UserAddress");
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
