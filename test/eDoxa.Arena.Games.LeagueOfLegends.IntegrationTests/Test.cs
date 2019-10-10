// Filename: Test.cs
// Date Created: 2019-10-05
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Arena.Games.LeagueOfLegends.TestHelpers;
using eDoxa.Arena.Games.LeagueOfLegends.TestHelpers.Fixtures;

using RiotSharp;
using RiotSharp.Interfaces;

using Xunit;

namespace eDoxa.Arena.Games.LeagueOfLegends.IntegrationTests
{
    public sealed class Test : IntegrationTest
    {
        public Test(TestApiFixture testApi) : base(testApi)
        {
        }

        [Fact]
        public async Task TestMethod()
        {

            //var api = RiotApi.GetDevelopmentInstance("RGAPI-da68d5a3-d8af-48e1-a2c9-f92e81e88647");

            //api.Summoner.GetSummonerByNameAsync()
        }
    }
}
