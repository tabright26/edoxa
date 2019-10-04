﻿// Filename: EmailSentIntegrationEventTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Notifications.Api.IntegrationEvents;
using eDoxa.Notifications.Domain.Models;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Notifications.UnitTests.IntegrationEvents
{
    public sealed class EmailSentIntegrationEventTest
    {
        [Fact]
        public void DeserializeObject_WhenDeserializeWithJsonConstructor_ShouldBeEquivalentToIntegrationEvent()
        {
            //Arrange
            var integrationEvent = new EmailSentIntegrationEvent(
                new UserId(),
                "gabriel@edoxa.gg",
                "mange Dla Baloney",
                "Mah man");

            var integrationEventSerialized = JsonConvert.SerializeObject(integrationEvent);

            //Act
            var integrationEventDeserialized = JsonConvert.DeserializeObject<EmailSentIntegrationEvent>(integrationEventSerialized);

            //Assert
            integrationEventDeserialized.Should().BeEquivalentTo(integrationEvent);
        }
    }
}
