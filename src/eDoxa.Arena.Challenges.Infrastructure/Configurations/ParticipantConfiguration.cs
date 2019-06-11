// Filename: ParticipantConfiguration.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
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

            builder.EntityId(participant => participant.Id).HasColumnName("Id").IsRequired();

            builder.Property(participant => participant.Timestamp).HasColumnName("Timestamp").IsRequired();

            builder.Property(participant => participant.LastSync).HasColumnName("LastSync").IsRequired(false);

            builder.EntityId(participant => participant.UserId).HasColumnName("UserId").IsRequired();

            builder.Property(participant => participant.ExternalAccount)
                .HasConversion(externalAccount => externalAccount.ToString(), externalAccount => new ExternalAccount(externalAccount))
                .HasColumnName("ExternalAccount")
                .IsRequired();

            builder.Property(participant => participant.MatchBestOf)
                .HasConversion(bestOf => bestOf.Value, value => new BestOf(value))
                .HasColumnName("MatchBestOf")
                .IsRequired();

            builder.Property<ChallengeId>(nameof(ChallengeId))
                .HasConversion(challengeId => challengeId.ToGuid(), value => ChallengeId.FromGuid(value))
                .HasColumnName(nameof(ChallengeId))
                .IsRequired();

            builder.Ignore(participant => participant.AverageScore);

            builder.HasKey(participant => participant.Id);

            builder.HasIndex(
                    participant => new
                    {
                        ParticipantId = participant.Id,
                        participant.UserId
                    }
                )
                .IsUnique();

            builder.HasMany(participant => participant.Matches).WithOne().HasForeignKey(nameof(ParticipantId)).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Metadata.FindNavigation(nameof(Participant.Matches)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
