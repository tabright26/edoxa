﻿// Filename: RoleDeletedIntegrationEventTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.IntegrationEvents;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Identity.UnitTests.IntegrationEvents
{
    public sealed class RoleDeletedIntegrationEventTest : UnitTest
    {
        public RoleDeletedIntegrationEventTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void DeserializeObject_WhenDeserializeWithJsonConstructor_ShouldBeEquivalentToIntegrationEvent()
        {
            //Arrange
            var integrationEvent = new RoleDeletedIntegrationEvent("role");

            var integrationEventSerialized = JsonConvert.SerializeObject(integrationEvent);

            //Act
            var integrationEventDeserialized = JsonConvert.DeserializeObject<RoleDeletedIntegrationEvent>(integrationEventSerialized);

            //Assert
            integrationEventDeserialized.Should().BeEquivalentTo(integrationEvent);
        }
    }
}
