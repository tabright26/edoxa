using System;

using eDoxa.Payment.Api.IntegrationEvents;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents
{
    [TestClass]
    public sealed class UserAccountDepositIntegrationEventTest
    {
        [TestMethod]
        public void UserCreatedIntegrationEvent_WithNewUserAccount_ShouldBeEquivalentToDepositEvent()
        {
            //Arrange
            var depositEvent = new UserAccountDepositIntegrationEvent(Guid.NewGuid(), "Test transaction", "123", 123);

            var serializedEvent = JsonConvert.SerializeObject(depositEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<UserAccountDepositIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(depositEvent);
        }
    }
}
