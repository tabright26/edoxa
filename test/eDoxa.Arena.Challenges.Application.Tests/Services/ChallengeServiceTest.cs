// Filename: ChallengeServiceTest.cs
// Date Created: 2019-05-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.Application.Tests.Services
{
    [TestClass]
    public sealed class ChallengeServiceTest
    {
        //private static readonly FakeRandomChallengeFactory FakeRandomChallengeFactory = FakeRandomChallengeFactory.Instance;
        //private Mock<IChallengeRepository> _mockChallengeRepository;

        //public ChallengeServiceTest()
        //{
        //    _mockChallengeRepository = new Mock<IChallengeRepository>();
        //}

        //[TestMethod]
        //public async Task CompleteAsync_ShouldBeCompletedTask()
        //{
        //    // Arrange
        //    _mockChallengeRepository.Setup(mock => mock.FindChallengesAsync(It.IsAny<Game>(), ChallengeState.Ended))
        //        .ReturnsAsync(FakeRandomChallengeFactory.CreateRandomChallenges(ChallengeState.Ended))
        //        .Verifiable();

        //    _mockChallengeRepository.Setup(mock => mock.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()))
        //        .Returns(Task.CompletedTask)
        //        .Verifiable();

        //    // Act
        //    var service = new ChallengeService(_mockChallengeRepository.Object);

        //    // Assert
        //    await service.CompleteAsync(default);

        //    _mockChallengeRepository.Verify(mock => mock.FindChallengesAsync(It.IsAny<Game>(), ChallengeState.Ended), Times.Once);

        //    _mockChallengeRepository.Verify(mock => mock.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()), Times.Once);
        //}
    }
}