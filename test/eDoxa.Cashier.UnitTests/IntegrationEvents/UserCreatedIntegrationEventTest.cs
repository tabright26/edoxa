// Filename: UserCreatedIntegrationEventTest.cs
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
    public sealed class UserCreatedIntegrationEventTest
    {
        [Fact]
        public void UserCreatedIntegrationEvent_WithNewUserAccount_ShouldBeEquivalentToUserCreationEvent()
        {
            //Arrange
            var userCreatedEvent = new UserCreatedIntegrationEvent(Guid.NewGuid());

            var serializedEvent = JsonConvert.SerializeObject(userCreatedEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<UserCreatedIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(userCreatedEvent);
        }
    }
}
