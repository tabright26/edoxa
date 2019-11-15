// Filename: LeagueOfLegendsAuthFactorValidatorAdapterTest.cs
// Date Created: 2019-11-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels;
using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Games.LeagueOfLegends;
using eDoxa.Games.LeagueOfLegends.Abstactions;
using eDoxa.Games.LeagueOfLegends.Adapter;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Moq;

using RiotSharp.Endpoints.SummonerEndpoint;
using RiotSharp.Misc;

using Xunit;

namespace eDoxa.Games.UnitTests.Games.LeagueOfLegends.Adapter
{
    public sealed class LeagueOfLegendsAuthFactorValidatorAdapterTest : UnitTest
    {
        public LeagueOfLegendsAuthFactorValidatorAdapterTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task ValidateAuthFactorAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var userId = new UserId();

            var mockAuthFactorRepository = new Mock<IAuthenticationRepository>();
            var mockLeagueOfLegendsService = new Mock<ILeagueOfLegendsService>();

            var summoner = new Summoner()
            {
                ProfileIconId = 0,
                Name = "testName",
                Region = Region.Na,
                AccountId = "testId"
            };

            mockLeagueOfLegendsService
                .Setup(leagueService => leagueService.Summoner.GetSummonerByAccountIdAsync(It.IsAny<RiotSharp.Misc.Region>(), It.IsAny<string>()))
                .ReturnsAsync(summoner)
                .Verifiable();

            mockAuthFactorRepository
                .Setup(repository => repository.RemoveAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var authFactorService = new LeagueOfLegendsAuthenticationValidatorAdapter(mockLeagueOfLegendsService.Object, mockAuthFactorRepository.Object);

            // Act
            var result = await authFactorService.ValidateAuthenticationAsync(userId, new GameAuthentication<LeagueOfLegendsGameAuthenticationFactor>(new PlayerId(), new LeagueOfLegendsGameAuthenticationFactor(1, string.Empty, 2, string.Empty)));

            // Assert
            result.Should().BeOfType<FluentValidation.Results.ValidationResult>();

            mockLeagueOfLegendsService.Verify(
                leagueService => leagueService.Summoner.GetSummonerByAccountIdAsync(It.IsAny<RiotSharp.Misc.Region>(), It.IsAny<string>()),
                Times.Once);

            mockAuthFactorRepository.Verify(
                repository => repository.RemoveAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>()),
                Times.Once);

        }

        [Fact]
        public async Task ValidateAuthFactorAsync_ShouldBeOfTypeValidationFailureResult()
        {
            // Arrange
            var userId = new UserId();

            var mockAuthFactorRepository = new Mock<IAuthenticationRepository>();
            var mockLeagueOfLegendsService = new Mock<ILeagueOfLegendsService>();

            var summoner = new Summoner()
            {
                ProfileIconId = 0,
                Name = "testName",
                Region = Region.Na,
                AccountId = "testId"
            };

            mockLeagueOfLegendsService
                .Setup(leagueService => leagueService.Summoner.GetSummonerByAccountIdAsync(It.IsAny<RiotSharp.Misc.Region>(), It.IsAny<string>()))
                .ReturnsAsync(summoner)
                .Verifiable();

            mockAuthFactorRepository
                .Setup(repository => repository.RemoveAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var authFactorService = new LeagueOfLegendsAuthenticationValidatorAdapter(mockLeagueOfLegendsService.Object, mockAuthFactorRepository.Object);

            // Act
            var result = await authFactorService.ValidateAuthenticationAsync(userId, new GameAuthentication<LeagueOfLegendsGameAuthenticationFactor>(new PlayerId(), new LeagueOfLegendsGameAuthenticationFactor(1, string.Empty, 2, string.Empty)));

            // Assert
            result.Should().BeOfType<FluentValidation.Results.ValidationResult>();

            mockLeagueOfLegendsService.Verify(
                leagueService => leagueService.Summoner.GetSummonerByAccountIdAsync(It.IsAny<RiotSharp.Misc.Region>(), It.IsAny<string>()),
                Times.Once);

            mockAuthFactorRepository.Verify(
                repository => repository.RemoveAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>()),
                Times.Once);
        }
    }
}
