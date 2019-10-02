// Filename: DoxaTagPostRequestTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Areas.Identity.Requests;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Requests
{
    public sealed class DoxaTagPostRequestTest
    {
        [Fact]
        public void DeserializeObject_WhenDeserializeWithDataContractConstructor_ShouldBeEquivalentToRequest()
        {
            // Arrange
            var request = new DoxaTagPostRequest("DoxaTag");
            var requestSerialized = JsonConvert.SerializeObject(request);

            // Act
            var requestDeserialized = JsonConvert.DeserializeObject<DoxaTagPostRequest>(requestSerialized);

            // Assert
            requestDeserialized.Should().BeEquivalentTo(request);
        }
    }
}
