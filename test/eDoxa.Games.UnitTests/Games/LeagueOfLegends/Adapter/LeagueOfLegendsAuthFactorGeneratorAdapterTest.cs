// Filename: LeagueOfLegendsAuthFactorGeneratorAdapterTest.cs
// Date Created: 2019-11-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.AuthFactorAggregate;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Games.LeagueOfLegends.Abstactions;
using eDoxa.Games.LeagueOfLegends.Adapter;
using eDoxa.Games.LeagueOfLegends.Requests;
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
    public sealed class LeagueOfLegendsAuthFactorGeneratorAdapterTest : UnitTest // Francis: How to test the private static method ?
    {
        public LeagueOfLegendsAuthFactorGeneratorAdapterTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task GenerateAuthFactorAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var userId = new UserId();

            var mockAuthFactorRepository = new Mock<IAuthFactorRepository>();
            var mockLeagueOfLegendsService = new Mock<ILeagueOfLegendsService>();

            var summoner = new Summoner()
            {
                ProfileIconId = 0,
                Name = "testName",
                Region = Region.Na,
                AccountId = "testId"
            };

            mockLeagueOfLegendsService
                .Setup(leagueService => leagueService.Summoner.GetSummonerByNameAsync(It.IsAny<RiotSharp.Misc.Region>(), It.IsAny<string>()))
                .ReturnsAsync(summoner)
                .Verifiable();

            mockAuthFactorRepository
                .Setup(repository => repository.AuthFactorExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(true)
                .Verifiable();

            mockAuthFactorRepository
                .Setup(repository => repository.RemoveAuthFactorAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockAuthFactorRepository
                .Setup(repository => repository.AddAuthFactorAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<AuthFactor>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var authFactorService = new LeagueOfLegendsAuthFactorGeneratorAdapter(mockLeagueOfLegendsService.Object, mockAuthFactorRepository.Object);

            // Act
            var result = await authFactorService.GenerateAuthFactorAsync(userId, new LeagueOfLegendsRequest("testSummoner"));

            // Assert
            result.Should().BeOfType<FluentValidation.Results.ValidationResult>();

            mockLeagueOfLegendsService.Verify(
                leagueService => leagueService.Summoner.GetSummonerByNameAsync(It.IsAny<RiotSharp.Misc.Region>(), It.IsAny<string>()),
                Times.Once);

            mockAuthFactorRepository.Verify(
                repository => repository.AuthFactorExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()),
                Times.Once);

            mockAuthFactorRepository.Verify(
                repository => repository.RemoveAuthFactorAsync(It.IsAny<UserId>(), It.IsAny<Game>()),
                Times.Once);

            mockAuthFactorRepository.Verify(
                repository => repository.AddAuthFactorAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<AuthFactor>()),
                Times.Once);
        }

        [Fact]
        public async Task GenerateAuthFactorAsync_ShouldBeOfTypeValidationFailureResult()
        {
            // Arrange
            var userId = new UserId();

            var mockAuthFactorRepository = new Mock<IAuthFactorRepository>();
            var mockLeagueOfLegendsService = new Mock<ILeagueOfLegendsService>();

            mockLeagueOfLegendsService
                .Setup(leagueService => leagueService.Summoner.GetSummonerByNameAsync(It.IsAny<RiotSharp.Misc.Region>(), It.IsAny<string>()))
                .Verifiable();

            var authFactorService = new LeagueOfLegendsAuthFactorGeneratorAdapter(mockLeagueOfLegendsService.Object, mockAuthFactorRepository.Object);

            // Act
            var result = await authFactorService.GenerateAuthFactorAsync(userId, new LeagueOfLegendsRequest("testSummoner"));

            // Assert
            result.Should().BeOfType<FluentValidation.Results.ValidationResult>();

            mockLeagueOfLegendsService.Verify(
                leagueService => leagueService.Summoner.GetSummonerByNameAsync(It.IsAny<RiotSharp.Misc.Region>(), It.IsAny<string>()),
                Times.Once);
        }
    }
}
