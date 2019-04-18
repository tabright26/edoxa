// Filename: UserConfiguration.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Identity.Infrastructure.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure([NotNull] EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(IdentityDbContext.Users));

            builder.Property(user => user.CurrentStatus).IsRequired();

            builder.Property(user => user.PreviousStatus).IsRequired();

            builder.Property(user => user.StatusChanged).IsRequired();

            builder.OwnsOne(
                user => user.PersonalName,
                userPersonalName =>
                {
                    userPersonalName.Property(personalName => personalName.FirstName).HasColumnName(nameof(PersonalName.FirstName)).IsRequired();

                    userPersonalName.Property(personalName => personalName.LastName).HasColumnName(nameof(PersonalName.LastName)).IsRequired();
                }
            );

            builder.OwnsOne(
                user => user.BirthDate,
                userBirthDate =>
                {
                    userBirthDate.Property<DateTime>("Date").HasField("_date").HasColumnName(nameof(BirthDate)).IsRequired();

                    userBirthDate.Ignore(birthDate => birthDate.Year);

                    userBirthDate.Ignore(birthDate => birthDate.Month);

                    userBirthDate.Ignore(birthDate => birthDate.Day);
                }
            );
        }
    }
}