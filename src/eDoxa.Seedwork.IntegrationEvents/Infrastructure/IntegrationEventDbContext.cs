// Filename: IntegrationEventDbContext.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Seedwork.IntegrationEvents.Infrastructure
{
    public sealed class IntegrationEventDbContext : DbContext
    {
        public const string DefaultSchema = "dbo";

        public IntegrationEventDbContext(DbContextOptions<IntegrationEventDbContext> options) : base(options)
        {
        }

        public DbSet<IntegrationEventLogEntry> Logs => this.Set<IntegrationEventLogEntry>();

        protected override void OnModelCreating(ModelBuilder builder)
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
