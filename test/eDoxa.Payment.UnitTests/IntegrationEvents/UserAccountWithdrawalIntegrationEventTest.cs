using System;

using eDoxa.Payment.Api.IntegrationEvents;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Payment.UnitTests.IntegrationEvents
{
    [TestClass]
    public sealed class UserAccountWithdrawalIntegrationEventTest
    {
        [TestMethod]
        public void UserAccountWithdrawalIntegrationEvent_WithNewUserAccount_ShouldBeEquivalentToWithdrawalEvent()
        {
            //Arrange
            var withdrawalEvent = new UserAccountWithdrawalIntegrationEvent(Guid.NewGuid(), "Test transaction", "123", 123);

            var serializedEvent = JsonConvert.SerializeObject(withdrawalEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<UserAccountWithdrawalIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(withdrawalEvent);
        }
    }
}
