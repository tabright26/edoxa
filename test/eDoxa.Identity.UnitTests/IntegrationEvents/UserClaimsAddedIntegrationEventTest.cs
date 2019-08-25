using System;
using System.Collections.Generic;

using eDoxa.Identity.Api.IntegrationEvents;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Identity.UnitTests.IntegrationEvents
{
    [TestClass]
    public sealed class UserClaimsAddedIntegrationEventTest
    {
        [TestMethod]
        public void UserClaimsAddedIntegrationEvent_WithNewClaim_ShouldBeEquivalentToUserClaimAddedEvent()
        {
            //Arrange
            var userClaimAddedEvent = new UserClaimsAddedIntegrationEvent(Guid.NewGuid(), new Dictionary<string, string>());

            var serializedEvent = JsonConvert.SerializeObject(userClaimAddedEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<UserClaimsAddedIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(userClaimAddedEvent);
        }
    }
}
