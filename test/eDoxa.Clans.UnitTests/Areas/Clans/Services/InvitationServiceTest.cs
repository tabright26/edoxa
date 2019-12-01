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
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Moq;

using Xunit;

namespace eDoxa.Clans.UnitTests.Areas.Clans.Services
{
    public sealed class InvitationServiceTest : UnitTest
    {
        public InvitationServiceTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task FetchInvitationsAsync_WithClanId_ShouldBeOfTypeInvitationList()
        {
            // Arrange
            var mockInvitationRepository = new Mock<IInvitationRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            var clanId = new ClanId();

            mockInvitationRepository.Setup(repository => repository.FetchAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(new List<Invitation>()
                {
                    new Invitation(new UserId(), clanId),
                    new Invitation(new UserId(), clanId),
                    new Invitation(new UserId(), clanId)
                })
                .Verifiable();

            var service = new InvitationService(mockInvitationRepository.Object, mockClanRepository.Object);

            // Act
            var result = await service.FetchInvitationsAsync(new ClanId());

            // Assert
            result.Should().BeOfType<List<Invitation>>();
            mockInvitationRepository.Verify(repository => repository.FetchAsync(It.IsAny<ClanId>()), Times.Once);
        }


        [Fact]
        public async Task FetchInvitationsAsync_WithUserId_ShouldBeOfTypeInvitationList()
        {
            // Arrange
            var mockInvitationRepository = new Mock<IInvitationRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            var userId = new UserId();

            mockInvitationRepository.Setup(repository => repository.FetchAsync(It.IsAny<UserId>()))
                .ReturnsAsync(new List<Invitation>()
                {
                    new Invitation(userId, new ClanId()),
                    new Invitation(userId, new ClanId()),
                    new Invitation(userId, new ClanId())
                })
                .Verifiable();

            var service = new InvitationService(mockInvitationRepository.Object, mockClanRepository.Object);

            // Act
            var result = await service.FetchInvitationsAsync(new UserId());

            // Assert
            result.Should().BeOfType<List<Invitation>>();
            mockInvitationRepository.Verify(repository => repository.FetchAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task FindInvitationAsync_ShouldBeOfTypeInvitation()
        {
            // Arrange
            var mockInvitationRepository = new Mock<IInvitationRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            mockInvitationRepository.Setup(repository => repository.FindAsync(It.IsAny<InvitationId>()))
                .ReturnsAsync(new Invitation(new UserId(), new ClanId()))
                .Verifiable();

            var service = new InvitationService(mockInvitationRepository.Object, mockClanRepository.Object);

            // Act
            var result = await service.FindInvitationAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<Invitation>();
            mockInvitationRepository.Verify(repository => repository.FindAsync(It.IsAny<InvitationId>()), Times.Once);
        }

        [Fact]
        public async Task SendInvitationAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockInvitationRepository = new Mock<IInvitationRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            mockClanRepository.Setup(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()))
                .ReturnsAsync(true)
                .Verifiable();

            mockClanRepository.Setup(repository => repository.IsMemberAsync(It.IsAny<UserId>()))
                .ReturnsAsync(false)
                .Verifiable();

            mockInvitationRepository.Setup(repository => repository.ExistsAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()))
                .ReturnsAsync(false)
                .Verifiable();

            mockInvitationRepository.Setup(repository => repository.Create(It.IsAny<Invitation>()))
                .Verifiable();

            mockInvitationRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new InvitationService(mockInvitationRepository.Object, mockClanRepository.Object);

            // Act
            var result = await service.SendInvitationAsync(new ClanId(), new UserId(), new UserId());

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            mockClanRepository.Verify(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()), Times.Once);
            mockClanRepository.Verify(repository => repository.IsMemberAsync(It.IsAny<UserId>()), Times.Once);
            mockInvitationRepository.Verify(repository => repository.ExistsAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()), Times.Once);
            mockInvitationRepository.Verify(repository => repository.Create(It.IsAny<Invitation>()), Times.Once);
            mockInvitationRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task SendInvitationAsync_WhenNotOwner_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockInvitationRepository = new Mock<IInvitationRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            mockClanRepository.Setup(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()))
                .ReturnsAsync(false)
                .Verifiable();

            var service = new InvitationService(mockInvitationRepository.Object, mockClanRepository.Object);

            // Act
            var result = await service.SendInvitationAsync(new ClanId(), new UserId(), new UserId());

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            result.Errors.Should().NotBeEmpty();
            mockClanRepository.Verify(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task SendInvitationAsync_WhenIsMember_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockInvitationRepository = new Mock<IInvitationRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            mockClanRepository.Setup(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()))
                .ReturnsAsync(true)
                .Verifiable();

            mockClanRepository.Setup(repository => repository.IsMemberAsync(It.IsAny<UserId>()))
                .ReturnsAsync(true)
                .Verifiable();

            var service = new InvitationService(mockInvitationRepository.Object, mockClanRepository.Object);

            // Act
            var result = await service.SendInvitationAsync(new ClanId(), new UserId(), new UserId());

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            result.Errors.Should().NotBeEmpty();
            mockClanRepository.Verify(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()), Times.Once);
            mockClanRepository.Verify(repository => repository.IsMemberAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task SendInvitationAsync_WhenInvitationExist_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockInvitationRepository = new Mock<IInvitationRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            mockClanRepository.Setup(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()))
                .ReturnsAsync(true)
                .Verifiable();

            mockClanRepository.Setup(repository => repository.IsMemberAsync(It.IsAny<UserId>()))
                .ReturnsAsync(false)
                .Verifiable();

            mockInvitationRepository.Setup(repository => repository.ExistsAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()))
                .ReturnsAsync(true)
                .Verifiable();

            var service = new InvitationService(mockInvitationRepository.Object, mockClanRepository.Object);

            // Act
            var result = await service.SendInvitationAsync(new ClanId(), new UserId(), new UserId());

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            result.Errors.Should().NotBeEmpty();
            mockClanRepository.Verify(repository => repository.IsOwnerAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()), Times.Once);
            mockClanRepository.Verify(repository => repository.IsMemberAsync(It.IsAny<UserId>()), Times.Once);
            mockInvitationRepository.Verify(repository => repository.ExistsAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task AcceptInvitationAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockInvitationRepository = new Mock<IInvitationRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            var userId = new UserId();
            var invitation = new Invitation(userId, new ClanId());

            mockInvitationRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new InvitationService(mockInvitationRepository.Object, mockClanRepository.Object);

            // Act
            var result = await service.AcceptInvitationAsync(invitation, userId);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            mockInvitationRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AcceptInvitationAsync_WhenDifferentUserId_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockInvitationRepository = new Mock<IInvitationRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            var invitation = new Invitation(new UserId(), new ClanId());

            var service = new InvitationService(mockInvitationRepository.Object, mockClanRepository.Object);

            // Act
            var result = await service.AcceptInvitationAsync(invitation, new UserId());

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task DeclineInvitationAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockInvitationRepository = new Mock<IInvitationRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            var userId = new UserId();
            var invitation = new Invitation(userId, new ClanId());

            mockInvitationRepository.Setup(repository => repository.Delete(It.IsAny<Invitation>()))
                .Verifiable();

            mockInvitationRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new InvitationService(mockInvitationRepository.Object, mockClanRepository.Object);

            // Act
            var result = await service.DeclineInvitationAsync(invitation, userId);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            mockInvitationRepository.Verify(repository => repository.Delete(It.IsAny<Invitation>()), Times.Once);
            mockInvitationRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeclineInvitationAsync_WhenDifferentUserId_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockInvitationRepository = new Mock<IInvitationRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            var invitation = new Invitation(new UserId(), new ClanId());

            var service = new InvitationService(mockInvitationRepository.Object, mockClanRepository.Object);

            // Act
            var result = await service.DeclineInvitationAsync(invitation, new UserId());

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task DeleteInvitationsAsync_WithUserId()
        {
            // Arrange
            var mockInvitationRepository = new Mock<IInvitationRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            var userId = new UserId();

            mockInvitationRepository.Setup(repository => repository.FetchAsync(It.IsAny<UserId>()))
                .ReturnsAsync(new List<Invitation>()
                {
                    new Invitation(userId, new ClanId()),
                    new Invitation(userId, new ClanId()),
                    new Invitation(userId, new ClanId())
                })
                .Verifiable();

            mockInvitationRepository.Setup(repository => repository.Delete(It.IsAny<Invitation>()))
                .Verifiable();

            mockInvitationRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new InvitationService(mockInvitationRepository.Object, mockClanRepository.Object);

            // Act
            await service.DeleteInvitationsAsync(new UserId());

            // Assert
            mockInvitationRepository.Verify(repository => repository.FetchAsync(It.IsAny<UserId>()), Times.Once);
            mockInvitationRepository.Verify(repository => repository.Delete(It.IsAny<Invitation>()), Times.Exactly(3));
            mockInvitationRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteInvitationsAsync_WithClanId()
        {
            // Arrange
            var mockInvitationRepository = new Mock<IInvitationRepository>();
            var mockClanRepository = new Mock<IClanRepository>();

            var clanId = new ClanId();

            mockInvitationRepository.Setup(repository => repository.FetchAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(new List<Invitation>()
                {
                    new Invitation(new UserId(), clanId),
                    new Invitation(new UserId(), clanId),
                    new Invitation(new UserId(), clanId)
                })
                .Verifiable();

            mockInvitationRepository.Setup(repository => repository.Delete(It.IsAny<Invitation>()))
                .Verifiable();

            mockInvitationRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new InvitationService(mockInvitationRepository.Object, mockClanRepository.Object);

            // Act
            await service.DeleteInvitationsAsync(new ClanId());

            // Assert
            mockInvitationRepository.Verify(repository => repository.FetchAsync(It.IsAny<ClanId>()), Times.Once);
            mockInvitationRepository.Verify(repository => repository.Delete(It.IsAny<Invitation>()), Times.Exactly(3));
            mockInvitationRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}
