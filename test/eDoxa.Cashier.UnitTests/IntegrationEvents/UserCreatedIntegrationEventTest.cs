using System;

using eDoxa.Cashier.Api.IntegrationEvents;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents
{
    [TestClass]
    public sealed class UserCreatedIntegrationEventTest
    {
        [TestMethod]
        public void UserCreatedIntegrationEventTest_WithNewUserAccount_ShouldBeEquivalentToUserCreationEvent()
        {
            //Arrange
            var userCreationEvent = new UserCreatedIntegrationEventTest();

            var serializedEvent = JsonConvert.SerializeObject(userCreationEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<UserCreatedIntegrationEventTest>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(userCreationEvent);
        }
    }
}
