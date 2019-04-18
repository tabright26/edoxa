// Filename: ChallengeCloserServiceTest.cs
// Date Created: 2019-03-22
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Application.Services;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Factories;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Seedwork.Domain.Common.Enums;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Challenges.Application.Tests.Services
{
    [TestClass]
    public sealed class ChallengeCloserServiceTest
    {
        private static readonly ChallengeAggregateFactory ChallengeAggregateFactory = ChallengeAggregateFactory.Instance;

        [TestMethod]
        public async Task HandleAsync_FindChallengeAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var mockChallengeRepository = new Mock<IChallengeRepository>();

            mockChallengeRepository.Setup(repository => repository.FindChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeType>(), ChallengeState.Ended))
                                   .ReturnsAsync(ChallengeAggregateFactory.CreateRandomChallenges(ChallengeState.Ended))
                                   .Verifiable();

            mockChallengeRepository.Setup(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()))
                                   .Returns(Task.CompletedTask)
                                   .Verifiable();

            // Act
            var service = new ChallengeCloserService(mockChallengeRepository.Object);

            // Assert
            await service.CloseAsync();

            mockChallengeRepository.Verify(
                repository => repository.FindChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeType>(), ChallengeState.Ended),
                Times.Once
            );

            mockChallengeRepository.Verify(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}