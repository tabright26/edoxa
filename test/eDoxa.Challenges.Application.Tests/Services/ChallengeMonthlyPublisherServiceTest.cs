// Filename: ChallengeMonthlyPublisherServiceTest.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Application.Services;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Testing.MSTest.Extensions;

using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Challenges.Application.Tests.Services
{
    [TestClass]
    public sealed class ChallengeMonthlyPublisherServiceTest
    {
        private Mock<IChallengeRepository> _mockChallengeRepository;
        private Mock<ILogger<ChallengeMonthlyPublisherService>> _mockLogger;

        public ChallengeMonthlyPublisherServiceTest()
        {
            _mockChallengeRepository = new Mock<IChallengeRepository>();
            _mockLogger = new Mock<ILogger<ChallengeMonthlyPublisherService>>();
            _mockLogger.SetupLog();
        }

        [TestMethod]
        public async Task PublishAsync_Create_ShouldBeInvoked()
        {
            // Arrange
            _mockChallengeRepository.Setup(repository => repository.Create(It.IsAny<Challenge>())).Verifiable();

            _mockChallengeRepository.Setup(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new ChallengeMonthlyPublisherService(_mockLogger.Object, _mockChallengeRepository.Object);

            // Act
            await service.PublishAsync();

            // Assert
            _mockChallengeRepository.Verify(repository => repository.Create(It.IsAny<Challenge>()));

            _mockChallengeRepository.Verify(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}