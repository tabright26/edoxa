// Filename: LeagueOfLegendsServiceTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Games.LeagueOfLegends;
using eDoxa.Games.LeagueOfLegends.Services;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;

using FluentAssertions;

using Microsoft.Extensions.Options;

using Moq;

using Xunit;

namespace eDoxa.Games.UnitTests.Games.LeagueOfLegends.Services
{
    public sealed class LeagueOfLegendsServiceTest : UnitTest
    {
        public LeagueOfLegendsServiceTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void Constructor()
        {
            //Arrange
            var mockOptionsSnapshot = new Mock<IOptionsSnapshot<LeagueOfLegendsOptions>>();

            mockOptionsSnapshot.Setup(optionsSnapshot => optionsSnapshot.Value)
                .Returns(
                    new LeagueOfLegendsOptions
                    {
                        ApiKey = "testKey"
                    });

            var leagueService = new LeagueOfLegendsService(mockOptionsSnapshot.Object);

            //Assert
            leagueService.Should().BeOfType<LeagueOfLegendsService>();
        }
    }
}
