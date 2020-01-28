// Filename: LeagueOfLegendsRequestTest.cs
// Date Created: 2020-01-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Games.LeagueOfLegends.Requests;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Games.UnitTests.Games.LeagueOfLegends.Requests
{
    public sealed class LeagueOfLegendsRequestTest : UnitTest
    {
        public LeagueOfLegendsRequestTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void DeserializeObject_WhenDeserializeWithDataContractConstructor_ShouldBeEquivalentToRequest()
        {
            //Arrange
            var request = new LeagueOfLegendsRequest("summonerName");

            var requestSerialized = JsonConvert.SerializeObject(request);

            //Act
            var requestDeserialized = JsonConvert.DeserializeObject<LeagueOfLegendsRequest>(requestSerialized);

            //Assert
            requestDeserialized.Should().BeEquivalentTo(request);
        }
    }
}
