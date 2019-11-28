// Filename: CreateAddressRequestTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Requests;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Identity.UnitTests.Requests
{
    public sealed class CreateAddressRequestTest
    {
        [Fact]
        public void DeserializeObject_WhenDeserializeWithDataContractConstructor_ShouldBeEquivalentToRequest()
        {
            // Arrange
            var country = Country.Canada.Name;

            var request = new CreateAddressRequest(
                country,
                "Line1",
                "Line2",
                "City",
                "Country",
                "PostalCode");

            var requestSerialized = JsonConvert.SerializeObject(request);

            // Act
            var requestDeserialized = JsonConvert.DeserializeObject<CreateAddressRequest>(requestSerialized);

            // Assert
            requestSerialized.Should().Contain(country);
            requestDeserialized.Should().BeEquivalentTo(request);
        }
    }
}
