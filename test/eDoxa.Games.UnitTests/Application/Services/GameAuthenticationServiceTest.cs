// Filename: GameAuthenticationServiceTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Api.Application.Services;
using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.LeagueOfLegends;
using eDoxa.Games.LeagueOfLegends.Adapter;
using eDoxa.Games.LeagueOfLegends.Requests;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using Moq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using RiotSharp.Endpoints.SummonerEndpoint;
using RiotSharp.Misc;

using Xunit;

namespace eDoxa.Games.UnitTests.Application.Services
{
    public sealed class GameAuthenticationServiceTest : UnitTest
    {
        public GameAuthenticationServiceTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task AuthFactorExistsAsync()
        {
            // Arrange
            var userId = new UserId();

            TestMock.GameAuthenticationRepository.Setup(repository => repository.AuthenticationExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(true)
                .Verifiable();

            var authFactorService = new GameAuthenticationService(
                TestMock.GameAuthenticationGeneratorFactory.Object,
                TestMock.GameAuthenticationValidatorFactory.Object,
                TestMock.GameAuthenticationRepository.Object);

            // Act
            await authFactorService.AuthenticationExistsAsync(userId, Game.LeagueOfLegends);

            // Assert
            TestMock.GameAuthenticationRepository.Verify(repository => repository.AuthenticationExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public async Task FindAuthFactorAsync()
        {
            // Arrange
            var userId = new UserId();

            var authFactor = new GameAuthentication<LeagueOfLegendsGameAuthenticationFactor>(
                new PlayerId(),
                new LeagueOfLegendsGameAuthenticationFactor(
                    1,
                    string.Empty,
                    2,
                    string.Empty));

            TestMock.GameAuthenticationRepository
                .Setup(repository => repository.GetAuthenticationAsync<LeagueOfLegendsGameAuthenticationFactor>(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(authFactor)
                .Verifiable();

            var authFactorService = new GameAuthenticationService(
                TestMock.GameAuthenticationGeneratorFactory.Object,
                TestMock.GameAuthenticationValidatorFactory.Object,
                TestMock.GameAuthenticationRepository.Object);

            // Act
            await authFactorService.FindAuthenticationAsync<LeagueOfLegendsGameAuthenticationFactor>(userId, Game.LeagueOfLegends);

            // Assert
            TestMock.GameAuthenticationRepository.Verify(
                repository => repository.GetAuthenticationAsync<LeagueOfLegendsGameAuthenticationFactor>(It.IsAny<UserId>(), It.IsAny<Game>()),
                Times.Once);
        }

        [Fact]
        public async Task GenerateAuthFactorAsync()
        {
            // Arrange
            var userId = new UserId();

            var game = Game.LeagueOfLegends;

            TestMock.AuthenticationGeneratorAdapter.SetupGet(authenticationGeneratorAdapter => authenticationGeneratorAdapter.Game).Returns(game);

            TestMock.GameAuthenticationGeneratorFactory.Setup(generator => generator.CreateInstance(It.IsAny<Game>()))
                .Returns(TestMock.AuthenticationGeneratorAdapter.Object)
                .Verifiable();

            var authFactorService = new GameAuthenticationService(
                TestMock.GameAuthenticationGeneratorFactory.Object,
                TestMock.GameAuthenticationValidatorFactory.Object,
                TestMock.GameAuthenticationRepository.Object);

            // Act
            await authFactorService.GenerateAuthenticationAsync(
                userId,
                Game.LeagueOfLegends,
                JObject.Parse(JsonConvert.SerializeObject(new LeagueOfLegendsRequest("Test"))));

            // Assert
            TestMock.GameAuthenticationGeneratorFactory.Verify(generator => generator.CreateInstance(It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public async Task ValidateAuthFactorAsync()
        {
            // Arrange
            var authFactor = new GameAuthentication<LeagueOfLegendsGameAuthenticationFactor>(
                new PlayerId(),
                new LeagueOfLegendsGameAuthenticationFactor(
                    1,
                    string.Empty,
                    2,
                    string.Empty));

            var summoner = new Summoner
            {
                ProfileIconId = 1
            };

            TestMock.LeagueOfLegendsService.Setup(x => x.Summoner.GetSummonerByAccountIdAsync(It.IsAny<Region>(), It.IsAny<string>()))
                .ReturnsAsync(summoner)
                .Verifiable();

            var leagueAdapter = new LeagueOfLegendsAuthenticationValidatorAdapter(
                TestMock.LeagueOfLegendsService.Object,
                TestMock.GameAuthenticationRepository.Object);

            TestMock.GameAuthenticationValidatorFactory.Setup(validator => validator.CreateInstance(It.IsAny<Game>())).Returns(leagueAdapter).Verifiable();

            var authFactorService = new GameAuthenticationService(
                TestMock.GameAuthenticationGeneratorFactory.Object,
                TestMock.GameAuthenticationValidatorFactory.Object,
                TestMock.GameAuthenticationRepository.Object);

            // Act
            await authFactorService.ValidateAuthenticationAsync(new UserId(), Game.LeagueOfLegends, authFactor);

            // Assert
            TestMock.GameAuthenticationValidatorFactory.Verify(validator => validator.CreateInstance(It.IsAny<Game>()), Times.Once);
        }
    }
}
