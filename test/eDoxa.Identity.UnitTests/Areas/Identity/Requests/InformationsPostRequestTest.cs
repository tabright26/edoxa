// Filename: InformationsPostRequestTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Identity.Requests;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Requests
{
    public sealed class InformationsPostRequestTest
    {
        [Fact]
        public void DeserializeObject_WhenDeserializeWithDataContractConstructor_ShouldBeEquivalentToRequest()
        {
            // Arrange
            var unixEpoch = DateTime.UnixEpoch;

            var request = new InformationsPostRequest(
                "FirstName",
                "LastName",
                Gender.Other.Name,
                unixEpoch.Year,
                unixEpoch.Month,
                unixEpoch.Day);

            var requestSerialized = JsonConvert.SerializeObject(request);

            // Act
            var requestDeserialized = JsonConvert.DeserializeObject<InformationsPostRequest>(requestSerialized);

            // Assert
            requestDeserialized.Should().BeEquivalentTo(request);
        }
    }
}
