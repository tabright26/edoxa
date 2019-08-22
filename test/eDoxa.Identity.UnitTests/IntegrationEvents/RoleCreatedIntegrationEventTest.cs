using System;

using eDoxa.Identity.Api.IntegrationEvents;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Identity.UnitTests.IntegrationEvents
{
    [TestClass]
    public sealed class RoleCreatedIntegrationEventTest
    {
        [TestMethod]
        public void RoleCreatedIntegrationEvent_WithNewRole_ShouldBeEquivalentToRoleCreatedEvent()
        {
            //Arrange
            var roleCreatedEvent = new RoleCreatedIntegrationEvent("role");

            var serializedEvent = JsonConvert.SerializeObject(roleCreatedEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<RoleCreatedIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(roleCreatedEvent);
        }
    }
}
