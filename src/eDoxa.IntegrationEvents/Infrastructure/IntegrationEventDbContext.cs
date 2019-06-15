// Filename: IntegrationEventDbContext.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.IntegrationEvents.Infrastructure
{
    public sealed class IntegrationEventDbContext : DbContext
    {
        public const string DefaultSchema = "dbo";

        public IntegrationEventDbContext(DbContextOptions<IntegrationEventDbContext> options) : base(options)
        {
        }

        public DbSet<IntegrationEventLogEntry> Logs => this.Set<IntegrationEventLogEntry>();

        protected override void OnModelCreating([NotNull] ModelBuilder builder)
        {
            builder.Entity<IntegrationEventLogEntry>(
                entity =>
                {
                    entity.ToTable("Log", DefaultSchema);

                    entity.Property(log => log.Id).IsRequired();

                    entity.Property(log => log.Created).IsRequired();

                    entity.Property(log => log.TypeFullName).IsRequired();

                    entity.Property(log => log.JsonObject).IsRequired();

                    entity.Property(log => log.State).IsRequired();

                    entity.Property(log => log.PublishAttempted).IsRequired();

                    entity.HasKey(log => log.Id);
                }
            );
        }
    }
}
