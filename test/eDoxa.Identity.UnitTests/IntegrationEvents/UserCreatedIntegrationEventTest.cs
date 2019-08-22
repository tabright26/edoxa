using System;

using eDoxa.Identity.Api.IntegrationEvents;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Identity.UnitTests.IntegrationEvents
{
    [TestClass]
    public sealed class UserCreatedIntegrationEventTest
    {
        [TestMethod]
        public void UserCreatedIntegrationEvent_WithNewGuid_ShouldBeEquivalentToUserCreatedEvent()
        {
            //Arrange
            var userCreatedEvent = new UserCreatedIntegrationEvent(Guid.NewGuid());

            var serializedEvent = JsonConvert.SerializeObject(userCreatedEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<UserCreatedIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(userCreatedEvent);
        }
    }
}
