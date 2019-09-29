// Filename: ChallengeServiceTestClass.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Services;
using eDoxa.Arena.Challenges.Domain.Adapters;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.UnitTests.TestHelpers;
using eDoxa.Seedwork.Domain;

using FluentAssertions;

using Moq;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Areas.Challenges.Services
{
    //TODO: Maybe split this ?

    public sealed class ChallengeServiceTestClass : UnitTestClass
    {
        public ChallengeServiceTestClass(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
            _mockChallengeRepository = new Mock<IChallengeRepository>();
            _mockGameReferencesFactory = new Mock<IGameReferencesFactory>();
            _mockMatchFactory = new Mock<IMatchFactory>();
            _mockIdentityService = new Mock<IIdentityService>();
        }

        private readonly Mock<IChallengeRepository> _mockChallengeRepository;
        private readonly Mock<IGameReferencesFactory> _mockGameReferencesFactory;
        private readonly Mock<IMatchFactory> _mockMatchFactory;
        private readonly Mock<IIdentityService> _mockIdentityService;

        [Fact]
        public void RegisterParticipantAsync_ChallengeNullReference_ShouldThrowInvalidOperationException()
        {
            // Arrange
            _mockChallengeRepository.Setup(challengeRepository => challengeRepository.FindChallengeAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync((IChallenge) null)
                .Verifiable();

            var challengeService = new ChallengeService(
                _mockChallengeRepository.Object,
                _mockGameReferencesFactory.Object,
                _mockMatchFactory.Object,
                _mockIdentityService.Object);

            // Act
            var action = new Func<Task>(
                async () => await challengeService.RegisterParticipantAsync(new ChallengeId(), new UserId(), new UtcNowDateTimeProvider()));

            // Assert
            action.Should().Throw<InvalidOperationException>();
            _mockChallengeRepository.Verify(challengeRepository => challengeRepository.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);
            _mockGameReferencesFactory.Verify(challengeRepository => challengeRepository.CreateInstance(It.IsAny<ChallengeGame>()), Times.Never);
            _mockMatchFactory.Verify(challengeRepository => challengeRepository.CreateInstance(It.IsAny<ChallengeGame>()), Times.Never);
            _mockIdentityService.Verify(identityService => identityService.GetGameAccountIdAsync(It.IsAny<UserId>(), It.IsAny<ChallengeGame>()), Times.Never);
        }

        [Fact]
        public void RegisterParticipantAsync_GameAccountIdNullReference_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(89892334, null, ChallengeState.Inscription);

            var challenge = challengeFaker.FakeChallenge();

            _mockChallengeRepository.Setup(challengeRepository => challengeRepository.FindChallengeAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(challenge)
                .Verifiable();

            _mockIdentityService.Setup(challengeRepository => challengeRepository.GetGameAccountIdAsync(It.IsAny<UserId>(), It.IsAny<ChallengeGame>()))
                .ReturnsAsync((GameAccountId) null)
                .Verifiable();

            var challengeService = new ChallengeService(
                _mockChallengeRepository.Object,
                _mockGameReferencesFactory.Object,
                _mockMatchFactory.Object,
                _mockIdentityService.Object);

            // Act
            var action = new Func<Task>(async () => await challengeService.RegisterParticipantAsync(challenge.Id, new UserId(), new UtcNowDateTimeProvider()));

            // Assert
            action.Should().Throw<InvalidOperationException>();
            _mockChallengeRepository.Verify(challengeRepository => challengeRepository.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);
            _mockGameReferencesFactory.Verify(challengeRepository => challengeRepository.CreateInstance(It.IsAny<ChallengeGame>()), Times.Never);
            _mockMatchFactory.Verify(challengeRepository => challengeRepository.CreateInstance(It.IsAny<ChallengeGame>()), Times.Never);

            _mockIdentityService.Verify(
                challengeRepository => challengeRepository.GetGameAccountIdAsync(It.IsAny<UserId>(), It.IsAny<ChallengeGame>()),
                Times.Once);
        }

        [Fact]
        public async Task RegisterParticipantAsync_ShouldBeVerified()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(39042334, null, ChallengeState.Inscription);
            var challenge = challengeFaker.FakeChallenge();
            var participantCount = challenge.Entries - challenge.Participants.Count;
            participantCount -= 1;

            for (var index = 0; index < participantCount; index++)
            {
                challenge.Register(new Participant(new UserId(), new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider()));
            }

            _mockChallengeRepository.Setup(challengeRepository => challengeRepository.FindChallengeAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(challenge)
                .Verifiable();

            _mockChallengeRepository.Setup(challengeRepository => challengeRepository.CommitAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _mockIdentityService.Setup(identityService => identityService.GetGameAccountIdAsync(It.IsAny<UserId>(), It.IsAny<ChallengeGame>()))
                .ReturnsAsync(new GameAccountId(Guid.NewGuid().ToString()))
                .Verifiable();

            var challengeService = new ChallengeService(
                _mockChallengeRepository.Object,
                _mockGameReferencesFactory.Object,
                _mockMatchFactory.Object,
                _mockIdentityService.Object);

            // Act
            await challengeService.RegisterParticipantAsync(challenge.Id, new UserId(), new UtcNowDateTimeProvider());

            // Assert
            challenge.Timeline.State.Should().Be(ChallengeState.InProgress);
            _mockChallengeRepository.Verify(challengeRepository => challengeRepository.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);
            _mockChallengeRepository.Verify(challengeRepository => challengeRepository.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
            _mockGameReferencesFactory.Verify(challengeRepository => challengeRepository.CreateInstance(It.IsAny<ChallengeGame>()), Times.Never);
            _mockMatchFactory.Verify(challengeRepository => challengeRepository.CreateInstance(It.IsAny<ChallengeGame>()), Times.Never);
            _mockIdentityService.Verify(identityService => identityService.GetGameAccountIdAsync(It.IsAny<UserId>(), It.IsAny<ChallengeGame>()), Times.Once);
        }

        //[Fact]
        //public async Task CloseAsync_ShouldBeVerified()
        //{
        //    // Arrange
        //    var challengeFaker = new ChallengeFaker(state: ChallengeState.Ended);
        //    challengeFaker.UseSeed(86597858);
        //    var challenges = challengeFaker.Generate(5);

        //    var closedAt = new UtcNowDateTimeProvider();

        //    var mockGameReferencesAdapter = new Mock<IGameReferencesAdapter>();

        //    mockGameReferencesAdapter
        //        .Setup(
        //            gameReferencesAdapter => gameReferencesAdapter.GetGameReferencesAsync(It.IsAny<GameAccountId>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())
        //        )
        //        .ReturnsAsync(new List<GameReference>());

        //    var mockMatchAdapter = new Mock<IMatchAdapter>();

        //    mockMatchAdapter
        //        .Setup(
        //            matchAdapter => matchAdapter.GetMatchAsync(
        //                It.IsAny<GameAccountId>(),
        //                It.IsAny<GameReference>(),
        //                It.IsAny<IScoring>(),
        //                It.IsAny<IDateTimeProvider>()
        //            )
        //        )
        //        .ReturnsAsync(
        //            new StatMatch(
        //                new Scoring(),
        //                new GameStats(
        //                    new
        //                    {
        //                    }
        //                ),
        //                new GameReference(Guid.NewGuid()),
        //                closedAt
        //            )
        //        );

        //    _mockGameReferencesFactory.Setup(challengeRepository => challengeRepository.CreateInstance(It.IsAny<ChallengeGame>()))
        //        .Returns(mockGameReferencesAdapter.Object)
        //        .Verifiable();

        //    _mockMatchFactory.Setup(challengeRepository => challengeRepository.CreateInstance(It.IsAny<ChallengeGame>()))
        //        .Returns(mockMatchAdapter.Object)
        //        .Verifiable();

        //    _mockChallengeRepository
        //        .Setup(challengeRepository => challengeRepository.FetchChallengesAsync(It.IsAny<ChallengeGame>(), It.IsAny<ChallengeState>()))
        //        .ReturnsAsync(challenges)
        //        .Verifiable();

        //    _mockChallengeRepository.Setup(challengeRepository => challengeRepository.CommitAsync(It.IsAny<CancellationToken>()))
        //        .Returns(Task.CompletedTask)
        //        .Verifiable();

        //    var challengeService = new ChallengeService(
        //        _mockChallengeRepository.Object,
        //        _mockGameReferencesFactory.Object,
        //        _mockMatchFactory.Object,
        //        _mockIdentityService.Object
        //    );

        //    // Act
        //    await challengeService.CloseAsync(closedAt);

        //    // Assert
        //    challenges.Should().OnlyContain(challenge => challenge.Timeline == ChallengeState.Closed);
        //    challenges.Should().OnlyContain(challenge => challenge.Timeline.ClosedAt == closedAt.DateTime);

        //    _mockChallengeRepository.Verify(
        //        challengeRepository => challengeRepository.FetchChallengesAsync(It.IsAny<ChallengeGame>(), It.IsAny<ChallengeState>()),
        //        Times.Once
        //    );

        //    _mockChallengeRepository.Verify(
        //        challengeRepository => challengeRepository.CommitAsync(It.IsAny<CancellationToken>()),
        //        Times.Exactly(challenges.Count)
        //    );

        //    _mockGameReferencesFactory.Verify(
        //        challengeRepository => challengeRepository.CreateInstance(It.IsAny<ChallengeGame>()),
        //        Times.Exactly(challenges.Count)
        //    );

        //    _mockMatchFactory.Verify(challengeRepository => challengeRepository.CreateInstance(It.IsAny<ChallengeGame>()), Times.Exactly(challenges.Count));
        //    _mockIdentityService.Verify(identityService => identityService.GetGameAccountIdAsync(It.IsAny<UserId>(), It.IsAny<ChallengeGame>()), Times.Never);
        //}

        [Fact]
        public async Task SynchronizeAsync_ShouldBeVerified()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(86597858, ChallengeGame.LeagueOfLegends, ChallengeState.InProgress);

            var challenges = challengeFaker.FakeChallenges(5);

            var synchronizedAt = new UtcNowDateTimeProvider();

            foreach (var challenge in challenges)
            {
                challenge.Synchronize(synchronizedAt);
            }

            var mockGameReferencesAdapter = new Mock<IGameReferencesAdapter>();

            mockGameReferencesAdapter
                .Setup(
                    gameReferencesAdapter =>
                        gameReferencesAdapter.GetGameReferencesAsync(It.IsAny<GameAccountId>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(new List<GameReference>());

            var mockMatchAdapter = new Mock<IMatchAdapter>();

            mockMatchAdapter.Setup(
                    matchAdapter => matchAdapter.GetMatchAsync(
                        It.IsAny<GameAccountId>(),
                        It.IsAny<GameReference>(),
                        It.IsAny<IScoring>(),
                        It.IsAny<IDateTimeProvider>()))
                .ReturnsAsync(
                    new StatMatch(
                        new Scoring(),
                        new GameStats(
                            new
                            {
                            }),
                        new GameReference(Guid.NewGuid()),
                        synchronizedAt));

            _mockGameReferencesFactory.Setup(challengeRepository => challengeRepository.CreateInstance(It.IsAny<ChallengeGame>()))
                .Returns(mockGameReferencesAdapter.Object)
                .Verifiable();

            _mockMatchFactory.Setup(challengeRepository => challengeRepository.CreateInstance(It.IsAny<ChallengeGame>()))
                .Returns(mockMatchAdapter.Object)
                .Verifiable();

            _mockChallengeRepository
                .Setup(challengeRepository => challengeRepository.FetchChallengesAsync(It.IsAny<ChallengeGame>(), It.IsAny<ChallengeState>()))
                .ReturnsAsync(challenges)
                .Verifiable();

            _mockChallengeRepository.Setup(challengeRepository => challengeRepository.CommitAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var challengeService = new ChallengeService(
                _mockChallengeRepository.Object,
                _mockGameReferencesFactory.Object,
                _mockMatchFactory.Object,
                _mockIdentityService.Object);

            // Act
            await challengeService.SynchronizeAsync(ChallengeGame.LeagueOfLegends, TimeSpan.Zero, synchronizedAt);

            // Assert
            challenges.Should().OnlyContain(challenge => challenge.Timeline == ChallengeState.InProgress);
            challenges.Should().OnlyContain(challenge => challenge.SynchronizedAt.Value == synchronizedAt.DateTime);

            _mockChallengeRepository.Verify(
                challengeRepository => challengeRepository.FetchChallengesAsync(It.IsAny<ChallengeGame>(), It.IsAny<ChallengeState>()),
                Times.Once);

            _mockChallengeRepository.Verify(
                challengeRepository => challengeRepository.CommitAsync(It.IsAny<CancellationToken>()),
                Times.Exactly(challenges.Count));

            _mockGameReferencesFactory.Verify(
                challengeRepository => challengeRepository.CreateInstance(It.IsAny<ChallengeGame>()),
                Times.Exactly(challenges.Count));

            _mockMatchFactory.Verify(challengeRepository => challengeRepository.CreateInstance(It.IsAny<ChallengeGame>()), Times.Exactly(challenges.Count));
            _mockIdentityService.Verify(identityService => identityService.GetGameAccountIdAsync(It.IsAny<UserId>(), It.IsAny<ChallengeGame>()), Times.Never);
        }
    }
}
