// Filename: IntegrationEventLogEntryTest.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.ServiceBus.Infrastructure;
using eDoxa.Seedwork.UnitTests.IntegrationEvents.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.UnitTests.IntegrationEvents
{
    [TestClass]
    public sealed class IntegrationEventLogEntryTest
    {
        [TestMethod]
        public void IntegrationEventLogEntry_InitializedDefaultInstance_ShouldBeValid()
        {
            // Arrange
            var integrationEvent = new MockIntegrationEvent1();

            // Act
            var integrationEventLogEntry = new IntegrationEventModel(integrationEvent);

            // Assert
            Assert.AreEqual(integrationEvent.Id, integrationEventLogEntry.Id);
            Assert.AreEqual(integrationEvent.Timestamp, integrationEventLogEntry.Timestamp);
            Assert.AreEqual(integrationEvent.GetType().FullName, integrationEventLogEntry.TypeName);
            Assert.AreEqual(JsonConvert.SerializeObject(integrationEvent), integrationEventLogEntry.Content);
            Assert.AreEqual(0, integrationEventLogEntry.PublishAttempted);
            Assert.AreEqual(IntegrationEventStatus.NotPublished, integrationEventLogEntry.Status);
        }

        [TestMethod]
        public void AsPublished_MarkIntegrationEventAsPublished_ShouldBeValid()
        {
            // Arrange
            var integrationEvent = new MockIntegrationEvent1();
            var integrationEventLogEntry = new IntegrationEventModel(integrationEvent);

            // Act
            integrationEventLogEntry.MarkAsPublished();

            // Assert
            Assert.AreEqual(1, integrationEventLogEntry.PublishAttempted);
            Assert.AreEqual(IntegrationEventStatus.Published, integrationEventLogEntry.Status);
        }
    }
}
