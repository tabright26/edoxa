// Filename: ChallengeTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Xunit;

namespace eDoxa.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeTest : UnitTest
    {
        public ChallengeTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        public static TheoryData<ChallengeState> ChallengeStateDataSets =>
            new TheoryData<ChallengeState>
            {
                ChallengeState.InProgress,
                ChallengeState.Ended,
                ChallengeState.Closed
            };

        [Theory]
        [MemberData(nameof(ChallengeStateDataSets))]
        public void Register_WhenStateNotInscription_ShouldThrowInvalidOperationException(ChallengeState state)
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(78536956, Game.LeagueOfLegends, state);

            var challenge = challengeFaker.FakeChallenge();

            // Act
            var action = new Action(
                () => challenge.Register(new Participant(new UserId(), PlayerId.Parse(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider())));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Register_WhenInscriptionFulfilled_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(43897896, Game.LeagueOfLegends, ChallengeState.Inscription);

            var challenge = challengeFaker.FakeChallenge();

            var participantCount = challenge.Entries - challenge.Participants.Count;

            for (var index = 0; index < participantCount; index++)
            {
                challenge.Register(new Participant(new UserId(), PlayerId.Parse(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider()));
            }

            // Act
            var action = new Action(
                () => challenge.Register(new Participant(new UserId(), PlayerId.Parse(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider())));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Register_WhenParticipantIsRegistered_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(48536956, Game.LeagueOfLegends, ChallengeState.Inscription);

            var challenge = challengeFaker.FakeChallenge();

            // Act
            var action = new Action(
                () => challenge.Register(
                    new Participant(challenge.Participants.First().UserId, PlayerId.Parse(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider())));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Register_WhenStateInscription_ShouldHaveOneMore()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(85256956, Game.LeagueOfLegends, ChallengeState.Inscription);

            var challenge = challengeFaker.FakeChallenge();

            var participantCount = challenge.Participants.Count;

            // Act
            challenge.Register(new Participant(new UserId(), PlayerId.Parse(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider()));

            // Assert
            challenge.Participants.Should().HaveCount(participantCount + 1);
        }
    }
}
