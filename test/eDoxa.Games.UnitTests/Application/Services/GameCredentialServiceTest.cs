// Filename: GameCredentialServiceTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Games.Api.Application.Services;
using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.LeagueOfLegends;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using Moq;

using Xunit;

namespace eDoxa.Games.UnitTests.Application.Services
{
    public sealed class GameCredentialServiceTest : UnitTest
    {
        public GameCredentialServiceTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task CredentialExistsAsync()
        {
            // Arrange
            var userId = new UserId();

            TestMock.GameCredentialRepository.Setup(repository => repository.CredentialExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(false)
                .Verifiable();

            var authFactorService = new GameCredentialService(TestMock.GameCredentialRepository.Object, TestMock.GameAuthenticationService.Object);

            // Act
            await authFactorService.CredentialExistsAsync(userId, Game.LeagueOfLegends);

            // Assert
            TestMock.GameCredentialRepository.Verify(repository => repository.CredentialExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public async Task FetchCredentialsAsync()
        {
            // Arrange
            var userId = new UserId();

            var credentials = new List<Credential>
            {
                new Credential(
                    userId,
                    Game.LeagueOfLegends,
                    new PlayerId(),
                    new UtcNowDateTimeProvider())
            };

            TestMock.GameCredentialRepository.Setup(repository => repository.FetchCredentialsAsync(It.IsAny<UserId>())).ReturnsAsync(credentials).Verifiable();

            var authFactorService = new GameCredentialService(TestMock.GameCredentialRepository.Object, TestMock.GameAuthenticationService.Object);

            // Act
            await authFactorService.FetchCredentialsAsync(userId);

            // Assert
            TestMock.GameCredentialRepository.Verify(repository => repository.FetchCredentialsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task FindCredentialAsync()
        {
            // Arrange
            var userId = new UserId();

            var credential = new Credential(
                userId,
                Game.LeagueOfLegends,
                new PlayerId(),
                new UtcNowDateTimeProvider());

            TestMock.GameCredentialRepository.Setup(repository => repository.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(credential)
                .Verifiable();

            var authFactorService = new GameCredentialService(TestMock.GameCredentialRepository.Object, TestMock.GameAuthenticationService.Object);

            // Act
            await authFactorService.FindCredentialAsync(userId, Game.LeagueOfLegends);

            // Assert
            TestMock.GameCredentialRepository.Verify(repository => repository.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public async Task LinkCredentialAsync()
        {
            // Arrange
            var userId = new UserId();

            var userAuthFactor = new GameAuthentication<LeagueOfLegendsGameAuthenticationFactor>(
                new PlayerId(),
                new LeagueOfLegendsGameAuthenticationFactor(
                    1,
                    string.Empty,
                    2,
                    string.Empty));

            TestMock.GameCredentialRepository.Setup(repository => repository.CredentialExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(false)
                .Verifiable();

            TestMock.GameAuthenticationService.Setup(authFactor => authFactor.AuthenticationExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(true)
                .Verifiable();

            TestMock.GameAuthenticationService.Setup(authFactor => authFactor.FindAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(userAuthFactor)
                .Verifiable();

            TestMock.GameAuthenticationService
                .Setup(
                    authFactor => authFactor.ValidateAuthenticationAsync(
                        It.IsAny<UserId>(),
                        It.IsAny<Game>(),
                        It.IsAny<GameAuthentication<LeagueOfLegendsGameAuthenticationFactor>>()))
                .ReturnsAsync(new DomainValidationResult<GameAuthentication>())
                .Verifiable();

            TestMock.GameCredentialRepository.Setup(repository => repository.CreateCredential(It.IsAny<Credential>())).Verifiable();

            TestMock.GameCredentialRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var authFactorService = new GameCredentialService(TestMock.GameCredentialRepository.Object, TestMock.GameAuthenticationService.Object);

            // Act
            await authFactorService.LinkCredentialAsync(userId, Game.LeagueOfLegends);

            // Assert
            TestMock.GameCredentialRepository.Verify(repository => repository.CredentialExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);

            TestMock.GameAuthenticationService.Verify(authFactor => authFactor.AuthenticationExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);

            TestMock.GameAuthenticationService.Verify(authFactor => authFactor.FindAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);

            TestMock.GameAuthenticationService.Verify(
                authFactor => authFactor.ValidateAuthenticationAsync(
                    It.IsAny<UserId>(),
                    It.IsAny<Game>(),
                    It.IsAny<GameAuthentication<LeagueOfLegendsGameAuthenticationFactor>>()),
                Times.Once);

            TestMock.GameCredentialRepository.Verify(repository => repository.CreateCredential(It.IsAny<Credential>()), Times.Once);

            TestMock.GameCredentialRepository.Verify(
                repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task LinkCredentialAsync_AuthFactorShouldNotExists()
        {
            // Arrange
            var userId = new UserId();

            TestMock.GameCredentialRepository.Setup(repository => repository.CredentialExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(false)
                .Verifiable();

            TestMock.GameAuthenticationService.Setup(authFactor => authFactor.AuthenticationExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(false)
                .Verifiable();

            var authFactorService = new GameCredentialService(TestMock.GameCredentialRepository.Object, TestMock.GameAuthenticationService.Object);

            // Act
            await authFactorService.LinkCredentialAsync(userId, Game.LeagueOfLegends);

            // Assert
            TestMock.GameCredentialRepository.Verify(repository => repository.CredentialExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);

            TestMock.GameAuthenticationService.Verify(authFactor => authFactor.AuthenticationExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public async Task LinkCredentialAsync_CredentialShouldNotExists()
        {
            // Arrange
            var userId = new UserId();

            TestMock.GameCredentialRepository.Setup(repository => repository.CredentialExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(true)
                .Verifiable();

            var authFactorService = new GameCredentialService(TestMock.GameCredentialRepository.Object, TestMock.GameAuthenticationService.Object);

            // Act
            await authFactorService.LinkCredentialAsync(userId, Game.LeagueOfLegends);

            // Assert
            TestMock.GameCredentialRepository.Verify(repository => repository.CredentialExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public async Task UnlinkCredentialAsync()
        {
            // Arrange
            var userId = new UserId();

            var credential = new Credential(
                userId,
                Game.LeagueOfLegends,
                new PlayerId(),
                new DateTimeProvider(DateTime.UtcNow.AddMonths(-2)));

            TestMock.GameCredentialRepository.Setup(repository => repository.DeleteCredential(It.IsAny<Credential>())).Verifiable();

            TestMock.GameCredentialRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var authFactorService = new GameCredentialService(TestMock.GameCredentialRepository.Object, TestMock.GameAuthenticationService.Object);

            // Act
            await authFactorService.UnlinkCredentialAsync(credential);

            // Assert
            TestMock.GameCredentialRepository.Verify(repository => repository.DeleteCredential(It.IsAny<Credential>()), Times.Once);

            TestMock.GameCredentialRepository.Verify(
                repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()),
                Times.Exactly(2));
        }
    }
}
