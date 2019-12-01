// Filename: UserCreatedIntegrationEventTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.IntegrationEvents;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Identity.UnitTests.IntegrationEvents
{
    public sealed class UserCreatedIntegrationEventTest : UnitTest
    {
        public UserCreatedIntegrationEventTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void DeserializeObject_WhenDeserializeWithJsonConstructor_ShouldBeEquivalentToIntegrationEvent()
        {
            //Arrange
            var integrationEvent = new UserCreatedIntegrationEvent(new UserId(), "noreply@edoxa.gg", Country.Canada);

            var integrationEventSerialized = JsonConvert.SerializeObject(integrationEvent);

            //Act
            var integrationEventDeserialized = JsonConvert.DeserializeObject<UserCreatedIntegrationEvent>(integrationEventSerialized);

            //Assert
            integrationEventDeserialized.Should().BeEquivalentTo(integrationEvent);
        }
    }
}
