// Filename: InvitationServiceTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Clans.Api.Application.Services;
using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.Domain.Repositories;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Moq;

using Xunit;

namespace eDoxa.Clans.UnitTests.Application.Services
{
    public sealed class InvitationServiceTest : UnitTest
    {
        public InvitationServiceTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task AcceptInvitationAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var userId = new UserId();
            var invitation = new Invitation(userId, new ClanId());

            TestMock.InvitationRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new InvitationService(TestMock.InvitationRepository.Object, TestMock.ClanRepository.Object);

            // Act
            var result = await service.AcceptInvitationAsync(invitation, userId);

            // Assert
            result.Should().BeOfType<DomainValidationResult<Invitation>>();
            TestMock.InvitationRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AcceptInvitationAsync_WhenDifferentUserId_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockClanRepository = new Mock<IClanRepository>();

            var invitation = new Invitation(new UserId(), new ClanId());

            var service = new InvitationService(TestMock.InvitationRepository.Object, mockClanRepository.Object);

            // Act
            var result = await service.AcceptInvitationAsync(invitation, new UserId());

            // Assert
            result.Should().BeOfType<DomainValidationResult<Invitation>>();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task DeclineInvitationAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var userId = new UserId();
            var invitation = new Invitation(userId, new ClanId());

            TestMock.InvitationRepository.Setup(repository => repository.Delete(It.IsAny<Invitation>())).Verifiable();

            TestMock.InvitationRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new InvitationService(TestMock.InvitationRepository.Object, TestMock.ClanRepository.Object);

            // Act
            var result = await service.DeclineInvitationAsync(invitation, userId);

            // Assert
            result.Should().BeOfType<DomainValidationResult<Invitation>>();
            TestMock.InvitationRepository.Verify(repository => repository.Delete(It.IsAny<Invitation>()), Times.Once);
            TestMock.InvitationRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeclineInvitationAsync_WhenDifferentUserId_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var invitation = new Invitation(new UserId(), new ClanId());

            var service = new InvitationService(TestMock.InvitationRepository.Object, TestMock.ClanRepository.Object);

            // Act
            var result = await service.DeclineInvitationAsync(invitation, new UserId());

            // Assert
            result.Should().BeOfType<DomainValidationResult<Invitation>>();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task DeleteInvitationsAsync_WithClanId()
        {
            // Arrange
            var clanId = new ClanId();

            TestMock.InvitationRepository.Setup(repository => repository.FetchAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(
                    new List<Invitation>
                    {
                        new Invitation(new UserId(), clanId),
                        new Invitation(new UserId(), clanId),
                        new Invitation(new UserId(), clanId)
                    })
                .Verifiable();

            TestMock.InvitationRepository.Setup(repository => repository.Delete(It.IsAny<Invitation>())).Verifiable();

            TestMock.InvitationRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new InvitationService(TestMock.InvitationRepository.Object, TestMock.ClanRepository.Object);

            // Act
            await service.DeleteInvitationsAsync(new ClanId());

            // Assert
            TestMock.InvitationRepository.Verify(repository => repository.FetchAsync(It.IsAny<ClanId>()), Times.Once);
            TestMock.InvitationRepository.Verify(repository => repository.Delete(It.IsAny<Invitation>()), Times.Exactly(3));
            TestMock.InvitationRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteInvitationsAsync_WithUserId()
        {
            // Arrange
            var userId = new UserId();

            TestMock.InvitationRepository.Setup(repository => repository.FetchAsync(It.IsAny<UserId>()))
                .ReturnsAsync(
                    new List<Invitation>
                    {
                        new Invitation(userId, new ClanId()),
                        new Invitation(userId, new ClanId()),
                        new Invitation(userId, new ClanId())
                    })
                .Verifiable();

            TestMock.InvitationRepository.Setup(repository => repository.Delete(It.IsAny<Invitation>())).Verifiable();

            TestMock.InvitationRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new InvitationService(TestMock.InvitationRepository.Object, TestMock.ClanRepository.Object);

            // Act
            await service.DeleteInvitationsAsync(new UserId());

            // Assert
            TestMock.InvitationRepository.Verify(repository => repository.FetchAsync(It.IsAny<UserId>()), Times.Once);
            TestMock.InvitationRepository.Verify(repository => repository.Delete(It.IsAny<Invitation>()), Times.Exactly(3));
            TestMock.InvitationRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task FetchInvitationsAsync_WithClanId_ShouldBeOfTypeInvitationList()
        {
            // Arrange
            var clanId = new ClanId();

            TestMock.InvitationRepository.Setup(repository => repository.FetchAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(
                    new List<Invitation>
                    {
                        new Invitation(new UserId(), clanId),
                        new Invitation(new UserId(), clanId),
                        new Invitation(new UserId(), clanId)
                    })
                .Verifiable();

            var service = new InvitationService(TestMock.InvitationRepository.Object, TestMock.ClanRepository.Object);

            // Act
            var result = await service.FetchInvitationsAsync(new ClanId());

            // Assert
            result.Should().BeOfType<List<Invitation>>();

            TestMock.InvitationRepository.Verify(repository => repository.FetchAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task FetchInvitationsAsync_WithUserId_ShouldBeOfTypeInvitationList()
        {
            // Arrange
            var userId = new UserId();

            TestMock.InvitationRepository.Setup(repository => repository.FetchAsync(It.IsAny<UserId>()))
                .ReturnsAsync(
                    new List<Invitation>
                    {
                        new Invitation(userId, new ClanId()),
                        new Invitation(userId, new ClanId()),
                        new Invitation(userId, new ClanId())
                    })
                .Verifiable();

            var service = new InvitationService(TestMock.InvitationRepository.Object, TestMock.ClanRepository.Object);

            // Act
            var result = await service.FetchInvitationsAsync(new UserId());

            // Assert
            result.Should().BeOfType<List<Invitation>>();

            TestMock.InvitationRepository.Verify(repository => repository.FetchAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task FindInvitationAsync_ShouldBeOfTypeInvitation()
        {
            // Arrange
            TestMock.InvitationRepository.Setup(repository => repository.FindAsync(It.IsAny<InvitationId>()))
                .ReturnsAsync(new Invitation(new UserId(), new ClanId()))
                .Verifiable();

            var service = new InvitationService(TestMock.InvitationRepository.Object, TestMock.ClanRepository.Object);

            // Act
            var result = await service.FindInvitationAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<Invitation>();
            TestMock.InvitationRepository.Verify(repository => repository.FindAsync(It.IsAny<InvitationId>()), Times.Once);
        }

        [Fact]
        public async Task SendInvitationAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            TestMock.ClanRepository.Setup(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            TestMock.ClanRepository.Setup(repository => repository.IsMemberAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            TestMock.InvitationRepository.Setup(repository => repository.ExistsAsync(It.IsAny<UserId>(), It.IsAny<ClanId>())).ReturnsAsync(false).Verifiable();

            TestMock.InvitationRepository.Setup(repository => repository.Create(It.IsAny<Invitation>())).Verifiable();

            TestMock.InvitationRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new InvitationService(TestMock.InvitationRepository.Object, TestMock.ClanRepository.Object);

            // Act
            var result = await service.SendInvitationAsync(new ClanId(), new UserId(), new UserId());

            // Assert
            result.Should().BeOfType<DomainValidationResult<Invitation>>();
            TestMock.ClanRepository.Verify(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()), Times.Once);
            TestMock.ClanRepository.Verify(repository => repository.IsMemberAsync(It.IsAny<UserId>()), Times.Once);
            TestMock.InvitationRepository.Verify(repository => repository.ExistsAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()), Times.Once);
            TestMock.InvitationRepository.Verify(repository => repository.Create(It.IsAny<Invitation>()), Times.Once);
            TestMock.InvitationRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task SendInvitationAsync_WhenInvitationExist_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            TestMock.ClanRepository.Setup(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            TestMock.ClanRepository.Setup(repository => repository.IsMemberAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            TestMock.InvitationRepository.Setup(repository => repository.ExistsAsync(It.IsAny<UserId>(), It.IsAny<ClanId>())).ReturnsAsync(true).Verifiable();

            var service = new InvitationService(TestMock.InvitationRepository.Object, TestMock.ClanRepository.Object);

            // Act
            var result = await service.SendInvitationAsync(new ClanId(), new UserId(), new UserId());

            // Assert
            result.Should().BeOfType<DomainValidationResult<Invitation>>();
            result.Errors.Should().NotBeEmpty();
            TestMock.ClanRepository.Verify(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()), Times.Once);
            TestMock.ClanRepository.Verify(repository => repository.IsMemberAsync(It.IsAny<UserId>()), Times.Once);
            TestMock.InvitationRepository.Verify(repository => repository.ExistsAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task SendInvitationAsync_WhenIsMember_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            TestMock.ClanRepository.Setup(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            TestMock.ClanRepository.Setup(repository => repository.IsMemberAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            var service = new InvitationService(TestMock.InvitationRepository.Object, TestMock.ClanRepository.Object);

            // Act
            var result = await service.SendInvitationAsync(new ClanId(), new UserId(), new UserId());

            // Assert
            result.Should().BeOfType<DomainValidationResult<Invitation>>();
            result.Errors.Should().NotBeEmpty();
            TestMock.ClanRepository.Verify(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()), Times.Once);
            TestMock.ClanRepository.Verify(repository => repository.IsMemberAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task SendInvitationAsync_WhenNotOwner_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            TestMock.ClanRepository.Setup(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            var service = new InvitationService(TestMock.InvitationRepository.Object, TestMock.ClanRepository.Object);

            // Act
            var result = await service.SendInvitationAsync(new ClanId(), new UserId(), new UserId());

            // Assert
            result.Should().BeOfType<DomainValidationResult<Invitation>>();

            result.Errors.Should().NotBeEmpty();

            TestMock.ClanRepository.Verify(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()), Times.Once);
        }
    }
}
