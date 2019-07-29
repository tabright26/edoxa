// Filename: ServiceBusDbContext.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Seedwork.ServiceBus.Infrastructure
{
    public sealed class ServiceBusDbContext : DbContext
    {
        public ServiceBusDbContext(DbContextOptions<ServiceBusDbContext> options) : base(options)
        {
        }

        public DbSet<IntegrationEventModel> IntegrationEvents => this.Set<IntegrationEventModel>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IntegrationEventModel>(
                builder =>
                {
                    builder.ToTable("IntegrationEvent", "log");
                    builder.Property(integrationEvent => integrationEvent.Id).IsRequired();
                    builder.Property(integrationEvent => integrationEvent.Timestamp).IsRequired();
                    builder.Property(integrationEvent => integrationEvent.TypeName).IsRequired();
                    builder.Property(integrationEvent => integrationEvent.Content).IsRequired();
                    builder.Property(integrationEvent => integrationEvent.Status).IsRequired();
                    builder.Property(integrationEvent => integrationEvent.PublishAttempted).IsRequired();
                    builder.HasKey(integrationEvent => integrationEvent.Id);
                }
            );
        }
    }
}
