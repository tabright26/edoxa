using System;

using eDoxa.Identity.Api.IntegrationEvents;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Identity.UnitTests.IntegrationEvents
{
    [TestClass]
    public sealed class RoleDeletedIntegrationEventTest
    {
        [TestMethod]
        public void RoleDeletedIntegrationEvent_WithNewRole_ShouldBeEquivalentToRoleDeletedEvent()
        {
            //Arrange
            var roleDeletedEvent = new RoleDeletedIntegrationEvent("role");

            var serializedEvent = JsonConvert.SerializeObject(roleDeletedEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<RoleDeletedIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(roleDeletedEvent);
        }
    }
}
