// Filename: LeagueOfLegendsServiceTest.cs
// Date Created: 2020-01-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;

namespace eDoxa.Games.UnitTests.Application.Games.LeagueOfLegends.Services
{
    public sealed class LeagueOfLegendsServiceTest : UnitTest
    {
        public LeagueOfLegendsServiceTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        //[Fact]
        //public void Constructor()
        //{
        //    //Arrange
        //    var mockOptionsSnapshot = new Mock<IOptionsSnapshot<GamesApiOptions>>();

        //    mockOptionsSnapshot.Setup(optionsSnapshot => optionsSnapshot.Value)
        //        .Returns(
        //            new GamesApiOptions
        //            {
        //                Configuration = new GamesApiOptions.Types.ConfigurationOptions
        //                {
        //                    ApiKeys =
        //                    {
        //                        {Game.LeagueOfLegends.Name, "ApiKey"}
        //                    }
        //                }
        //            });

        //    var leagueService = new LeagueOfLegendsService(mockOptionsSnapshot.Object);

        //    //Assert
        //    leagueService.Should().BeOfType<LeagueOfLegendsService>();
        //}
    }
}
