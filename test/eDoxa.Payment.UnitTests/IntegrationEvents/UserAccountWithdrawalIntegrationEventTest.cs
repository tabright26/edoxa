// Filename: UserAccountWithdrawalIntegrationEventTest.cs
// Date Created: 2019-08-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Payment.Api.IntegrationEvents;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Payment.UnitTests.IntegrationEvents
{
    [TestClass]
    public sealed class UserAccountWithdrawalIntegrationEventTest
    {
        [TestMethod]
        public void DeserializeObject_WhenDeserializeWithJsonConstructor_ShouldBeEquivalentToIntegrationEvent()
        {
            //Arrange
            var integrationEvent = new UserAccountWithdrawalIntegrationEvent(Guid.NewGuid(), "Test transaction", "123", 123);

            var integrationEventSerialized = JsonConvert.SerializeObject(integrationEvent);

            //Act
            var integrationEventDeserialized = JsonConvert.DeserializeObject<UserAccountWithdrawalIntegrationEvent>(integrationEventSerialized);

            //Assert
            integrationEventDeserialized.Should().BeEquivalentTo(integrationEvent);
        }
    }
}
