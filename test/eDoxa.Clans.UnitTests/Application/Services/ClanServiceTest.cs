// Filename: ClanServiceTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
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
    public sealed class ClanServiceTest : UnitTest
    {
        public ClanServiceTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task AddMemberToClanAsync()
        {
            // Arrange
            var mockClanRepository = new Mock<IClanRepository>();

            var ownerId = new UserId();
            var clan = new Clan("test", ownerId);

            mockClanRepository.Setup(repository => repository.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(clan).Verifiable();

            mockClanRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new ClanService(mockClanRepository.Object);

            // Act
            await service.AddMemberToClanAsync(clan.Id, new Candidature(new UserId(), clan.Id));

            // Assert;
            mockClanRepository.Verify(repository => repository.FindClanAsync(It.IsAny<ClanId>()), Times.Once);
            mockClanRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void AddMemberToClanAsync_WhenClanDoesNotExists_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var mockClanRepository = new Mock<IClanRepository>();

            var ownerId = new UserId();
            var clan = new Clan("test", ownerId);

            mockClanRepository.Setup(repository => repository.FindClanAsync(It.IsAny<ClanId>())).Verifiable();

            var service = new ClanService(mockClanRepository.Object);

            // Act
            var action = new Func<Task>(async () => await service.AddMemberToClanAsync(clan.Id, new Candidature(new UserId(), clan.Id)));

            // Assert;
            action.Should().Throw<InvalidOperationException>();
            mockClanRepository.Verify(repository => repository.FindClanAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task CreateClanAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockClanRepository = new Mock<IClanRepository>();

            mockClanRepository.Setup(repository => repository.IsMemberAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            mockClanRepository.Setup(repository => repository.ExistsAsync(It.IsAny<string>())).ReturnsAsync(false).Verifiable();

            mockClanRepository.Setup(repository => repository.Create(It.IsAny<Clan>())).Verifiable();

            mockClanRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new ClanService(mockClanRepository.Object);

            // Act
            var result = await service.CreateClanAsync(new UserId(), "test clan");

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            mockClanRepository.Verify(repository => repository.IsMemberAsync(It.IsAny<UserId>()), Times.Once);
            mockClanRepository.Verify(repository => repository.ExistsAsync(It.IsAny<string>()), Times.Once);
            mockClanRepository.Verify(repository => repository.Create(It.IsAny<Clan>()), Times.Once);
            mockClanRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateClanAsync_WhenExists_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockClanRepository = new Mock<IClanRepository>();

            mockClanRepository.Setup(repository => repository.IsMemberAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            mockClanRepository.Setup(repository => repository.ExistsAsync(It.IsAny<string>())).ReturnsAsync(true).Verifiable();

            var service = new ClanService(mockClanRepository.Object);

            // Act
            var result = await service.CreateClanAsync(new UserId(), "test clan");

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            result.Errors.Should().NotBeEmpty();
            mockClanRepository.Verify(repository => repository.IsMemberAsync(It.IsAny<UserId>()), Times.Once);
            mockClanRepository.Verify(repository => repository.ExistsAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task CreateClanAsync_WhenMember_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockClanRepository = new Mock<IClanRepository>();

            mockClanRepository.Setup(repository => repository.IsMemberAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            var service = new ClanService(mockClanRepository.Object);

            // Act
            var result = await service.CreateClanAsync(new UserId(), "test clan");

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            result.Errors.Should().NotBeEmpty();
            mockClanRepository.Verify(repository => repository.IsMemberAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task DeleteLogoAsync()
        {
            // Arrange
            var mockClanRepository = new Mock<IClanRepository>();

            mockClanRepository.Setup(repository => repository.DeleteLogoAsync(It.IsAny<ClanId>())).Returns(Task.CompletedTask).Verifiable();

            var service = new ClanService(mockClanRepository.Object);

            // Act
            await service.DeleteLogoAsync(new ClanId());

            // Assert;
            mockClanRepository.Verify(repository => repository.DeleteLogoAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task DownloadLogoAsync_ShouldBeOfTypeStream()
        {
            // Arrange
            var mockClanRepository = new Mock<IClanRepository>();

            var memoryStream = new MemoryStream();

            mockClanRepository.Setup(repository => repository.DownloadLogoAsync(It.IsAny<ClanId>())).ReturnsAsync(memoryStream).Verifiable();

            var service = new ClanService(mockClanRepository.Object);

            var clan = new Clan("test", new UserId());

            // Act
            var result = await service.DownloadLogoAsync(clan);

            // Assert
            result.Should().BeOfType<MemoryStream>();
            mockClanRepository.Verify(repository => repository.DownloadLogoAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task FetchClansAsync_ShouldBeOfTypeClanList()
        {
            // Arrange
            var mockClanRepository = new Mock<IClanRepository>();

            mockClanRepository.Setup(repository => repository.FetchClansAsync())
                .ReturnsAsync(
                    new List<Clan>
                    {
                        new Clan("test", new UserId()),
                        new Clan("test", new UserId()),
                        new Clan("test", new UserId())
                    })
                .Verifiable();

            var service = new ClanService(mockClanRepository.Object);

            // Act
            var result = await service.FetchClansAsync();

            // Assert
            result.Should().BeOfType<List<Clan>>();
            mockClanRepository.Verify(repository => repository.FetchClansAsync(), Times.Once);
        }

        [Fact]
        public async Task FetchMembersAsync_ShouldBeOfTypeMemberList()
        {
            // Arrange
            var mockClanRepository = new Mock<IClanRepository>();

            var ownerId = new UserId();
            var clan = new Clan("test", ownerId);

            mockClanRepository.Setup(repository => repository.FetchMembersAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(
                    new List<Member>
                    {
                        new Member(clan.Id, ownerId)
                    })
                .Verifiable();

            var service = new ClanService(mockClanRepository.Object);

            // Act
            var result = await service.FetchMembersAsync(clan);

            // Assert
            result.Should().BeOfType<List<Member>>();
            mockClanRepository.Verify(repository => repository.FetchMembersAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task FindClanAsync_ShouldBeOfTypeClan()
        {
            // Arrange
            var mockClanRepository = new Mock<IClanRepository>();

            mockClanRepository.Setup(repository => repository.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(new Clan("test", new UserId())).Verifiable();

            var service = new ClanService(mockClanRepository.Object);

            // Act
            var result = await service.FindClanAsync(new ClanId());

            // Assert
            result.Should().BeOfType<Clan>();
            mockClanRepository.Verify(repository => repository.FindClanAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task FindMemberAsync_ShouldBeOfTypeMember()
        {
            // Arrange
            var mockClanRepository = new Mock<IClanRepository>();

            var ownerId = new UserId();
            var clan = new Clan("test", ownerId);

            mockClanRepository.Setup(repository => repository.FindMemberAsync(It.IsAny<ClanId>(), It.IsAny<MemberId>()))
                .ReturnsAsync(new Member(new ClanId(), new UserId()))
                .Verifiable();

            var service = new ClanService(mockClanRepository.Object);

            // Act
            var result = await service.FindMemberAsync(clan, new MemberId());

            // Assert
            result.Should().BeOfType<Member>();
            mockClanRepository.Verify(repository => repository.FindMemberAsync(It.IsAny<ClanId>(), It.IsAny<MemberId>()), Times.Once);
        }

        [Fact]
        public async Task IsMemberAsync()
        {
            // Arrange
            var mockClanRepository = new Mock<IClanRepository>();

            var service = new ClanService(mockClanRepository.Object);

            mockClanRepository.Setup(repository => repository.IsMemberAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            // Act
            await service.IsMemberAsync(new UserId());

            // Assert
            mockClanRepository.Verify(repository => repository.IsMemberAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task KickMemberFromClanAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockClanRepository = new Mock<IClanRepository>();

            var ownerId = new UserId();
            var clan = new Clan("test", ownerId);

            var memberUserId = new UserId();
            clan.AddMember(new Member(clan.Id, memberUserId));

            var member = clan.FindMember(memberUserId);

            mockClanRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new ClanService(mockClanRepository.Object);

            // Act
            var result = await service.KickMemberFromClanAsync(clan, ownerId, member.Id);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            mockClanRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task KickMemberFromClanAsync_WhenHasNotMember_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockClanRepository = new Mock<IClanRepository>();

            var ownerId = new UserId();
            var clan = new Clan("test", ownerId);

            var service = new ClanService(mockClanRepository.Object);

            // Act
            var result = await service.KickMemberFromClanAsync(clan, ownerId, new MemberId());

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task KickMemberFromClanAsync_WhenNotOwner_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var ownerId = new UserId();
            var mockClanRepository = new Mock<IClanRepository>();

            var clan = new Clan("test", new UserId());

            var service = new ClanService(mockClanRepository.Object);

            // Act
            var result = await service.KickMemberFromClanAsync(clan, ownerId, new MemberId());

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task LeaveClanAsync_WhenMember_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockClanRepository = new Mock<IClanRepository>();

            var ownerId = new UserId();
            var clan = new Clan("test", ownerId);

            var memberUserId = new UserId();
            clan.AddMember(new Member(clan.Id, memberUserId));

            mockClanRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new ClanService(mockClanRepository.Object);

            // Act
            var result = await service.LeaveClanAsync(clan, memberUserId);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            mockClanRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task LeaveClanAsync_WhenNotMember_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockClanRepository = new Mock<IClanRepository>();

            var ownerId = new UserId();
            var clan = new Clan("test", ownerId);

            var service = new ClanService(mockClanRepository.Object);

            // Act
            var result = await service.LeaveClanAsync(clan, new UserId());

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task LeaveClanAsync_WhenOwner_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockClanRepository = new Mock<IClanRepository>();

            var ownerId = new UserId();
            var clan = new Clan("test", ownerId);

            mockClanRepository.Setup(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockClanRepository.Setup(repository => repository.Delete(It.IsAny<Clan>())).Verifiable();

            var service = new ClanService(mockClanRepository.Object);

            // Act
            var result = await service.LeaveClanAsync(clan, ownerId);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            mockClanRepository.Verify(repository => repository.UnitOfWork.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
            mockClanRepository.Verify(repository => repository.Delete(It.IsAny<Clan>()), Times.Once);
        }

        [Fact]
        public async Task UploadLogoAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockClanRepository = new Mock<IClanRepository>();

            var memoryStream = new MemoryStream();

            mockClanRepository.Setup(repository => repository.UploadLogoAsync(It.IsAny<ClanId>(), It.IsAny<Stream>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new ClanService(mockClanRepository.Object);

            var ownerId = new UserId();
            var clan = new Clan("test", ownerId);

            // Act
            var result = await service.UploadLogoAsync(
                clan,
                ownerId,
                memoryStream,
                "testFile");

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            mockClanRepository.Verify(repository => repository.UploadLogoAsync(It.IsAny<ClanId>(), It.IsAny<Stream>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task UploadLogoAsync_WhenNotOwner_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockClanRepository = new Mock<IClanRepository>();

            var memoryStream = new MemoryStream();

            var service = new ClanService(mockClanRepository.Object);

            var ownerId = new UserId();

            var clan = new Clan("test", ownerId);

            // Act
            var result = await service.UploadLogoAsync(
                clan,
                new UserId(),
                memoryStream,
                "testFile");

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            result.Errors.Should().NotBeEmpty();
        }
    }
}
