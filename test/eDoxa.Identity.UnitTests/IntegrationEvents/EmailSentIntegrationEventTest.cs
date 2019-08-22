using System;

using eDoxa.Identity.Api.IntegrationEvents;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Identity.UnitTests.IntegrationEvents
{
    [TestClass]
    public sealed class EmailSentIntegrationEventTest
    {
        [TestMethod]
        public void EmailSentIntegrationEvent_WithNewEmail_ShouldBeEquivalentToSendingEvent()
        {
            //Arrange
            var sendingEvent = new EmailSentIntegrationEvent("gabriel@edoxa.gg", "mange Dla Baloney", "Mah man");

            var serializedEvent = JsonConvert.SerializeObject(sendingEvent);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<EmailSentIntegrationEvent>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(sendingEvent);
        }
    }
}
