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

using eDoxa.ServiceBus.Tests.Mocks;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.ServiceBus.Tests.Infrastructure
{
    [TestClass]
    public sealed class IntegrationEventLogDbContextTest
    {
        [TestMethod]
        public async Task IntegrationEventLoggerDbContext_EntityMappingConsistency_ShouldBeValid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<IntegrationEventLogDbContext>().UseInMemoryDatabase(
                    $"{nameof(IntegrationEventLogDbContextTest)}.{nameof(this.IntegrationEventLoggerDbContext_EntityMappingConsistency_ShouldBeValid)}"
                )
                .Options;

            var mockIntegrationEvent = new MockIntegrationEvent();
            var mockIntegrationEventLogEntry = new MockIntegrationEventLogEntry(mockIntegrationEvent);

            // Act
            using (var context = new IntegrationEventLogDbContext(options))
            {
                context.IntegrationEventLogs.Add(mockIntegrationEventLogEntry);

                await context.SaveChangesAsync();
            }

            // Assert
            using (var context = new IntegrationEventLogDbContext(options))
            {
                var integrationEventLogEntry = await context.IntegrationEventLogs.SingleAsync();

                var integrationEvent = MockIntegrationEvent.Deserialize(integrationEventLogEntry.JsonObject);

                integrationEventLogEntry.Id.Should().Be(integrationEvent.Id);
                integrationEventLogEntry.Created.Should().BeCloseTo(integrationEvent.Created, 1000);
                integrationEvent.Equals(mockIntegrationEvent).Should().BeTrue();
            }

            // Act
            using (var context = new IntegrationEventLogDbContext(options))
            {
                var integrationEventLogEntry = await context.IntegrationEventLogs.SingleAsync();

                Assert.AreEqual(0, integrationEventLogEntry.PublishAttempted);
                Assert.AreEqual(IntegrationEventState.NotPublished, integrationEventLogEntry.State);

                integrationEventLogEntry.MarkAsPublished();

                Assert.AreEqual(1, integrationEventLogEntry.PublishAttempted);
                Assert.AreEqual(IntegrationEventState.Published, integrationEventLogEntry.State);

                await context.SaveChangesAsync();
            }

            // Assert
            using (var context = new IntegrationEventLogDbContext(options))
            {
                var integrationEventLogEntry = await context.IntegrationEventLogs.SingleAsync();

                Assert.AreEqual(1, integrationEventLogEntry.PublishAttempted);
                Assert.AreEqual(IntegrationEventState.Published, integrationEventLogEntry.State);
            }
        }
    }
}
