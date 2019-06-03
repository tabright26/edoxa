﻿// Filename: ParticipantConfiguration.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Domain.ValueObjects;
using eDoxa.Seedwork.Infrastructure.Extensions;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Arena.Challenges.Infrastructure.Configurations
{
    internal sealed class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure([NotNull] EntityTypeBuilder<Participant> builder)
        {
            builder.ToTable("Participant");

            builder.EntityId(participant => participant.Id).IsRequired();

            builder.Property(participant => participant.Timestamp).IsRequired();

            builder.Property<ChallengeId>(nameof(ChallengeId))
                .HasConversion(challengeId => challengeId.ToGuid(), value => ChallengeId.FromGuid(value))
                .IsRequired();

            builder.EntityId(participant => participant.UserId).IsRequired();

            builder.Property(participant => participant.ExternalAccount).HasConversion(externalAccount => externalAccount.ToString(), externalAccount => new ExternalAccount(externalAccount)).IsRequired();

            builder.Ignore(participant => participant.AverageScore);

            builder.HasMany(participant => participant.Matches)
                .WithOne(match => match.Participant)
                .HasForeignKey(nameof(ParticipantId))
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Metadata.FindNavigation(nameof(Participant.Matches)).SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasKey(participant => participant.Id);
        }
    }
}
