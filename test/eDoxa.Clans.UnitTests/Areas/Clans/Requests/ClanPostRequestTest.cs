// Filename: ClanPostRequestTest.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Clans.Requests;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Clans.UnitTests.Areas.Clans.Requests
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
