// Filename: UserTransactionSuccededIntegrationEventTest.cs
// Date Created: 2019-09-29
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.Api.IntegrationEvents;
using eDoxa.Payment.TestHelper;
using eDoxa.Payment.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Payment.UnitTests.IntegrationEvents
{
    public sealed class UserTransactionSuccededIntegrationEventTest : UnitTest
    {
        public UserTransactionSuccededIntegrationEventTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public void DeserializeObject_WhenDeserializeWithJsonConstructor_ShouldBeEquivalentToIntegrationEvent()
        {
            //Arrange
            var integrationEvent = new UserTransactionSuccededIntegrationEvent(new UserId(), new TransactionId());

            var integrationEventSerialized = JsonConvert.SerializeObject(integrationEvent);

            //Act
            var integrationEventDeserialized = JsonConvert.DeserializeObject<UserTransactionSuccededIntegrationEvent>(integrationEventSerialized);

            //Assert
            integrationEventDeserialized.Should().BeEquivalentTo(integrationEvent);
        }
    }
}
