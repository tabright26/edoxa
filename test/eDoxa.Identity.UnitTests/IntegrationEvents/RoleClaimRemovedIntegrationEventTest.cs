using System;

using eDoxa.Identity.Api.IntegrationEvents;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Identity.UnitTests.IntegrationEvents
{
    [TestClass]
    public sealed class RoleClaimRemovedIntegrationEventTest
    {
        [TestMethod]
        public void RoleClaimRemovedIntegrationEvent_WithNewRole_ShouldBeEquivalentToRoleClaimRemovedEvent()
        {
            //Arrange
            var roleClaimRemovedEvent = new RoleClaimRemovedIntegrationEvent("admin", "test", "allow");

            var serializedEvent = JsonConvert.SerializeObject(roleClaimRemovedEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<RoleClaimRemovedIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(roleClaimRemovedEvent);
        }
    }
}
