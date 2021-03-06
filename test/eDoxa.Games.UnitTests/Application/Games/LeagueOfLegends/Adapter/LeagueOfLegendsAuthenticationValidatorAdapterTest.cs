﻿// Filename: LeagueOfLegendsAuthenticationValidatorAdapterTest.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.LeagueOfLegends;
using eDoxa.Games.LeagueOfLegends.Adapter;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Moq;

using RiotSharp.Endpoints.SummonerEndpoint;
using RiotSharp.Misc;

using Xunit;

namespace eDoxa.Games.UnitTests.Application.Games.LeagueOfLegends.Adapter
{
    public sealed class LeagueOfLegendsAuthenticationValidatorAdapterTest : UnitTest
    {
        public LeagueOfLegendsAuthenticationValidatorAdapterTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task ValidateAuthFactorAsync_ShouldBeOfTypeValidationFailureResult()
        {
            // Arrange
            var userId = new UserId();

            var summoner = new Summoner
            {
                ProfileIconId = 0,
                Name = "testName",
                Region = Region.Na,
                AccountId = "testId"
            };

            TestMock.LeagueOfLegendsService.Setup(leagueService => leagueService.Summoner.GetSummonerByAccountIdAsync(It.IsAny<Region>(), It.IsAny<string>()))
                .ReturnsAsync(summoner)
                .Verifiable();

            TestMock.GameAuthenticationRepository.Setup(repository => repository.RemoveAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var authFactorService = new LeagueOfLegendsAuthenticationValidatorAdapter(
                TestMock.LeagueOfLegendsService.Object,
                TestMock.GameAuthenticationRepository.Object);

            // Act
            var result = await authFactorService.ValidateAuthenticationAsync(
                userId,
                new GameAuthentication<LeagueOfLegendsGameAuthenticationFactor>(
                    new PlayerId(),
                    new LeagueOfLegendsGameAuthenticationFactor(
                        1,
                        string.Empty,
                        2,
                        string.Empty)));

            // Assert
            result.Should().BeOfType<DomainValidationResult<GameAuthentication>>();

            TestMock.LeagueOfLegendsService.Verify(
                leagueService => leagueService.Summoner.GetSummonerByAccountIdAsync(It.IsAny<Region>(), It.IsAny<string>()),
                Times.Once);

            TestMock.GameAuthenticationRepository.Verify(repository => repository.RemoveAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public async Task ValidateAuthFactorAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var userId = new UserId();

            var summoner = new Summoner
            {
                ProfileIconId = 0,
                Name = "testName",
                Region = Region.Na,
                AccountId = "testId"
            };

            TestMock.LeagueOfLegendsService.Setup(leagueService => leagueService.Summoner.GetSummonerByAccountIdAsync(It.IsAny<Region>(), It.IsAny<string>()))
                .ReturnsAsync(summoner)
                .Verifiable();

            TestMock.GameAuthenticationRepository.Setup(repository => repository.RemoveAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var authFactorService = new LeagueOfLegendsAuthenticationValidatorAdapter(
                TestMock.LeagueOfLegendsService.Object,
                TestMock.GameAuthenticationRepository.Object);

            // Act
            var result = await authFactorService.ValidateAuthenticationAsync(
                userId,
                new GameAuthentication<LeagueOfLegendsGameAuthenticationFactor>(
                    new PlayerId(),
                    new LeagueOfLegendsGameAuthenticationFactor(
                        1,
                        string.Empty,
                        2,
                        string.Empty)));

            // Assert
            result.Should().BeOfType<DomainValidationResult<GameAuthentication>>();

            TestMock.LeagueOfLegendsService.Verify(
                leagueService => leagueService.Summoner.GetSummonerByAccountIdAsync(It.IsAny<Region>(), It.IsAny<string>()),
                Times.Once);

            TestMock.GameAuthenticationRepository.Verify(repository => repository.RemoveAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);
        }
    }
}
