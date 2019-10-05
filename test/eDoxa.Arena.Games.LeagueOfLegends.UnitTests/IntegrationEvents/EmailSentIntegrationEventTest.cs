// Filename: EmailSentIntegrationEventTest.cs
// Date Created: 2019-10-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Games.LeagueOfLegends.Api.Infrastructure.Models;
using eDoxa.Arena.Games.LeagueOfLegends.Api.IntegrationEvents;
using eDoxa.Arena.Games.LeagueOfLegends.TestHelpers;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Arena.Games.LeagueOfLegends.UnitTests.IntegrationEvents
{
    public sealed class EmailSentIntegrationEventTest : UnitTest
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
