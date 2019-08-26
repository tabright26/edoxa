using System;

using eDoxa.Payment.Api.IntegrationEvents;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Payment.UnitTests.IntegrationEvents
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
            var deserializedEvent = JsonConvert.DeserializeObject<UserTransactionSuccededIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(transactionSuccededEvent);
        }
    }
}
