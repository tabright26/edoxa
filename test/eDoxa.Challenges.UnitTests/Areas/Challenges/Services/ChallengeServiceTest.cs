// Filename: ChallengeServiceTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Areas.Challenges.Services;
using eDoxa.Challenges.Api.HttpClients;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Dtos;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Moq;

using Xunit;

namespace eDoxa.Challenges.UnitTests.Areas.Challenges.Services
{
    public sealed class ChallengeServiceTest : UnitTest
    {
        public ChallengeServiceTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task RegisterParticipantAsync_ShouldBeVerified()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(39042334, Game.LeagueOfLegends, ChallengeState.Inscription);
            var challenge = challengeFaker.FakeChallenge();
            var participantCount = challenge.Entries - challenge.Participants.Count;
            participantCount -= 1;

            for (var index = 0; index < participantCount; index++)
            {
                challenge.Register(new Participant(new UserId(), PlayerId.Parse(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider()));
            }

            var _mockChallengeRepository = new Mock<IChallengeRepository>();
            var _mockGamesApiRefitClient = new Mock<IGamesHttpClient>();

            _mockChallengeRepository.Setup(challengeRepository => challengeRepository.FindChallengeAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(challenge)
                .Verifiable();

            _mockChallengeRepository.Setup(challengeRepository => challengeRepository.CommitAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _mockGamesApiRefitClient
                .Setup(challengeRepository => challengeRepository.GetChallengeMatchesAsync(It.IsAny<Game>(), It.IsAny<PlayerId>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>()))
                .ReturnsAsync(
                    new List<MatchDto>
                    {
                        new MatchDto(string.Empty, DateTime.UtcNow, new Dictionary<string, double>())
                    })
                .Verifiable();

            var challengeService = new ChallengeService(_mockChallengeRepository.Object, _mockGamesApiRefitClient.Object);

            // Act
            await challengeService.RegisterParticipantAsync(
                challenge,
                new UserId(),
                new PlayerId(),
                new UtcNowDateTimeProvider());

            // Assert
            challenge.Timeline.State.Should().Be(ChallengeState.InProgress);
            _mockChallengeRepository.Verify(challengeRepository => challengeRepository.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Never);
            _mockChallengeRepository.Verify(challengeRepository => challengeRepository.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task SynchronizeAsync_ShouldBeVerified()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(86597858, Game.LeagueOfLegends, ChallengeState.InProgress);

            var challenges = challengeFaker.FakeChallenges(5);

            var synchronizedAt = new UtcNowDateTimeProvider();

            foreach (var challenge in challenges)
            {
                challenge.Synchronize(synchronizedAt);
            }

            var _mockChallengeRepository = new Mock<IChallengeRepository>();
            var _mockGamesApiRefitClient = new Mock<IGamesHttpClient>();

            _mockChallengeRepository.Setup(challengeRepository => challengeRepository.FetchChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()))
                .ReturnsAsync(challenges)
                .Verifiable();

            _mockChallengeRepository.Setup(challengeRepository => challengeRepository.CommitAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _mockGamesApiRefitClient
                .Setup(challengeRepository => challengeRepository.GetChallengeMatchesAsync(It.IsAny<Game>(), It.IsAny<PlayerId>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>()))
                .ReturnsAsync(new List<MatchDto>())
                .Verifiable();

            var challengeService = new ChallengeService(_mockChallengeRepository.Object, _mockGamesApiRefitClient.Object);

            // Act
            await challengeService.SynchronizeAsync(Game.LeagueOfLegends, TimeSpan.Zero, synchronizedAt);

            // Assert
            challenges.Should().OnlyContain(challenge => challenge.Timeline == ChallengeState.InProgress);
            challenges.Should().OnlyContain(challenge => challenge.SynchronizedAt.Value == synchronizedAt.DateTime);

            _mockChallengeRepository.Verify(
                challengeRepository => challengeRepository.FetchChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()),
                Times.Once);

            _mockChallengeRepository.Verify(
                challengeRepository => challengeRepository.CommitAsync(It.IsAny<CancellationToken>()),
                Times.Exactly(challenges.SelectMany(x => x.Participants).Count()));
        }
    }
}
