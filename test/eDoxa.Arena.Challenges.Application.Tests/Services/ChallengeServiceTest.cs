// Filename: ChallengeServiceTest.cs
// Date Created: 2019-05-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Application.Services;
using eDoxa.Arena.Challenges.Domain;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Seedwork.Domain.Enumerations;
using eDoxa.Testing.MSTest.Extensions;

using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.Application.Tests.Services
{
    [TestClass]
    public sealed class ChallengeServiceTest
    {
        private static readonly FakeRandomChallengeFactory FakeRandomChallengeFactory = FakeRandomChallengeFactory.Instance;
        private Mock<IChallengeRepository> _mockChallengeRepository;
        private Mock<ILogger<ChallengeService>> _mockLogger;

        public ChallengeServiceTest()
        {
            _mockChallengeRepository = new Mock<IChallengeRepository>();
            _mockLogger = new Mock<ILogger<ChallengeService>>();
            _mockLogger.SetupLog();
        }

        [TestMethod]
        public async Task CompleteAsync_ShouldBeCompletedTask()
        {
            // Arrange
            _mockChallengeRepository.Setup(mock => mock.FindChallengesAsync(It.IsAny<ChallengeType>(), It.IsAny<Game>(), ChallengeState.Ended))
                .ReturnsAsync(FakeRandomChallengeFactory.CreateRandomChallenges(ChallengeState.Ended))
                .Verifiable();

            _mockChallengeRepository.Setup(mock => mock.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            var service = new ChallengeService(_mockLogger.Object, _mockChallengeRepository.Object);

            // Assert
            await service.CompleteAsync(default);

            _mockChallengeRepository.Verify(mock => mock.FindChallengesAsync(It.IsAny<ChallengeType>(), It.IsAny<Game>(), ChallengeState.Ended), Times.Once);

            _mockChallengeRepository.Verify(mock => mock.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task PublishAsync_ShouldBeCompletedTask()
        {
            // Arrange
            _mockChallengeRepository.Setup(mock => mock.Create(It.IsAny<Challenge>())).Verifiable();

            _mockChallengeRepository.Setup(mock => mock.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new ChallengeService(_mockLogger.Object, _mockChallengeRepository.Object);

            // Act
            await service.PublishAsync(PublisherInterval.Daily, default);

            // Assert
            _mockChallengeRepository.Verify(mock => mock.Create(It.IsAny<Challenge>()));

            _mockChallengeRepository.Verify(mock => mock.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}