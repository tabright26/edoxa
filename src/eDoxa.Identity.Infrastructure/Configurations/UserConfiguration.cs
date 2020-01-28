// Filename: UserConfiguration.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Identity.Domain.AggregateModels.AddressAggregate;
using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Identity.Infrastructure.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(user => user.Id).IsRequired().ValueGeneratedNever();

            builder.Property(user => user.Email).IsRequired();

            builder.Property(user => user.NormalizedEmail).IsRequired();

            builder.Property(user => user.Country).HasConversion(country => country.Name, name => Country.FromName(name)).IsRequired();

            builder.OwnsOne(
                user => user!.Dob,
                userDob =>
                {
                    userDob.WithOwner().HasForeignKey("UserId");

                    userDob.Property<Guid>("Id").ValueGeneratedOnAdd();

                    userDob.Property(dob => dob.Year).IsRequired();

                    userDob.Property(dob => dob.Month).IsRequired();

                    userDob.Property(dob => dob.Day).IsRequired();

                    userDob.HasKey("Id");

                    userDob.ToTable("UserDob");
                });

            builder.OwnsOne(
                user => user.Profile,
                userProfile =>
                {
                    userProfile.WithOwner().HasForeignKey("UserId");

                    userProfile.Property<Guid>("Id").ValueGeneratedOnAdd();

                    userProfile.Property(profile => profile!.FirstName).IsRequired();

                    userProfile.Property(profile => profile!.LastName).IsRequired();

                    userProfile.Property(profile => profile!.Gender).HasConversion(gender => gender.Value, value => Gender.FromValue(value)).IsRequired();

                    userProfile.HasKey("Id");

                    userProfile.ToTable("UserProfile");
                });

            builder.HasMany<Doxatag>().WithOne().HasForeignKey(doxatag => doxatag.UserId).IsRequired();

            builder.HasMany<Address>().WithOne().HasForeignKey(address => address.UserId).IsRequired();

            builder.ToTable("User");
        }
    }
}
