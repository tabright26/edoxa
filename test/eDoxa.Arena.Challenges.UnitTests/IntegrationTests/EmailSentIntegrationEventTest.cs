// Filename: EmailSentIntegrationEventTest.cs
// Date Created: 2019-10-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Challenges.Api.IntegrationEvents;
using eDoxa.Arena.Challenges.Domain.AggregateModels;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.IntegrationTests
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
