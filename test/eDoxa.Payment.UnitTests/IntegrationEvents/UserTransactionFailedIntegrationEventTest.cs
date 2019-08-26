using System;

using eDoxa.Payment.Api.IntegrationEvents;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Payment.UnitTests.IntegrationEvents
{
    [TestClass]
    public sealed class UserTransactionFailedIntegrationEventTest
    {
        [TestMethod]
        public void UserTransactionFailedIntegrationEvent_WithNewUserAccount_ShouldBeEquivalentToTransactionFailedEvent()
        {
            //Arrange
            var transactionFailedEvent = new UserTransactionFailedIntegrationEvent(Guid.NewGuid());

            var serializedEvent = JsonConvert.SerializeObject(transactionFailedEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<UserTransactionFailedIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(transactionFailedEvent);
        }
    }
}
