// Filename: UserClaimsRemovedIntegrationEventTest.cs
// Date Created: 2019-11-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Games.Api.IntegrationEvents;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.Security;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Games.UnitTests.IntegrationEvents
{
    public sealed class UserClaimsRemovedIntegrationEventTest : UnitTest
    {
        public UserClaimsRemovedIntegrationEventTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void DeserializeObject_WhenDeserializeWithJsonConstructor_ShouldBeEquivalentToIntegrationEvent()
        {
            //Arrange
            var integrationEvent = new UserClaimsRemovedIntegrationEvent(
                new UserId(),
                new Claims());

            var integrationEventSerialized = JsonConvert.SerializeObject(integrationEvent);

            //Act
            var integrationEventDeserialized = JsonConvert.DeserializeObject<UserClaimsRemovedIntegrationEvent>(integrationEventSerialized);

            //Assert
            integrationEventDeserialized.Should().BeEquivalentTo(integrationEvent);
        }
    }
}
