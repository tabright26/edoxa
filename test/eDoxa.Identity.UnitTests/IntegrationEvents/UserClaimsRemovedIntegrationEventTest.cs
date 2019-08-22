using System;
using System.Collections.Generic;

using eDoxa.Identity.Api.IntegrationEvents;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Identity.UnitTests.IntegrationEvents
{
    [TestClass]
    public sealed class UserClaimsRemovedIntegrationEventTest
    {
        [TestMethod]
        public void UserClaimsRemovedIntegrationEvent_WithNewClaim_ShouldBeEquivalentToUserClaimRemovedEvent()
        {
            //Arrange
            var userClaimRemovedEvent = new UserClaimsRemovedIntegrationEvent(Guid.NewGuid(), new Dictionary<string, string>());

            var serializedEvent = JsonConvert.SerializeObject(userClaimRemovedEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<UserClaimsRemovedIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(userClaimRemovedEvent);
        }
    }
}
