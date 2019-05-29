// Filename: IntegrationEventLogEntryTest.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.ServiceBus.Tests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.ServiceBus.Tests
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
            var integrationEventLogEntry = new MockIntegrationEventLogEntry(integrationEvent);

            // Assert
            Assert.AreEqual(integrationEvent.Id, integrationEventLogEntry.Id);
            Assert.AreEqual(integrationEvent.Created, integrationEventLogEntry.Created);
            Assert.AreEqual(integrationEvent.GetType().FullName, integrationEventLogEntry.TypeFullName);
            Assert.AreEqual(JsonConvert.SerializeObject(integrationEvent), integrationEventLogEntry.JsonObject);
            Assert.AreEqual(0, integrationEventLogEntry.PublishAttempted);
            Assert.AreEqual(IntegrationEventState.NotPublished, integrationEventLogEntry.State);
        }

        [TestMethod]
        public void AsPublished_MarkIntegrationEventAsPublished_ShouldBeValid()
        {
            // Arrange
            var integrationEvent = new MockIntegrationEvent1();
            var integrationEventLogEntry = new MockIntegrationEventLogEntry(integrationEvent);

            // Act
            integrationEventLogEntry.MarkAsPublished();

            // Assert
            Assert.AreEqual(1, integrationEventLogEntry.PublishAttempted);
            Assert.AreEqual(IntegrationEventState.Published, integrationEventLogEntry.State);
        }
    }
}
