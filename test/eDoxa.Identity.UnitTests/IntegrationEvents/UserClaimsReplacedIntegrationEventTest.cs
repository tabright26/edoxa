using System;
using System.Collections.Generic;

using eDoxa.Identity.Api.IntegrationEvents;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Identity.UnitTests.IntegrationEvents
{
    [TestClass]
    public sealed class UserClaimsReplacedIntegrationEventTest
    {
        [TestMethod]
        public void UserClaimsReplacedIntegrationEvent_WithNewClaim_ShouldBeEquivalentToUserClaimReplacedEvent()
        {
            //Arrange
            var userClaimReplacedEvent = new UserClaimsReplacedIntegrationEvent(Guid.NewGuid(), 1, new Dictionary<string, string>(), new Dictionary<string, string>());

            var serializedEvent = JsonConvert.SerializeObject(userClaimReplacedEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<UserClaimsReplacedIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(userClaimReplacedEvent);
        }
    }
}
