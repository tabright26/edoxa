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
using eDoxa.Seedwork.TestHelper.Mocks;

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
                challenge.Register(new Participant(new ParticipantId(), new UserId(), PlayerId.Parse(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider()));
            }

            var mockChallengeRepository = new Mock<IChallengeRepository>();
            var mockGamesHttpClient = new Mock<IGamesHttpClient>();
            var mockLogger = new MockLogger<ChallengeService>();

            mockChallengeRepository.Setup(challengeRepository => challengeRepository.FindChallengeAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(challenge)
                .Verifiable();

            mockChallengeRepository.Setup(challengeRepository => challengeRepository.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockGamesHttpClient
                .Setup(challengeRepository => challengeRepository.GetChallengeMatchesAsync(It.IsAny<Game>(), It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>()))
                .ReturnsAsync(
                    new List<MatchDto>
                    {
                        new MatchDto(string.Empty, new Dictionary<string, double>())
                    })
                .Verifiable();

            var challengeService = new ChallengeService(mockChallengeRepository.Object, mockGamesHttpClient.Object, mockLogger.Object);

            // Act
            await challengeService.RegisterChallengeParticipantAsync(
                challenge,
                new ParticipantId(),
                new UserId(),
                new PlayerId(),
                new UtcNowDateTimeProvider());

            // Assert
            challenge.Timeline.State.Should().Be(ChallengeState.InProgress);
            mockChallengeRepository.Verify(challengeRepository => challengeRepository.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Never);
            mockChallengeRepository.Verify(challengeRepository => challengeRepository.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
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

            var mockChallengeRepository = new Mock<IChallengeRepository>();
            var mockGamesHttpClient = new Mock<IGamesHttpClient>();
            var mockLogger = new MockLogger<ChallengeService>();

            mockChallengeRepository.Setup(challengeRepository => challengeRepository.FetchChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()))
                .ReturnsAsync(challenges)
                .Verifiable();

            mockChallengeRepository.Setup(challengeRepository => challengeRepository.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockGamesHttpClient
                .Setup(challengeRepository => challengeRepository.GetChallengeMatchesAsync(It.IsAny<Game>(), It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>()))
                .ReturnsAsync(new List<MatchDto>())
                .Verifiable();

            var challengeService = new ChallengeService(mockChallengeRepository.Object, mockGamesHttpClient.Object, mockLogger.Object);

            // Act
            await challengeService.SynchronizeChallengesAsync(Game.LeagueOfLegends, synchronizedAt);

            // Assert
            challenges.Should().OnlyContain(challenge => challenge.Timeline == ChallengeState.InProgress);
            challenges.Should().OnlyContain(challenge => challenge.SynchronizedAt.Value == synchronizedAt.DateTime);

            mockChallengeRepository.Verify(
                challengeRepository => challengeRepository.FetchChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()),
                Times.Exactly(2));

            mockChallengeRepository.Verify(
                challengeRepository => challengeRepository.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()),
                Times.Exactly(challenges.SelectMany(challenge => challenge.Participants).Count() + challenges.Count));
        }
    }
}
