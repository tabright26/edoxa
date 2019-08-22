using System;

using eDoxa.Cashier.Api.IntegrationEvents;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents
{
    [TestClass]
    public sealed class UserTransactionSuccededIntegrationEventTest
    {
        [TestMethod]
        public void UserTransactionSuccededIntegrationEvent_WithNewUserAccount_ShouldBeEquivalentToTransactionSuccededEvent()
        {
            //Arrange
            var transactionSuccededEvent = new UserTransactionSuccededIntegrationEvent(Guid.NewGuid());

            var serializedEvent = JsonConvert.SerializeObject(transactionSuccededEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<UserAccountDepositIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(transactionSuccededEvent);
        }
    }
}
