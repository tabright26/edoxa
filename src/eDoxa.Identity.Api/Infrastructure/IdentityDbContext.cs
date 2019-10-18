// Filename: IdentityDbContext.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Miscs;

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
                    builder.Property(user => user.Country).HasConversion(country => country.Name, name => Country.FromName(name)).IsRequired();

                    builder.OwnsOne(
                        user => user.Informations,
                        userInformations =>
                        {
                            userInformations.Property(informations => informations!.FirstName).IsRequired();
                            userInformations.Property(informations => informations!.LastName).IsRequired();
                            userInformations.Property(informations => informations!.Gender)
                                .HasConversion(gender => gender.Value, value => Gender.FromValue(value))
                                .IsRequired();
                            userInformations.OwnsOne(informations => informations!.Dob).Property(dob => dob.Year).HasColumnName("Dob_Year").IsRequired();
                            userInformations.OwnsOne(informations => informations!.Dob).Property(dob => dob.Month).HasColumnName("Dob_Month").IsRequired();
                            userInformations.OwnsOne(informations => informations!.Dob).Property(dob => dob.Day).HasColumnName("Dob_Day").IsRequired();
                            userInformations.ToTable("UserInformations");
                        });

                    builder.HasMany(user => user.DoxatagHistory).WithOne().HasForeignKey(doxatag => doxatag.UserId).IsRequired();
                    builder.HasMany(user => user.AddressBook).WithOne().HasForeignKey(address => address.UserId).IsRequired();
                    builder.HasMany<UserGame>().WithOne().HasForeignKey(userGame => userGame.UserId).IsRequired();
                    builder.ToTable("User");
                });

            modelBuilder.Entity<UserDoxatag>(
                builder =>
                {
                    builder.HasKey(doxatag => doxatag.Id);
                    builder.Property(doxatag => doxatag.Id).IsRequired();
                    builder.Property(doxatag => doxatag.UserId).IsRequired();
                    builder.Property(doxatag => doxatag.Name).IsRequired();
                    builder.Property(doxatag => doxatag.Code).IsRequired();
                    builder.Property(doxatag => doxatag.Timestamp).HasConversion(dateTime => dateTime.Ticks, ticks => new DateTime(ticks)).IsRequired();
                    builder.ToTable("UserDoxatag");
                });

            modelBuilder.Entity<UserAddress>(
                builder =>
                {
                    builder.HasKey(address => address.Id);
                    builder.Property(address => address.Id).IsRequired();
                    builder.Property(address => address.Type).HasConversion(type => type.Value, type => UserAddressType.FromValue(type)).IsRequired();
                    builder.Property(address => address.Country).HasConversion(country => country.Name, name => Country.FromName(name)).IsRequired();
                    builder.Property(address => address.Line1).IsRequired();
                    builder.Property(address => address.Line2).IsRequired(false);
                    builder.Property(address => address.City).IsRequired();
                    builder.Property(address => address.State).IsRequired(false);
                    builder.Property(address => address.PostalCode).IsRequired(false);
                    builder.ToTable("UserAddress");
                });

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
                        });

                    builder.ToTable("UserGame");
                });
        }
    }
}
