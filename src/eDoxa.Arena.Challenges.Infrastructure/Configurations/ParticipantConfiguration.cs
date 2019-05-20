// Filename: ParticipantConfiguration.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Arena.Challenges.Infrastructure.Configurations
{
    internal sealed class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure([NotNull] EntityTypeBuilder<Participant> builder)
        {
            builder.ToTable(nameof(ChallengesDbContext.Participants));

            builder.Property(participant => participant.Id)
                   .HasConversion(participantId => participantId.ToGuid(), value => ParticipantId.FromGuid(value))
                   .IsRequired()
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(participant => participant.Timestamp).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property<ChallengeId>(nameof(ChallengeId)).HasConversion(challengeId => challengeId.ToGuid(), value => ChallengeId.FromGuid(value)).IsRequired();

            builder.Property(participant => participant.UserId)
                   .HasConversion(userId => userId.ToGuid(), value => UserId.FromGuid(value))
                   .IsRequired()
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(participant => participant.LinkedAccount)
                   .HasConversion(linkedAccount => linkedAccount.ToString(), linkedAccount => new LinkedAccount(linkedAccount))
                   .IsRequired()
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Ignore(participant => participant.AverageScore);

            builder.HasKey(participant => participant.Id);

            builder.HasMany(participant => participant.Matches)
                   .WithOne(match => match.Participant)
                   .HasForeignKey(nameof(ParticipantId))
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Metadata.FindNavigation(nameof(Participant.Matches)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}