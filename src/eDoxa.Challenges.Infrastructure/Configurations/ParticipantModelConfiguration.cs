// Filename: ParticipantModelConfiguration.cs
// Date Created: 2019-06-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Infrastructure.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Challenges.Infrastructure.Configurations
{
    internal sealed class ParticipantModelConfiguration : IEntityTypeConfiguration<ParticipantModel>
    {
        public void Configure( EntityTypeBuilder<ParticipantModel> builder)
        {
            builder.ToTable("Participant");

            builder.Property(participant => participant.Id);

            builder.HasMany(participant => participant.Matches).WithOne(match => match.Participant).OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(participant => participant.Id);
        }
    }
}
