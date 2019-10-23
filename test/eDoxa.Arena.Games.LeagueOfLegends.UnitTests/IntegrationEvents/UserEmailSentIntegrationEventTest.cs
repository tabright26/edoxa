// Filename: EmailSentIntegrationEventTest.cs
// Date Created: 2019-10-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Games.LeagueOfLegends.Api.IntegrationEvents;
using eDoxa.Arena.Games.LeagueOfLegends.TestHelpers;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Arena.Games.LeagueOfLegends.UnitTests.IntegrationEvents
{
    public sealed class UserEmailSentIntegrationEventTest : UnitTest
    {
        [Fact]
        public void DeserializeObject_WhenDeserializeWithJsonConstructor_ShouldBeEquivalentToIntegrationEvent()
        {
            //Arrange
            var integrationEvent = new UserEmailSentIntegrationEvent(
                new UserId(),
                "mange Dla Baloney",
                "Mah man");

            var integrationEventSerialized = JsonConvert.SerializeObject(integrationEvent);

            //Act
            var integrationEventDeserialized = JsonConvert.DeserializeObject<UserEmailSentIntegrationEvent>(integrationEventSerialized);

            //Assert
            integrationEventDeserialized.Should().BeEquivalentTo(integrationEvent);
        }
    }
}
