// Filename: UserPhoneChangedIntegrationEventTest.cs
// Date Created: 2019-10-11
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.Api.IntegrationEvents;
using eDoxa.Payment.TestHelper;
using eDoxa.Payment.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Payment.UnitTests.IntegrationEvents
{
    public sealed class UserPhoneChangedIntegrationEventTest : UnitTest
    {
        public UserPhoneChangedIntegrationEventTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public void DeserializeObject_WhenDeserializeWithJsonConstructor_ShouldBeEquivalentToIntegrationEvent()
        {
            //Arrange
            var integrationEvent = new UserPhoneChangedIntegrationEvent(
                new UserId(),
                "514-420-6969");

            var integrationEventSerialized = JsonConvert.SerializeObject(integrationEvent);

            //Act
            var integrationEventDeserialized = JsonConvert.DeserializeObject<UserPhoneChangedIntegrationEvent>(integrationEventSerialized);

            //Assert
            integrationEventDeserialized.Should().BeEquivalentTo(integrationEvent);
        }
    }
}
