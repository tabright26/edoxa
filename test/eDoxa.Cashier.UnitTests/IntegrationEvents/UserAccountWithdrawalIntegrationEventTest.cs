// Filename: UserAccountWithdrawalIntegrationEventTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.IntegrationEvents;
using eDoxa.Seedwork.Domain.Misc;

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
                new UserId(),
                "noreply@edoxa.gg",
                new TransactionId(),
                "TransactionDescription",
                123);

            var serializedEvent = JsonConvert.SerializeObject(withdrawalEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<UserAccountWithdrawalIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(withdrawalEvent);
        }
    }
}
