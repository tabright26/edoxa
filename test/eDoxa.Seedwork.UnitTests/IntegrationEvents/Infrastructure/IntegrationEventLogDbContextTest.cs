// Filename: IntegrationEventLogDbContextTest.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Seedwork.IntegrationEvents.Infrastructure;
using eDoxa.Seedwork.UnitTests.IntegrationEvents.Mocks;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.UnitTests.IntegrationEvents.Infrastructure
{
    [TestClass]
    public sealed class IntegrationEventLogDbContextTest
    {
        [TestMethod]
        public async Task IntegrationEventLoggerDbContext_EntityMappingConsistency_ShouldBeValid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ServiceBusDbContext>().UseInMemoryDatabase(
                    $"{nameof(IntegrationEventLogDbContextTest)}.{nameof(this.IntegrationEventLoggerDbContext_EntityMappingConsistency_ShouldBeValid)}"
                )
                .Options;

            var mockIntegrationEvent = new MockIntegrationEvent();
            var mockIntegrationEventLogEntry = new IntegrationEventModel(mockIntegrationEvent);

            // Act
            using (var context = new ServiceBusDbContext(options))
            {
                context.IntegrationEvents.Add(mockIntegrationEventLogEntry);

                await context.SaveChangesAsync();
            }

            // Assert
            using (var context = new ServiceBusDbContext(options))
            {
                var integrationEventLogEntry = await context.IntegrationEvents.SingleAsync();

                var integrationEvent = MockIntegrationEvent.Deserialize(integrationEventLogEntry.Content);

                integrationEventLogEntry.Id.Should().Be(integrationEvent.Id);
                integrationEventLogEntry.Timestamp.Should().BeCloseTo(integrationEvent.Timestamp, 1000);
                integrationEvent.Equals(mockIntegrationEvent).Should().BeTrue();
            }

            // Act
            using (var context = new ServiceBusDbContext(options))
            {
                var integrationEventLogEntry = await context.IntegrationEvents.SingleAsync();

                Assert.AreEqual(0, integrationEventLogEntry.PublishAttempted);
                Assert.AreEqual(IntegrationEventStatus.NotPublished, integrationEventLogEntry.Status);

                integrationEventLogEntry.MarkAsPublished();

                Assert.AreEqual(1, integrationEventLogEntry.PublishAttempted);
                Assert.AreEqual(IntegrationEventStatus.Published, integrationEventLogEntry.Status);

                await context.SaveChangesAsync();
            }

            // Assert
            using (var context = new ServiceBusDbContext(options))
            {
                var integrationEventLogEntry = await context.IntegrationEvents.SingleAsync();

                Assert.AreEqual(1, integrationEventLogEntry.PublishAttempted);
                Assert.AreEqual(IntegrationEventStatus.Published, integrationEventLogEntry.Status);
            }
        }
    }
}
