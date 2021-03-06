﻿// Filename: ParticipantModelConfiguration.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Challenges.Infrastructure.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Challenges.Infrastructure.Configurations
{
    internal sealed class ParticipantModelConfiguration : IEntityTypeConfiguration<ParticipantModel>
    {
        public void Configure(EntityTypeBuilder<ParticipantModel> builder)
        {
            builder.ToTable("Participant");

            builder.Ignore(participant => participant.DomainEvents);

            builder.Property(participant => participant.Id).ValueGeneratedNever();

            builder.HasMany(participant => participant.Matches).WithOne(match => match.Participant).OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(participant => participant.Id);
        }
    }
}
