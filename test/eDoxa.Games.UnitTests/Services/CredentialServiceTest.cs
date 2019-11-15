// Filename: CredentialServiceTest.cs
// Date Created: 2019-11-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Games.Abstractions.Services;
using eDoxa.Games.Domain.AggregateModels;
using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Games.LeagueOfLegends;
using eDoxa.Games.Services;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

using Moq;

using Xunit;

namespace eDoxa.Games.UnitTests.Services
{
    public sealed class CredentialServiceTest : UnitTest
    {
        public CredentialServiceTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task LinkCredentialAsync_CredentialShouldNotExists()
        {
            // Arrange
            var userId = new UserId();

            var mockCredentialRepository = new Mock<ICredentialRepository>();
            var mockAuthFactorService = new Mock<IAuthenticationService>();

            mockCredentialRepository
                .Setup(repository => repository.CredentialExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(true)
                .Verifiable();

            var authFactorService = new CredentialService(mockCredentialRepository.Object, mockAuthFactorService.Object);

            // Act
            await authFactorService.LinkCredentialAsync(userId, Game.LeagueOfLegends);

            // Assert
            mockCredentialRepository.Verify(
                repository => repository.CredentialExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()),
                Times.Once);
        }

        [Fact]
        public async Task LinkCredentialAsync_AuthFactorShouldNotExists()
        {
            // Arrange
            var userId = new UserId();

            var mockCredentialRepository = new Mock<ICredentialRepository>();
            var mockAuthFactorService = new Mock<IAuthenticationService>();

            mockCredentialRepository
                .Setup(repository => repository.CredentialExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(false)
                .Verifiable();

            mockAuthFactorService
                .Setup(authFactor => authFactor.AuthenticationExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(false)
                .Verifiable();

            var authFactorService = new CredentialService(mockCredentialRepository.Object, mockAuthFactorService.Object);

            // Act
            await authFactorService.LinkCredentialAsync(userId, Game.LeagueOfLegends);

            // Assert
            mockCredentialRepository.Verify(
                repository => repository.CredentialExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()),
                Times.Once);

            mockAuthFactorService.Verify(
                authFactor => authFactor.AuthenticationExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()),
                Times.Once);
        }

        [Fact]
        public async Task LinkCredentialAsync()
        {
            // Arrange
            var userId = new UserId();

            var userAuthFactor = new GameAuthentication<LeagueOfLegendsGameAuthenticationFactor>(new PlayerId(), new LeagueOfLegendsGameAuthenticationFactor(1, string.Empty, 2, string.Empty));

            var mockCredentialRepository = new Mock<ICredentialRepository>();
            var mockAuthFactorService = new Mock<IAuthenticationService>();

            mockCredentialRepository
                .Setup(repository => repository.CredentialExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(false)
                .Verifiable();

            mockAuthFactorService
                .Setup(authFactor => authFactor.AuthenticationExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(true)
                .Verifiable();

            mockAuthFactorService
                .Setup(authFactor => authFactor.FindAuthenticationAsync<LeagueOfLegendsGameAuthenticationFactor>(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(userAuthFactor)
                .Verifiable();

            mockAuthFactorService
                .Setup(authFactor => authFactor.ValidateAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<GameAuthentication<LeagueOfLegendsGameAuthenticationFactor>>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            mockCredentialRepository
                .Setup(repository => repository.CreateCredential(It.IsAny<Credential>()))
                .Verifiable();

            mockCredentialRepository
                .Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var authFactorService = new CredentialService(mockCredentialRepository.Object, mockAuthFactorService.Object);

            // Act
            await authFactorService.LinkCredentialAsync(userId, Game.LeagueOfLegends);

            // Assert
            mockCredentialRepository.Verify(
                repository => repository.CredentialExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()),
                Times.Once);

            mockAuthFactorService.Verify(
                authFactor => authFactor.AuthenticationExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()),
                Times.Once);

            mockAuthFactorService.Verify(
                authFactor => authFactor.FindAuthenticationAsync<LeagueOfLegendsGameAuthenticationFactor>(It.IsAny<UserId>(), It.IsAny<Game>()),
                Times.Once);

            mockAuthFactorService.Verify(
                authFactor => authFactor.ValidateAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<GameAuthentication<LeagueOfLegendsGameAuthenticationFactor>>()),
                Times.Once);

            mockCredentialRepository.Verify(repository => repository.CreateCredential(It.IsAny<Credential>()), Times.Once);

            mockCredentialRepository.Verify(
                repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task UnlinkCredentialAsync()
        {
            // Arrange
            var userId = new UserId();

            var mockCredentialRepository = new Mock<ICredentialRepository>();
            var mockAuthFactorService = new Mock<IAuthenticationService>();

            var credential = new Credential(userId, Game.LeagueOfLegends, new PlayerId(), new UtcNowDateTimeProvider());

            mockCredentialRepository
                .Setup(repository => repository.DeleteCredential(It.IsAny<Credential>()))
                .Verifiable();

            mockCredentialRepository
                .Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var authFactorService = new CredentialService(mockCredentialRepository.Object, mockAuthFactorService.Object);

            // Act
            await authFactorService.UnlinkCredentialAsync(credential);

            // Assert
            mockCredentialRepository.Verify(
                repository => repository.DeleteCredential(It.IsAny<Credential>()),
                Times.Once);

            mockCredentialRepository.Verify(
                repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()),
                Times.Exactly(2));
        }

        [Fact]
        public async Task FetchCredentialsAsync()
        {
            // Arrange
            var userId = new UserId();

            var mockCredentialRepository = new Mock<ICredentialRepository>();
            var mockAuthFactorService = new Mock<IAuthenticationService>();

            var credentials = new List<Credential>() { new Credential(userId, Game.LeagueOfLegends, new PlayerId(), new UtcNowDateTimeProvider())};

            mockCredentialRepository
                .Setup(repository => repository.FetchCredentialsAsync(It.IsAny<UserId>()))
                .ReturnsAsync(credentials)
                .Verifiable();

            var authFactorService = new CredentialService(mockCredentialRepository.Object, mockAuthFactorService.Object);

            // Act
            await authFactorService.FetchCredentialsAsync(userId);

            // Assert
            mockCredentialRepository.Verify(
                repository => repository.FetchCredentialsAsync(It.IsAny<UserId>()),
                Times.Once);
        }

        [Fact]
        public async Task FindCredentialAsync()
        {
            // Arrange
            var userId = new UserId();

            var mockCredentialRepository = new Mock<ICredentialRepository>();
            var mockAuthFactorService = new Mock<IAuthenticationService>();

            var credential = new Credential(userId, Game.LeagueOfLegends, new PlayerId(), new UtcNowDateTimeProvider());

            mockCredentialRepository
                .Setup(repository => repository.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(credential)
                .Verifiable();

            var authFactorService = new CredentialService(mockCredentialRepository.Object, mockAuthFactorService.Object);

            // Act
            await authFactorService.FindCredentialAsync(userId, Game.LeagueOfLegends);

            // Assert
            mockCredentialRepository.Verify(
                repository => repository.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()),
                Times.Once);
        }

        [Fact]
        public async Task CredentialExistsAsync()
        {
            // Arrange
            var userId = new UserId();

            var mockCredentialRepository = new Mock<ICredentialRepository>();
            var mockAuthFactorService = new Mock<IAuthenticationService>();

            mockCredentialRepository
                .Setup(repository => repository.CredentialExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(false)
                .Verifiable();

            var authFactorService = new CredentialService(mockCredentialRepository.Object, mockAuthFactorService.Object);

            // Act
            await authFactorService.CredentialExistsAsync(userId, Game.LeagueOfLegends);

            // Assert
            mockCredentialRepository.Verify(
                repository => repository.CredentialExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()),
                Times.Once);
        }

        [Fact]
        public async Task FetchGamesWithCredentialAsync()
        {
            // Arrange
            var userId = new UserId();

            var mockCredentialRepository = new Mock<ICredentialRepository>();
            var mockAuthFactorService = new Mock<IAuthenticationService>();

            var credentials = new List<Credential>() { new Credential(userId, Game.LeagueOfLegends, new PlayerId(), new UtcNowDateTimeProvider())};

            mockCredentialRepository
                .Setup(repository => repository.FetchCredentialsAsync(It.IsAny<UserId>()))
                .ReturnsAsync(credentials)
                .Verifiable();

            var authFactorService = new CredentialService(mockCredentialRepository.Object, mockAuthFactorService.Object);

            // Act
            await authFactorService.FetchGamesWithCredentialAsync(userId);

            // Assert
            mockCredentialRepository.Verify(
                repository => repository.FetchCredentialsAsync(It.IsAny<UserId>()),
                Times.Once);
        }

    }
}
