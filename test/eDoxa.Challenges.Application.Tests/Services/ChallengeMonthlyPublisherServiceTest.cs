// Filename: ChallengeMonthlyPublisherServiceTest.cs
// Date Created: 2019-03-22
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
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
        [TestMethod]
        public async Task PublishAsync_Create_ShouldBeInvoked()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ChallengeMonthlyPublisherService>>();

            mockLogger.SetupLoggerWithLogWarningVerifiable();

            mockLogger.SetupLoggerWithLogLevelCriticalVerifiable();

            var mockChallengeRepository = new Mock<IChallengeRepository>();

            mockChallengeRepository.Setup(repository => repository.Create(It.IsAny<Challenge>())).Verifiable();

            mockChallengeRepository.Setup(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()))
                                   .Returns(Task.CompletedTask)
                                   .Verifiable();
            
            var service = new ChallengeMonthlyPublisherService(mockLogger.Object, mockChallengeRepository.Object);

            // Act
            await service.PublishAsync();

            // Assert
            mockChallengeRepository.Verify(repository => repository.Create(It.IsAny<Challenge>()));

            mockChallengeRepository.Verify(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}