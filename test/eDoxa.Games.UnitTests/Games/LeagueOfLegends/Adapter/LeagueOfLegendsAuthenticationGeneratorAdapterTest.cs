// Filename: LeagueOfLegendsAuthFactorGeneratorAdapterTest.cs
// Date Created: 2019-11-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Games.LeagueOfLegends;
using eDoxa.Games.LeagueOfLegends.Abstactions;
using eDoxa.Games.LeagueOfLegends.Adapter;
using eDoxa.Games.LeagueOfLegends.Requests;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using FluentValidation.Results;

using Microsoft.Azure.Storage;

using Moq;

using RiotSharp.Endpoints.SummonerEndpoint;
using RiotSharp.Misc;

using Xunit;

namespace eDoxa.Games.UnitTests.Games.LeagueOfLegends.Adapter
{
    public sealed class LeagueOfLegendsAuthenticationGeneratorAdapterTest : UnitTest // Francis: How to test the private static method ?
    {
        public LeagueOfLegendsAuthenticationGeneratorAdapterTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact(Skip = "CloudStorageAccount must implement an interface to mock and test.")]
        public async Task GenerateAuthFactorAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var userId = new UserId();

            var mockAuthFactorRepository = new Mock<IGameAuthenticationRepository>();
            var mockLeagueOfLegendsService = new Mock<ILeagueOfLegendsService>();
            var mockCloudStorageAccount = new Mock<CloudStorageAccount>();

            var summoner = new Summoner()
            {
                ProfileIconId = 0,
                Name = "testName",
                Region = Region.Na,
                AccountId = "testId"
            };

            mockLeagueOfLegendsService
                .Setup(leagueService => leagueService.Summoner.GetSummonerByNameAsync(It.IsAny<Region>(), It.IsAny<string>()))
                .ReturnsAsync(summoner)
                .Verifiable();

            mockAuthFactorRepository
                .Setup(repository => repository.AuthenticationExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(true)
                .Verifiable();

            mockAuthFactorRepository
                .Setup(repository => repository.RemoveAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockAuthFactorRepository
                .Setup(repository => repository.AddAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<GameAuthentication<LeagueOfLegendsGameAuthenticationFactor>>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var authFactorService = new LeagueOfLegendsAuthenticationGeneratorAdapter(mockLeagueOfLegendsService.Object, mockAuthFactorRepository.Object, mockCloudStorageAccount.Object);

            // Act
            var result = await authFactorService.GenerateAuthenticationAsync(userId, new LeagueOfLegendsRequest("testSummoner"));

            // Assert
            result.Should().BeOfType<ValidationResult>();

            mockLeagueOfLegendsService.Verify(
                leagueService => leagueService.Summoner.GetSummonerByNameAsync(It.IsAny<Region>(), It.IsAny<string>()),
                Times.Once);

            mockAuthFactorRepository.Verify(
                repository => repository.AuthenticationExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()),
                Times.Once);

            mockAuthFactorRepository.Verify(
                repository => repository.RemoveAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>()),
                Times.Once);

            mockAuthFactorRepository.Verify(
                repository => repository.AddAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<GameAuthentication<LeagueOfLegendsGameAuthenticationFactor>>()),
                Times.Once);
        }

        [Fact(Skip = "CloudStorageAccount must implement an interface to mock and test.")]
        public async Task GenerateAuthFactorAsync_ShouldBeOfTypeValidationFailureResult()
        {
            // Arrange
            var userId = new UserId();

            var mockAuthFactorRepository = new Mock<IGameAuthenticationRepository>();
            var mockLeagueOfLegendsService = new Mock<ILeagueOfLegendsService>();
            var mockCloudStorageAccount = new Mock<CloudStorageAccount>();

            mockLeagueOfLegendsService
                .Setup(leagueService => leagueService.Summoner.GetSummonerByNameAsync(It.IsAny<Region>(), It.IsAny<string>()))
                .Verifiable();

            var authFactorService = new LeagueOfLegendsAuthenticationGeneratorAdapter(mockLeagueOfLegendsService.Object, mockAuthFactorRepository.Object, mockCloudStorageAccount.Object);

            // Act
            var result = await authFactorService.GenerateAuthenticationAsync(userId, new LeagueOfLegendsRequest("testSummoner"));

            // Assert
            result.Should().BeOfType<ValidationResult>();

            mockLeagueOfLegendsService.Verify(
                leagueService => leagueService.Summoner.GetSummonerByNameAsync(It.IsAny<Region>(), It.IsAny<string>()),
                Times.Once);
        }
    }
}
