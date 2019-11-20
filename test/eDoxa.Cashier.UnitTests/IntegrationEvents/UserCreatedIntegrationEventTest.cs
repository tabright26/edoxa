// Filename: UserCreatedIntegrationEventTest.cs
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
    public sealed class UserCreatedIntegrationEventTest
    {
        [Fact]
        public void UserCreatedIntegrationEvent_WithNewUserAccount_ShouldBeEquivalentToUserCreationEvent()
        {
            //Arrange
            var userCreatedEvent = new UserCreatedIntegrationEvent(new UserId(), "noreply@edoxa.gg", "CA");

            var serializedEvent = JsonConvert.SerializeObject(userCreatedEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<UserCreatedIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(userCreatedEvent);
        }
    }
}
