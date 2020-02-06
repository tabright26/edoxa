// Filename: CandidatureServiceTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Clans.Api.Application.Services;
using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Moq;

using Xunit;

namespace eDoxa.Clans.UnitTests.Application.Services
{
    public sealed class CandidatureServiceTest : UnitTest
    {
        public CandidatureServiceTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task AcceptCandidatureAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var candidature = new Candidature(new UserId(), new ClanId());

            TestMock.ClanRepository.Setup(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            TestMock.CandidatureRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new CandidatureService(TestMock.CandidatureRepository.Object, TestMock.ClanRepository.Object);

            // Act
            var result = await service.AcceptCandidatureAsync(candidature, new UserId());

            // Assert
            result.Should().BeOfType<DomainValidationResult<Candidature>>();

            TestMock.ClanRepository.Verify(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()), Times.Once);

            TestMock.CandidatureRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AcceptCandidatureAsync_WhenNotOwner_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var candidature = new Candidature(new UserId(), new ClanId());

            TestMock.ClanRepository.Setup(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            var service = new CandidatureService(TestMock.CandidatureRepository.Object, TestMock.ClanRepository.Object);

            // Act
            var result = await service.AcceptCandidatureAsync(candidature, new UserId());

            // Assert
            result.Should().BeOfType<DomainValidationResult<Candidature>>();

            result.Errors.Should().NotBeEmpty();

            TestMock.ClanRepository.Verify(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task DeclineCandidatureAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var candidature = new Candidature(new UserId(), new ClanId());

            TestMock.ClanRepository.Setup(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            TestMock.CandidatureRepository.Setup(repository => repository.Delete(It.IsAny<Candidature>())).Verifiable();

            TestMock.CandidatureRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new CandidatureService(TestMock.CandidatureRepository.Object, TestMock.ClanRepository.Object);

            // Act
            var result = await service.DeclineCandidatureAsync(candidature, new UserId());

            // Assert
            result.Should().BeOfType<DomainValidationResult<Candidature>>();

            TestMock.ClanRepository.Verify(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()), Times.Once);

            TestMock.CandidatureRepository.Verify(repository => repository.Delete(It.IsAny<Candidature>()), Times.Once);

            TestMock.CandidatureRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeclineCandidatureAsync_WhenNotOwner_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var candidature = new Candidature(new UserId(), new ClanId());

            TestMock.ClanRepository.Setup(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            var service = new CandidatureService(TestMock.CandidatureRepository.Object, TestMock.ClanRepository.Object);

            // Act
            var result = await service.DeclineCandidatureAsync(candidature, new UserId());

            // Assert
            result.Should().BeOfType<DomainValidationResult<Candidature>>();

            result.Errors.Should().NotBeEmpty();

            TestMock.ClanRepository.Verify(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task DeleteCandidaturesAsync_WithClanId()
        {
            // Arrange
            var clanId = new ClanId();

            TestMock.CandidatureRepository.Setup(repository => repository.FetchAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(
                    new List<Candidature>
                    {
                        new Candidature(new UserId(), clanId),
                        new Candidature(new UserId(), clanId),
                        new Candidature(new UserId(), clanId)
                    })
                .Verifiable();

            TestMock.CandidatureRepository.Setup(repository => repository.Delete(It.IsAny<Candidature>())).Verifiable();

            TestMock.CandidatureRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new CandidatureService(TestMock.CandidatureRepository.Object, TestMock.ClanRepository.Object);

            // Act
            await service.DeleteCandidaturesAsync(new ClanId());

            // Assert
            TestMock.CandidatureRepository.Verify(repository => repository.FetchAsync(It.IsAny<ClanId>()), Times.Once);

            TestMock.CandidatureRepository.Verify(repository => repository.Delete(It.IsAny<Candidature>()), Times.Exactly(3));

            TestMock.CandidatureRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteCandidaturesAsync_WithUserId()
        {
            // Arrange
            var userId = new UserId();

            TestMock.CandidatureRepository.Setup(repository => repository.FetchAsync(It.IsAny<UserId>()))
                .ReturnsAsync(
                    new List<Candidature>
                    {
                        new Candidature(userId, new ClanId()),
                        new Candidature(userId, new ClanId()),
                        new Candidature(userId, new ClanId())
                    })
                .Verifiable();

            TestMock.CandidatureRepository.Setup(repository => repository.Delete(It.IsAny<Candidature>())).Verifiable();

            TestMock.CandidatureRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new CandidatureService(TestMock.CandidatureRepository.Object, TestMock.ClanRepository.Object);

            // Act
            await service.DeleteCandidaturesAsync(new UserId());

            // Assert
            TestMock.CandidatureRepository.Verify(repository => repository.FetchAsync(It.IsAny<UserId>()), Times.Once);

            TestMock.CandidatureRepository.Verify(repository => repository.Delete(It.IsAny<Candidature>()), Times.Exactly(3));

            TestMock.CandidatureRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task FetchCandidaturesAsync_WithClanId_ShouldBeOfTypeCandidatureList()
        {
            // Arrange
            var clanId = new ClanId();

            TestMock.CandidatureRepository.Setup(repository => repository.FetchAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(
                    new List<Candidature>
                    {
                        new Candidature(new UserId(), clanId),
                        new Candidature(new UserId(), clanId),
                        new Candidature(new UserId(), clanId)
                    })
                .Verifiable();

            var service = new CandidatureService(TestMock.CandidatureRepository.Object, TestMock.ClanRepository.Object);

            // Act
            var result = await service.FetchCandidaturesAsync(new ClanId());

            // Assert
            result.Should().BeOfType<List<Candidature>>();

            TestMock.CandidatureRepository.Verify(repository => repository.FetchAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task FetchCandidaturesAsync_WithUserId_ShouldBeOfTypeCandidatureList()
        {
            // Arrange
            var userId = new UserId();

            TestMock.CandidatureRepository.Setup(repository => repository.FetchAsync(It.IsAny<UserId>()))
                .ReturnsAsync(
                    new List<Candidature>
                    {
                        new Candidature(userId, new ClanId()),
                        new Candidature(userId, new ClanId()),
                        new Candidature(userId, new ClanId())
                    })
                .Verifiable();

            var service = new CandidatureService(TestMock.CandidatureRepository.Object, TestMock.ClanRepository.Object);

            // Act
            var result = await service.FetchCandidaturesAsync(new UserId());

            // Assert
            result.Should().BeOfType<List<Candidature>>();

            TestMock.CandidatureRepository.Verify(repository => repository.FetchAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task FindCandidatureAsync_ShouldBeOfTypeCandidature()
        {
            // Arrange
            TestMock.CandidatureRepository.Setup(repository => repository.FindAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync(new Candidature(new UserId(), new ClanId()))
                .Verifiable();

            var service = new CandidatureService(TestMock.CandidatureRepository.Object, TestMock.ClanRepository.Object);

            // Act
            var result = await service.FindCandidatureAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<Candidature>();

            TestMock.CandidatureRepository.Verify(repository => repository.FindAsync(It.IsAny<CandidatureId>()), Times.Once);
        }

        [Fact]
        public async Task SendCandidatureAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            TestMock.ClanRepository.Setup(repository => repository.IsMemberAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            TestMock.CandidatureRepository.Setup(repository => repository.ExistsAsync(It.IsAny<UserId>(), It.IsAny<ClanId>())).ReturnsAsync(false).Verifiable();

            TestMock.CandidatureRepository.Setup(repository => repository.Create(It.IsAny<Candidature>())).Verifiable();

            TestMock.CandidatureRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new CandidatureService(TestMock.CandidatureRepository.Object, TestMock.ClanRepository.Object);

            // Act
            var result = await service.SendCandidatureAsync(new UserId(), new ClanId());

            // Assert
            result.Should().BeOfType<DomainValidationResult<Candidature>>();

            TestMock.ClanRepository.Verify(repository => repository.IsMemberAsync(It.IsAny<UserId>()), Times.Once);

            TestMock.CandidatureRepository.Verify(repository => repository.ExistsAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()), Times.Once);
            TestMock.CandidatureRepository.Verify(repository => repository.Create(It.IsAny<Candidature>()), Times.Once);
            TestMock.CandidatureRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task SendCandidatureAsync_WhenCandidatureExist_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            TestMock.ClanRepository.Setup(repository => repository.IsMemberAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            TestMock.CandidatureRepository.Setup(repository => repository.ExistsAsync(It.IsAny<UserId>(), It.IsAny<ClanId>())).ReturnsAsync(true).Verifiable();

            var service = new CandidatureService(TestMock.CandidatureRepository.Object, TestMock.ClanRepository.Object);

            // Act
            var result = await service.SendCandidatureAsync(new UserId(), new ClanId());

            // Assert
            result.Should().BeOfType<DomainValidationResult<Candidature>>();

            result.Errors.Should().NotBeEmpty();

            TestMock.ClanRepository.Verify(repository => repository.IsMemberAsync(It.IsAny<UserId>()), Times.Once);

            TestMock.CandidatureRepository.Verify(repository => repository.ExistsAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task SendCandidatureAsync_WhenIsMember_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            TestMock.ClanRepository.Setup(repository => repository.IsMemberAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            var service = new CandidatureService(TestMock.CandidatureRepository.Object, TestMock.ClanRepository.Object);

            // Act
            var result = await service.SendCandidatureAsync(new UserId(), new ClanId());

            // Assert
            result.Should().BeOfType<DomainValidationResult<Candidature>>();

            result.Errors.Should().NotBeEmpty();

            TestMock.ClanRepository.Verify(repository => repository.IsMemberAsync(It.IsAny<UserId>()), Times.Once);
        }
    }
}
