using System;

using eDoxa.Identity.Api.IntegrationEvents;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Identity.UnitTests.IntegrationEvents
{
    [TestClass]
    public sealed class UserRoleAddedIntegrationEventTest
    {
        [TestMethod]
        public void UserRoleAddedIntegrationEvent_WithNewUserAccount_ShouldBeEquivalentToUserRoleAddedEvent()
        {
            //Arrange
            var userRoleAddedEvent = new UserRoleAddedIntegrationEvent(Guid.NewGuid(), "role");

            var serializedEvent = JsonConvert.SerializeObject(userRoleAddedEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<UserRoleAddedIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(userRoleAddedEvent);
        }
    }
}
