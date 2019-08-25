using System;

using eDoxa.Identity.Api.IntegrationEvents;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Identity.UnitTests.IntegrationEvents
{
    [TestClass]
    public sealed class UserRoleRemovedIntegrationEventTest
    {
        [TestMethod]
        public void UserRoleRemovedIntegrationEvent_WithNewUserAccount_ShouldBeEquivalentToUserRoleRemovedEvent()
        {
            //Arrange
            var userRoleRemovedEvent = new UserRoleRemovedIntegrationEvent(Guid.NewGuid(), "role");

            var serializedEvent = JsonConvert.SerializeObject(userRoleRemovedEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<UserRoleRemovedIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(userRoleRemovedEvent);
        }
    }
}
