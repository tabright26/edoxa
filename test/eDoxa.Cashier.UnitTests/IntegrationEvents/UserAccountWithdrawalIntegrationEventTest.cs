// Filename: UserAccountWithdrawalIntegrationEventTest.cs
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
    public sealed class UserAccountWithdrawalIntegrationEventTest
    {
        [Fact]
        public void UserAccountWithdrawalIntegrationEvent_WithNewUserAccount_ShouldBeEquivalentToWithdrawalEvent()
        {
            //Arrange
            var withdrawalEvent = new UserAccountWithdrawalIntegrationEvent(
                Guid.NewGuid(),
                "Test transaction",
                "123",
                123);

            var serializedEvent = JsonConvert.SerializeObject(withdrawalEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<UserAccountWithdrawalIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(withdrawalEvent);
        }
    }
}
