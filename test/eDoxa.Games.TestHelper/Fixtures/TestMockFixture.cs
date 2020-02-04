// Filename: TestMockFixture.cs
// Date Created: 2020-02-02
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Games.Domain.Adapters;
using eDoxa.Games.Domain.Factories;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Games.Domain.Services;
using eDoxa.Games.LeagueOfLegends.Abstactions;
using eDoxa.ServiceBus.Abstractions;

using Moq;

namespace eDoxa.Games.TestHelper.Fixtures
{
    public sealed class TestMockFixture
    {
        public TestMockFixture()
        {
            ServiceBusPublisher = new Mock<IServiceBusPublisher>();
            LeagueOfLegendsService = new Mock<ILeagueOfLegendsService>();
            AuthenticationGeneratorAdapter = new Mock<IAuthenticationGeneratorAdapter>();
            AuthenticationValidatorAdapter = new Mock<IAuthenticationValidatorAdapter>();
            ChallengeMatchesAdapter = new Mock<IChallengeMatchesAdapter>();
            ChallengeMatchesFactory = new Mock<IChallengeMatchesFactory>();
            GameAuthenticationGeneratorFactory = new Mock<IGameAuthenticationGeneratorFactory>();
            GameAuthenticationValidatorFactory = new Mock<IGameAuthenticationValidatorFactory>();
            GameAuthenticationRepository = new Mock<IGameAuthenticationRepository>();
            GameCredentialRepository = new Mock<IGameCredentialRepository>();
            ChallengeService = new Mock<IChallengeService>();
            GameAuthenticationService = new Mock<IGameAuthenticationService>();
            GameCredentialService = new Mock<IGameCredentialService>();
        }

        public Mock<IServiceBusPublisher> ServiceBusPublisher { get; }

        public Mock<ILeagueOfLegendsService> LeagueOfLegendsService { get; }
        
        public Mock<IAuthenticationGeneratorAdapter> AuthenticationGeneratorAdapter { get; }

        public Mock<IAuthenticationValidatorAdapter> AuthenticationValidatorAdapter { get; }

        public Mock<IChallengeMatchesAdapter> ChallengeMatchesAdapter { get; }

        public Mock<IChallengeMatchesFactory> ChallengeMatchesFactory { get; }

        public Mock<IGameAuthenticationGeneratorFactory> GameAuthenticationGeneratorFactory { get; }

        public Mock<IGameAuthenticationValidatorFactory> GameAuthenticationValidatorFactory { get; }

        public Mock<IGameAuthenticationRepository> GameAuthenticationRepository { get; }

        public Mock<IGameCredentialRepository> GameCredentialRepository { get; }

        public Mock<IChallengeService> ChallengeService { get; }

        public Mock<IGameAuthenticationService> GameAuthenticationService { get; }

        public Mock<IGameCredentialService> GameCredentialService { get; }
    }
}
