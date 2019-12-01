// Filename: ChangeDoxatagRequestTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Requests;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Identity.UnitTests.Requests
{
    public sealed class ChangeDoxatagRequestTest
    {
        [Fact]
        public void DeserializeObject_WhenDeserializeWithDataContractConstructor_ShouldBeEquivalentToRequest()
        {
            // Arrange
            var request = new ChangeDoxatagRequest("Doxatag");
            var requestSerialized = JsonConvert.SerializeObject(request);

            // Act
            var requestDeserialized = JsonConvert.DeserializeObject<ChangeDoxatagRequest>(requestSerialized);

            // Assert
            requestDeserialized.Should().BeEquivalentTo(request);
        }
    }
}
