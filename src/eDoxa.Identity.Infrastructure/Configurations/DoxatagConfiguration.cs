// Filename: DoxatagConfiguration.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;
using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Identity.Infrastructure.Configurations
{
    internal sealed class DoxatagConfiguration : IEntityTypeConfiguration<Doxatag>
    {
        public void Configure(EntityTypeBuilder<Doxatag> builder)
        {
            builder.Property(doxatag => doxatag.Id).HasConversion(doxatagId => doxatagId.ToGuid(), value => DoxatagId.FromGuid(value)).IsRequired().ValueGeneratedNever();

            builder.Property(doxatag => doxatag.Name).IsRequired();

            builder.Property(doxatag => doxatag.Code).IsRequired();

            builder.Property(doxatag => doxatag.Timestamp).HasConversion(timestamp => timestamp.Ticks, ticks => new DateTime(ticks)).IsRequired();

            builder.Property(doxatag => doxatag.UserId).IsRequired();

            builder.HasKey(doxatag => doxatag.Id);

            builder.ToTable("Doxatag");
        }
    }
}
