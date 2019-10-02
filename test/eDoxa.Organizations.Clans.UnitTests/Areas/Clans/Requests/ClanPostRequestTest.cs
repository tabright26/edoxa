// Filename: ClanPostRequestTest.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Organizations.Clans.Api.Areas.Clans.Requests;
using eDoxa.Organizations.Clans.TestHelpers;
using eDoxa.Organizations.Clans.TestHelpers.Fixtures;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Organizations.Clans.UnitTests.Areas.Clans.Requests
{
    public sealed class ClanPostRequestTest : UnitTest
    {
        public ClanPostRequestTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public void DeserializeObject_WhenDeserializeWithDataContractConstructor_ShouldBeEquivalentToRequest()
        {
            //Arrange
            var request = new ClanPostRequest("BroClan", "Bad Clan");

            var requestSerialized = JsonConvert.SerializeObject(request);

            //Act
            var requestDeserialized = JsonConvert.DeserializeObject<ClanPostRequest>(requestSerialized);

            //Assert
            requestDeserialized.Should().BeEquivalentTo(request);
        }
    }
}
