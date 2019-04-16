// Filename: UserConfiguration.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Common.ValueObjects;
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
                user => user.Name,
                user =>
                {
                    user.Property(name => name.FirstName).HasColumnName(nameof(Name.FirstName)).HasMaxLength(35).IsRequired();
                    user.Property(name => name.LastName).HasColumnName(nameof(Name.LastName)).HasMaxLength(35).IsRequired();
                }
            );

            builder.OwnsOne(
                user => user.BirthDate,
                user =>
                {
                    user.Property("_date").HasColumnName(nameof(BirthDate)).IsRequired(false);
                    user.Ignore(birthDate => birthDate.Year);
                    user.Ignore(birthDate => birthDate.Month);
                    user.Ignore(birthDate => birthDate.Day);
                }
            );

            builder.OwnsOne(
                user => user.Tag,
                user =>
                {
                    user.Property(userTag => userTag.Name).HasColumnName($"{nameof(UserTag)}_{nameof(UserTag.Name)}").HasMaxLength(256).IsRequired();

                    user.Property(userTag => userTag.ReferenceNumber)
                        .HasColumnName($"{nameof(UserTag)}_{nameof(UserTag.ReferenceNumber)}")
                        .HasMaxLength(4)
                        .IsRequired();

                    user.HasIndex(
                            userTag => new
                            {
                                UserTag_Name = userTag.Name, UserTag_ReferenceNumber = userTag.ReferenceNumber
                            }
                        )
                        .IsUnique();
                }
            );
        }
    }
}