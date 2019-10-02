// Filename: UserTransactionSuccededIntegrationEventTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Cashier.Api.IntegrationEvents;

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
            var transactionSuccededEvent = new UserTransactionSuccededIntegrationEvent(Guid.NewGuid());

            var serializedEvent = JsonConvert.SerializeObject(transactionSuccededEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<UserTransactionSuccededIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(transactionSuccededEvent);
        }
    }
}
