// Filename: ChallengeTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.UnitTests.TestHelpers;
using eDoxa.Seedwork.Domain;

using FluentAssertions;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeTest : UnitTest
    {
        public ChallengeTest(ChallengeFakerFixture challengeFaker) : base(challengeFaker)
        {
        }

        public static IEnumerable<object[]> ChallengeStateDataSets =>
            ChallengeState.GetEnumerations().Where(state => state != ChallengeState.Inscription).Select(state => new object[] {state}).ToList();

        [MemberData(nameof(ChallengeStateDataSets))]
        public void Register_WhenStateNotInscription_ShouldThrowInvalidOperationException(ChallengeState state)
        {
            // Arrange
            var challengeFaker = ChallengeFaker.Factory.CreateFaker(78536956, state: state);

            var challenge = challengeFaker.FakeChallenge();

            // Act
            var action = new Action(
                () => challenge.Register(new Participant(new UserId(), new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider())));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Register_WhenInscriptionFulfilled_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challengeFaker = ChallengeFaker.Factory.CreateFaker(43897896, null, ChallengeState.Inscription);

            var challenge = challengeFaker.FakeChallenge();

            var participantCount = challenge.Entries - challenge.Participants.Count;

            for (var index = 0; index < participantCount; index++)
            {
                challenge.Register(new Participant(new UserId(), new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider()));
            }

            // Act
            var action = new Action(
                () => challenge.Register(new Participant(new UserId(), new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider())));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Register_WhenParticipantIsRegistered_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challengeFaker = ChallengeFaker.Factory.CreateFaker(48536956, null, ChallengeState.Inscription);

            var challenge = challengeFaker.FakeChallenge();

            // Act
            var action = new Action(
                () => challenge.Register(
                    new Participant(challenge.Participants.First().UserId, new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider())));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Register_WhenStateInscription_ShouldHaveOneMore()
        {
            // Arrange
            var challengeFaker = ChallengeFaker.Factory.CreateFaker(85256956, null, ChallengeState.Inscription);

            var challenge = challengeFaker.FakeChallenge();

            var participantCount = challenge.Participants.Count;

            // Act
            challenge.Register(new Participant(new UserId(), new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider()));

            // Assert
            challenge.Participants.Should().HaveCount(participantCount + 1);
        }
    }
}
