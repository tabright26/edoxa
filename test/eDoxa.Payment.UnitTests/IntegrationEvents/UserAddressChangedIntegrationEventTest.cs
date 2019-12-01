// Filename: UserAddressChangedIntegrationEventTest.cs
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
    public sealed class UserAddressChangedIntegrationEventTest : UnitTest
    {
        public UserAddressChangedIntegrationEventTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public void DeserializeObject_WhenDeserializeWithJsonConstructor_ShouldBeEquivalentToIntegrationEvent()
        {
            //Arrange
            var integrationEvent = new UserAddressChangedIntegrationEvent(
                new UserId(),
                "This is address", null, null, "Montreal", "A1B2C3");

            var integrationEventSerialized = JsonConvert.SerializeObject(integrationEvent);

            //Act
            var integrationEventDeserialized = JsonConvert.DeserializeObject<UserAddressChangedIntegrationEvent>(integrationEventSerialized);

            //Assert
            integrationEventDeserialized.Should().BeEquivalentTo(integrationEvent);
        }
    }
}
