// Filename: AccountDepositPostRequestTest.cs
// Date Created: 2019-09-16
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Clans.Api.Areas.Clans.Services;
using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.Domain.Repositories;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using FluentValidation.Results;

using Moq;

using Xunit;

namespace eDoxa.Clans.UnitTests.Areas.Clans.Services
{
    public sealed class CandidatureServiceTest : UnitTest
    {
        public CandidatureServiceTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task FetchCandidaturesAsync_WithClanId_ShouldBeOfTypeCandidatureList()
        {
            // Arrange
            var mockCandidatureRepository = new Mock<ICandidatureRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            var clanId = new ClanId();

            mockCandidatureRepository.Setup(repository => repository.FetchAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(new List<Candidature>()
                {
                    new Candidature(new UserId(), clanId),
                    new Candidature(new UserId(), clanId),
                    new Candidature(new UserId(), clanId)
                })
                .Verifiable();

            var service = new CandidatureService(mockCandidatureRepository.Object, mockClanRepository.Object);

            // Act
            var result = await service.FetchCandidaturesAsync(new ClanId());

            // Assert
            result.Should().BeOfType<List<Candidature>>();
            mockCandidatureRepository.Verify(repository => repository.FetchAsync(It.IsAny<ClanId>()), Times.Once);
        }


        [Fact]
        public async Task FetchCandidaturesAsync_WithUserId_ShouldBeOfTypeCandidatureList()
        {
            // Arrange
            var mockCandidatureRepository = new Mock<ICandidatureRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            var userId = new UserId();

            mockCandidatureRepository.Setup(repository => repository.FetchAsync(It.IsAny<UserId>()))
                .ReturnsAsync(new List<Candidature>()
                {
                    new Candidature(userId, new ClanId()),
                    new Candidature(userId, new ClanId()),
                    new Candidature(userId, new ClanId())
                })
                .Verifiable();

            var service = new CandidatureService(mockCandidatureRepository.Object, mockClanRepository.Object);

            // Act
            var result = await service.FetchCandidaturesAsync(new UserId());

            // Assert
            result.Should().BeOfType<List<Candidature>>();
            mockCandidatureRepository.Verify(repository => repository.FetchAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task FindCandidatureAsync_ShouldBeOfTypeCandidature()
        {
            // Arrange
            var mockCandidatureRepository = new Mock<ICandidatureRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            mockCandidatureRepository.Setup(repository => repository.FindAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync(new Candidature(new UserId(), new ClanId()))
                .Verifiable();

            var service = new CandidatureService(mockCandidatureRepository.Object, mockClanRepository.Object);

            // Act
            var result = await service.FindCandidatureAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<Candidature>();
            mockCandidatureRepository.Verify(repository => repository.FindAsync(It.IsAny<CandidatureId>()), Times.Once);
        }

        [Fact]
        public async Task SendCandidatureAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockCandidatureRepository = new Mock<ICandidatureRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            mockClanRepository.Setup(repository => repository.IsMemberAsync(It.IsAny<UserId>()))
                .ReturnsAsync(false)
                .Verifiable();

            mockCandidatureRepository.Setup(repository => repository.ExistsAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()))
                .ReturnsAsync(false)
                .Verifiable();

            mockCandidatureRepository.Setup(repository => repository.Create(It.IsAny<Candidature>()))
                .Verifiable();

            mockCandidatureRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new CandidatureService(mockCandidatureRepository.Object, mockClanRepository.Object);

            // Act
            var result = await service.SendCandidatureAsync(new UserId(), new ClanId());

            // Assert
            result.Should().BeOfType<ValidationResult>();
            mockClanRepository.Verify(repository => repository.IsMemberAsync(It.IsAny<UserId>()), Times.Once);
            mockCandidatureRepository.Verify(repository => repository.ExistsAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()), Times.Once);
            mockCandidatureRepository.Verify(repository => repository.Create(It.IsAny<Candidature>()), Times.Once);
            mockCandidatureRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task SendCandidatureAsync_WhenIsMember_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockCandidatureRepository = new Mock<ICandidatureRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            mockClanRepository.Setup(repository => repository.IsMemberAsync(It.IsAny<UserId>()))
                .ReturnsAsync(true)
                .Verifiable();

            var service = new CandidatureService(mockCandidatureRepository.Object, mockClanRepository.Object);

            // Act
            var result = await service.SendCandidatureAsync(new UserId(), new ClanId());

            // Assert
            result.Should().BeOfType<ValidationResult>();
            result.Errors.Should().NotBeEmpty();
            mockClanRepository.Verify(repository => repository.IsMemberAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task SendCandidatureAsync_WhenCandidatureExist_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockCandidatureRepository = new Mock<ICandidatureRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            mockClanRepository.Setup(repository => repository.IsMemberAsync(It.IsAny<UserId>()))
                .ReturnsAsync(false)
                .Verifiable();

            mockCandidatureRepository.Setup(repository => repository.ExistsAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()))
                .ReturnsAsync(true)
                .Verifiable();

            var service = new CandidatureService(mockCandidatureRepository.Object, mockClanRepository.Object);

            // Act
            var result = await service.SendCandidatureAsync(new UserId(), new ClanId());

            // Assert
            result.Should().BeOfType<ValidationResult>();
            result.Errors.Should().NotBeEmpty();
            mockClanRepository.Verify(repository => repository.IsMemberAsync(It.IsAny<UserId>()), Times.Once);
            mockCandidatureRepository.Verify(repository => repository.ExistsAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task AcceptCandidatureAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockCandidatureRepository = new Mock<ICandidatureRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            var candidature = new Candidature(new UserId(), new ClanId());

            mockClanRepository.Setup(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()))
                .ReturnsAsync(true)
                .Verifiable();

            mockCandidatureRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new CandidatureService(mockCandidatureRepository.Object, mockClanRepository.Object);

            // Act
            var result = await service.AcceptCandidatureAsync(candidature, new UserId());

            // Assert
            result.Should().BeOfType<ValidationResult>();
            mockClanRepository.Verify(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()), Times.Once);
            mockCandidatureRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AcceptCandidatureAsync_WhenNotOwner_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockCandidatureRepository = new Mock<ICandidatureRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            var candidature = new Candidature(new UserId(), new ClanId());

            mockClanRepository.Setup(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()))
                .ReturnsAsync(false)
                .Verifiable();

            var service = new CandidatureService(mockCandidatureRepository.Object, mockClanRepository.Object);

            // Act
            var result = await service.AcceptCandidatureAsync(candidature, new UserId());

            // Assert
            result.Should().BeOfType<ValidationResult>();
            result.Errors.Should().NotBeEmpty();
            mockClanRepository.Verify(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task DeclineCandidatureAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockCandidatureRepository = new Mock<ICandidatureRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            var candidature = new Candidature(new UserId(), new ClanId());

            mockClanRepository.Setup(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()))
                .ReturnsAsync(true)
                .Verifiable();

            mockCandidatureRepository.Setup(repository => repository.Delete(It.IsAny<Candidature>()))
                .Verifiable();

            mockCandidatureRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new CandidatureService(mockCandidatureRepository.Object, mockClanRepository.Object);

            // Act
            var result = await service.DeclineCandidatureAsync(candidature, new UserId());

            // Assert
            result.Should().BeOfType<ValidationResult>();
            mockClanRepository.Verify(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()), Times.Once);
            mockCandidatureRepository.Verify(repository => repository.Delete(It.IsAny<Candidature>()), Times.Once);
            mockCandidatureRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeclineCandidatureAsync_WhenNotOwner_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockCandidatureRepository = new Mock<ICandidatureRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            var candidature = new Candidature(new UserId(), new ClanId());

            mockClanRepository.Setup(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()))
                .ReturnsAsync(false)
                .Verifiable();

            var service = new CandidatureService(mockCandidatureRepository.Object, mockClanRepository.Object);

            // Act
            var result = await service.DeclineCandidatureAsync(candidature, new UserId());

            // Assert
            result.Should().BeOfType<ValidationResult>();
            result.Errors.Should().NotBeEmpty();
            mockClanRepository.Verify(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task DeleteCandidaturesAsync_WithUserId()
        {
            // Arrange
            var mockCandidatureRepository = new Mock<ICandidatureRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            var userId = new UserId();

            mockCandidatureRepository.Setup(repository => repository.FetchAsync(It.IsAny<UserId>()))
                .ReturnsAsync(new List<Candidature>()
                {
                    new Candidature(userId, new ClanId()),
                    new Candidature(userId, new ClanId()),
                    new Candidature(userId, new ClanId())
                })
                .Verifiable();

            mockCandidatureRepository.Setup(repository => repository.Delete(It.IsAny<Candidature>()))
                .Verifiable();

            mockCandidatureRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new CandidatureService(mockCandidatureRepository.Object, mockClanRepository.Object);

            // Act
            await service.DeleteCandidaturesAsync(new UserId());

            // Assert
            mockCandidatureRepository.Verify(repository => repository.FetchAsync(It.IsAny<UserId>()), Times.Once);
            mockCandidatureRepository.Verify(repository => repository.Delete(It.IsAny<Candidature>()), Times.Exactly(3));
            mockCandidatureRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteCandidaturesAsync_WithClanId()
        {
            // Arrange
            var mockCandidatureRepository = new Mock<ICandidatureRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            var clanId = new ClanId();

            mockCandidatureRepository.Setup(repository => repository.FetchAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(new List<Candidature>()
                {
                    new Candidature(new UserId(), clanId),
                    new Candidature(new UserId(), clanId),
                    new Candidature(new UserId(), clanId)
                })
                .Verifiable();

            mockCandidatureRepository.Setup(repository => repository.Delete(It.IsAny<Candidature>()))
                .Verifiable();

            mockCandidatureRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new CandidatureService(mockCandidatureRepository.Object, mockClanRepository.Object);

            // Act
            await service.DeleteCandidaturesAsync(new ClanId());

            // Assert
            mockCandidatureRepository.Verify(repository => repository.FetchAsync(It.IsAny<ClanId>()), Times.Once);
            mockCandidatureRepository.Verify(repository => repository.Delete(It.IsAny<Candidature>()), Times.Exactly(3));
            mockCandidatureRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}
