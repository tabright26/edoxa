using System;

using eDoxa.Identity.Api.IntegrationEvents;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Identity.UnitTests.IntegrationEvents
{
    [TestClass]
    public sealed class RoleClaimAddedIntegrationEventTest
    {
        [TestMethod]
        public void RoleClaimAddedIntegrationEvent_WithNewRole_ShouldBeEquivalentToRoleClaimAddedEvent()
        {
            //Arrange
            var roleClaimAddedEvent = new RoleClaimAddedIntegrationEvent("admin", "test", "allow");

            var serializedEvent = JsonConvert.SerializeObject(roleClaimAddedEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<RoleClaimAddedIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(roleClaimAddedEvent);
        }
    }
}
