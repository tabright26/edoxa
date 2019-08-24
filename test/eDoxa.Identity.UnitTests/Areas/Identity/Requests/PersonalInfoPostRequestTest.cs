// Filename: PersonalInfoPostRequestTest.cs
// Date Created: 2019-08-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Identity.Api.Areas.Identity.Requests;
using eDoxa.Identity.Api.Infrastructure.Models;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Requests
{
    [TestClass]
    public sealed class PersonalInfoPostRequestTest
    {
        [TestMethod]
        public void DeserializeObject_WhenDeserializeWithDataContractContructor_ShouldBeEquivalentToRequest()
        {
            // Arrange
            var request = new PersonalInfoPostRequest("Bob", "Bob", Gender.Male, new DateTime(2000, 1, 1));

            var requestSerialized = JsonConvert.SerializeObject(request);

            // Act
            var requestDeserialized = JsonConvert.DeserializeObject<PersonalInfoPostRequest>(requestSerialized);

            // Assert
            requestDeserialized.Should().BeEquivalentTo(request);
        }
    }
}
