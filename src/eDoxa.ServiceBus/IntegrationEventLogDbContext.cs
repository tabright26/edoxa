// Filename: IntegrationEventLogDbContext.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.ServiceBus
{
    public class IntegrationEventLogDbContext : DbContext
    {
        public const string DefaultSchema = "dbo";

        public IntegrationEventLogDbContext(DbContextOptions<IntegrationEventLogDbContext> options) : base(options)
        {
        }

        public DbSet<IntegrationEventLogEntry> IntegrationEventLogs
        {
            get
            {
                return this.Set<IntegrationEventLogEntry>();
            }
        }

        protected override void OnModelCreating([NotNull] ModelBuilder builder)
        {
            builder.Entity<IntegrationEventLogEntry>(
                entity =>
                {
                    entity.ToTable(nameof(IntegrationEventLogs), DefaultSchema);
                    entity.HasKey(integrationEventLogEntry => integrationEventLogEntry.Id);
                    entity.Property(integrationEventLogEntry => integrationEventLogEntry.Id).IsRequired();
                    entity.Property(integrationEventLogEntry => integrationEventLogEntry.Created).IsRequired();
                    entity.Property(integrationEventLogEntry => integrationEventLogEntry.TypeFullName).IsRequired();
                    entity.Property(integrationEventLogEntry => integrationEventLogEntry.JsonObject).IsRequired();
                    entity.Property(integrationEventLogEntry => integrationEventLogEntry.State).IsRequired();
                    entity.Property(integrationEventLogEntry => integrationEventLogEntry.PublishAttempted).IsRequired();
                }
            );
        }
    }
}