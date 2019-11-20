// Filename: UserTransactionSuccededIntegrationEventTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.IntegrationEvents;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents
{
    public sealed class UserTransactionSuccededIntegrationEventTest
    {
        [Fact]
        public void UserTransactionSuccededIntegrationEvent_WithNewUserAccount_ShouldBeEquivalentToTransactionSuccededEvent()
        {
            //Arrange
            var integrationEvent = new UserTransactionSuccededIntegrationEvent(new UserId(), new TransactionId());

            var integrationEventSerialized = JsonConvert.SerializeObject(integrationEvent);

            //Act
            var integrationEventDeserialized = JsonConvert.DeserializeObject<UserTransactionSuccededIntegrationEvent>(integrationEventSerialized);

            //Assert
            integrationEventDeserialized.Should().BeEquivalentTo(integrationEvent);
        }
    }
}
