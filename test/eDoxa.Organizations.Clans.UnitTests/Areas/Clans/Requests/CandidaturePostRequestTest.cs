// Filename: AccountDepositPostRequestTest.cs
// Date Created: 2019-08-27
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Organizations.Clans.Api.Areas.Clans.Requests;
using eDoxa.Organizations.Clans.Domain.Models;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Organizations.Clans.UnitTests.Areas.Clans.Requests
{
    [TestClass]
    public sealed class CandidaturePostRequestTest
    {
        [TestMethod]
        public void DeserializeObject_WhenDeserializeWithDataContractConstructor_ShouldBeEquivalentToRequest()
        {
            //Arrange
            var request = new CandidaturePostRequest(new UserId(), new ClanId());

            var requestSerialized = JsonConvert.SerializeObject(request);

            //Act
            var requestDeserialized = JsonConvert.DeserializeObject<CandidaturePostRequest>(requestSerialized);

            //Assert
            requestDeserialized.Should().BeEquivalentTo(request);
        }
    }
}
