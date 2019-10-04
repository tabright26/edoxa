// Filename: UserTransactionFailedIntegrationEventTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Payment.Api.IntegrationEvents;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Payment.UnitTests.IntegrationEvents
{
    public sealed class UserTransactionFailedIntegrationEventTest
    {
        [Fact]
        public void DeserializeObject_WhenDeserializeWithJsonConstructor_ShouldBeEquivalentToIntegrationEvent()
        {
            //Arrange
            var integrationEvent = new UserTransactionFailedIntegrationEvent(Guid.NewGuid());

            var integrationEventSerialized = JsonConvert.SerializeObject(integrationEvent);

            //Act
            var integrationEventDeserialized = JsonConvert.DeserializeObject<UserTransactionFailedIntegrationEvent>(integrationEventSerialized);

            //Assert
            integrationEventDeserialized.Should().BeEquivalentTo(integrationEvent);
        }
    }
}
