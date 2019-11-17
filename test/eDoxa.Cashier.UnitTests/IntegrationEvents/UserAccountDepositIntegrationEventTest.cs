// Filename: UserAccountDepositIntegrationEventTest.cs
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
    public sealed class UserAccountDepositIntegrationEventTest
    {
        [Fact]
        public void UserCreatedIntegrationEvent_WithNewUserAccount_ShouldBeEquivalentToDepositEvent()
        {
            //Arrange
            var depositEvent = new UserAccountDepositIntegrationEvent(
                new UserId(),
                "noreply@edoxa.gg",
                new TransactionId(),
                "TransactionDescription",
                123);

            var serializedEvent = JsonConvert.SerializeObject(depositEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<UserAccountDepositIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(depositEvent);
        }
    }
}
