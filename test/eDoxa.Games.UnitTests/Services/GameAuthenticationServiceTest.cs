// Filename: AuthFactorServiceTest.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Abstractions.Adapters;
using eDoxa.Games.Abstractions.Factories;
using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Games.LeagueOfLegends;
using eDoxa.Games.LeagueOfLegends.Abstactions;
using eDoxa.Games.LeagueOfLegends.Adapter;
using eDoxa.Games.LeagueOfLegends.Requests;
using eDoxa.Games.Services;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using Moq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using RiotSharp.Endpoints.SummonerEndpoint;
using RiotSharp.Misc;

using Xunit;

namespace eDoxa.Games.UnitTests.Services
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

            var mockAuthFactorGenerator = new Mock<IGameAuthenticationGeneratorFactory>();
            var mockAuthFactorValidator = new Mock<IGameAuthenticationValidatorFactory>();
            var mockAuthFactorRepository = new Mock<IGameAuthenticationRepository>();

            mockAuthFactorRepository.Setup(repository => repository.AuthenticationExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(true)
                .Verifiable();

            var authFactorService = new GameAuthenticationService(mockAuthFactorGenerator.Object, mockAuthFactorValidator.Object, mockAuthFactorRepository.Object);

            // Act
            await authFactorService.AuthenticationExistsAsync(userId, Game.LeagueOfLegends);

            // Assert
            mockAuthFactorRepository.Verify(repository => repository.AuthenticationExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public async Task FindAuthFactorAsync()
        {
            // Arrange
            var userId = new UserId();

            var authFactor = new GameAuthentication<LeagueOfLegendsGameAuthenticationFactor>(new PlayerId(), new LeagueOfLegendsGameAuthenticationFactor(1, string.Empty, 2, string.Empty));

            var mockAuthFactorGenerator = new Mock<IGameAuthenticationGeneratorFactory>();
            var mockAuthFactorValidator = new Mock<IGameAuthenticationValidatorFactory>();
            var mockAuthFactorRepository = new Mock<IGameAuthenticationRepository>();

            mockAuthFactorRepository.Setup(repository => repository.GetAuthenticationAsync<LeagueOfLegendsGameAuthenticationFactor>(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(authFactor)
                .Verifiable();

            var authFactorService = new GameAuthenticationService(mockAuthFactorGenerator.Object, mockAuthFactorValidator.Object, mockAuthFactorRepository.Object);

            // Act
            await authFactorService.FindAuthenticationAsync<LeagueOfLegendsGameAuthenticationFactor>(userId, Game.LeagueOfLegends);

            // Assert
            mockAuthFactorRepository.Verify(repository => repository.GetAuthenticationAsync<LeagueOfLegendsGameAuthenticationFactor>(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public async Task GenerateAuthFactorAsync()
        {
            // Arrange
            var userId = new UserId();
            
            var mockAuthFactorValidator = new Mock<IGameAuthenticationValidatorFactory>();
            var mockAuthFactorRepository = new Mock<IGameAuthenticationRepository>();
            var mockLeagueOfLegendsService = new Mock<ILeagueOfLegendsService>();

            var summonner = new Summoner
            {
                AccountId = "Nqwe1231eqwe123__!3212213",
                ProfileIconId = 1
            };

            mockLeagueOfLegendsService.Setup(x => x.Summoner.GetSummonerByNameAsync(It.IsAny<Region>(), It.IsAny<string>()))
                .ReturnsAsync(summonner)
                .Verifiable();

            var game = Game.LeagueOfLegends;

            var mockAuthenticationGeneratorAdapter = new Mock<IAuthenticationGeneratorAdapter>();

            mockAuthenticationGeneratorAdapter.SetupGet(authenticationGeneratorAdapter => authenticationGeneratorAdapter.Game).Returns(game);
            
            var mockAuthenticationGeneratorFactory = new Mock<IGameAuthenticationGeneratorFactory>();

            mockAuthenticationGeneratorFactory.Setup(generator => generator.CreateInstance(It.IsAny<Game>())).Returns(mockAuthenticationGeneratorAdapter.Object).Verifiable();

            var authFactorService = new GameAuthenticationService(mockAuthenticationGeneratorFactory.Object, mockAuthFactorValidator.Object, mockAuthFactorRepository.Object);

            // Act
            await authFactorService.GenerateAuthenticationAsync(
                userId,
                Game.LeagueOfLegends,
                JObject.Parse(JsonConvert.SerializeObject(new LeagueOfLegendsRequest("Test"))));

            // Assert
            mockAuthenticationGeneratorFactory.Verify(generator => generator.CreateInstance(It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public async Task ValidateAuthFactorAsync()
        {
            // Arrange
            var authFactor = new GameAuthentication<LeagueOfLegendsGameAuthenticationFactor>(new PlayerId(), new LeagueOfLegendsGameAuthenticationFactor(1, string.Empty, 2, string.Empty));
            var mockAuthFactorGenerator = new Mock<IGameAuthenticationGeneratorFactory>();
            var mockAuthFactorValidator = new Mock<IGameAuthenticationValidatorFactory>();
            var mockAuthFactorRepository = new Mock<IGameAuthenticationRepository>();
            var mockLeagueOfLegendsService = new Mock<ILeagueOfLegendsService>();

            var summoner = new Summoner
            {
                ProfileIconId = 1
            };

            mockLeagueOfLegendsService.Setup(x => x.Summoner.GetSummonerByAccountIdAsync(It.IsAny<Region>(), It.IsAny<string>()))
                .ReturnsAsync(summoner)
                .Verifiable();

            var leagueAdapter = new LeagueOfLegendsAuthenticationValidatorAdapter(mockLeagueOfLegendsService.Object, mockAuthFactorRepository.Object);

            mockAuthFactorValidator.Setup(validator => validator.CreateInstance(It.IsAny<Game>())).Returns(leagueAdapter).Verifiable();

            var authFactorService = new GameAuthenticationService(mockAuthFactorGenerator.Object, mockAuthFactorValidator.Object, mockAuthFactorRepository.Object);

            // Act
            await authFactorService.ValidateAuthenticationAsync(new UserId(), Game.LeagueOfLegends, authFactor);

            // Assert
            mockAuthFactorValidator.Verify(validator => validator.CreateInstance(It.IsAny<Game>()), Times.Once);
        }
    }
}
