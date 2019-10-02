﻿// Filename: PersonalInfoPostRequestTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Identity.Api.Areas.Identity.Requests;
using eDoxa.Identity.Api.Infrastructure.Models;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Requests
{
    public sealed class PersonalInfoPostRequestTest
    {
        [Fact]
        public void DeserializeObject_WhenDeserializeWithDataContractConstructor_ShouldBeEquivalentToRequest()
        {
            // Arrange
            var request = new PersonalInfoPostRequest(
                "FirstName",
                "LastName",
                Gender.Other,
                DateTime.UnixEpoch);

            var requestSerialized = JsonConvert.SerializeObject(request);

            // Act
            var requestDeserialized = JsonConvert.DeserializeObject<PersonalInfoPostRequest>(requestSerialized);

            // Assert
            requestDeserialized.Should().BeEquivalentTo(request);
        }
    }
}
