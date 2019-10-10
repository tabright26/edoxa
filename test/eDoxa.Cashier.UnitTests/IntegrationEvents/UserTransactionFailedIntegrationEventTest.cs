// Filename: UserTransactionFailedIntegrationEventTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.IntegrationEvents;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents
{
    public sealed class UserTransactionFailedIntegrationEventTest
    {
        [Fact]
        public void UserTransactionFailedIntegrationEvent_WithNewUserAccount_ShouldBeEquivalentToTransactionFailedEvent()
        {
            //Arrange
            var transactionFailedEvent = new UserTransactionFailedIntegrationEvent(new TransactionId());

            var serializedEvent = JsonConvert.SerializeObject(transactionFailedEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<UserTransactionFailedIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(transactionFailedEvent);
        }
    }
}
