// Filename: UserAccountDepositIntegrationEventTest.cs
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
    public sealed class UserAccountDepositIntegrationEventTest
    {
        [Fact]
        public void UserCreatedIntegrationEvent_WithNewUserAccount_ShouldBeEquivalentToDepositEvent()
        {
            //Arrange
            var depositEvent = new UserAccountDepositIntegrationEvent(
                Guid.NewGuid(),
                "Test transaction",
                "123",
                123);

            var serializedEvent = JsonConvert.SerializeObject(depositEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<UserAccountDepositIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(depositEvent);
        }
    }
}
