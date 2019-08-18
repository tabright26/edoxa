// Filename: ChallengeTest.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ChallengeTest
    {
        private static IEnumerable<object[]> ChallengeStateDataSets =>
            ChallengeState.GetEnumerations().Where(state => state != ChallengeState.Inscription).Select(state => new object[] {state}).ToList();

        //private static IEnumerable<object[]> ChallengeDataSets =>
        //    ChallengeGame.GetEnumerations()
        //        .SelectMany(
        //            game => ValueObject.GetValues<ChallengeDuration>()
        //                .SelectMany(
        //                    duration => ValueObject.GetValues<PayoutEntries>()
        //                        .SelectMany(
        //                            payoutEntries => ValueObject.GetValues<BestOf>()
        //                                .SelectMany(
        //                                    bestOf => EntryFee.GetValues()
        //                                        .Select(
        //                                            entryFee => new object[]
        //                                            {
        //                                                new ChallengeName(nameof(Challenge)),
        //                                                game,
        //                                                new ChallengeSetup(bestOf, payoutEntries, entryFee),
        //                                                new ChallengeTimeline(new UtcNowDateTimeProvider(), duration),
        //                                                new ScoringFactory().CreateInstance(game).Scoring,
        //                                                new PayoutFactory(new PayoutStrategy()).CreateInstance().GetPayout(payoutEntries, entryFee)
        //                                            }
        //                                        )
        //                                )
        //                        )
        //                )
        //        );

        //[DataTestMethod]
        //[DynamicData(nameof(ChallengeDataSets))]
        //public void Constructor_ShouldNotThrow(
        //    ChallengeName name,
        //    ChallengeGame game,
        //    ChallengeSetup setup,
        //    ChallengeTimeline timeline,
        //    IScoring scoring,
        //    IPayout payout
        //)
        //{
        //    // Arrange
        //    var action = new Action(
        //        () =>
        //        {
        //            // Act
        //            var challenge = new Challenge(
        //                name,
        //                game,
        //                setup,
        //                timeline,
        //                scoring,
        //                payout
        //            );

        //            challenge.DumbAsJson(true);
        //        }
        //    );

        //    // Assert
        //    action.Should().NotThrow();
        //}

        [TestMethod]
        public void Register_Participant_ShouldBeRegistered()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(state: ChallengeState.Inscription);
            challengeFaker.UseSeed(85256956);
            var challenge = challengeFaker.Generate();
            var participantCount = challenge.Participants.Count;

            // Act
            challenge.Register(new Participant(new UserId(), new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider()));

            // Assert
            challenge.Participants.Should().HaveCount(participantCount + 1);
        }

        [DataTestMethod]
        [DynamicData(nameof(ChallengeStateDataSets))]
        public void Register_ChallengeStateNotInscription_ShouldThrowInvalidOperationException(ChallengeState state)
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(state: state);
            challengeFaker.UseSeed(78536956);
            var challenge = challengeFaker.Generate();

            // Act
            var action = new Action(
                () => challenge.Register(new Participant(new UserId(), new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider()))
            );

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void Register_RegisteredParticipant_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(state: ChallengeState.Inscription);
            challengeFaker.UseSeed(48536956);
            var challenge = challengeFaker.Generate();

            // Act
            var action = new Action(
                () => challenge.Register(
                    new Participant(challenge.Participants.First().UserId, new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider())
                )
            );

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void Register_WhenInscriptionFulfilled_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(state: ChallengeState.Inscription);
            challengeFaker.UseSeed(43897896);
            var challenge = challengeFaker.Generate();
            var participantCount = challenge.Entries - challenge.Participants.Count;

            for (var index = 0; index < participantCount; index++)
            {
                challenge.Register(new Participant(new UserId(), new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider()));
            }

            // Act
            var action = new Action(
                () => challenge.Register(new Participant(new UserId(), new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider()))
            );

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }
    }
}
