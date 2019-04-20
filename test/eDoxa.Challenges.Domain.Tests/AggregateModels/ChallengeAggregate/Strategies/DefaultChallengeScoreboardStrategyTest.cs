// Filename: DefaultChallengeScoreboardStrategyTest.cs
// Date Created: 2019-03-05
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Strategies;
using eDoxa.Challenges.Domain.Factories;
using eDoxa.Challenges.Domain.ValueObjects;
using eDoxa.Seedwork.Domain.Common.Enums;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate.Strategies
{
    [TestClass]
    public sealed class DefaultChallengeScoreboardStrategyTest
    {
        private static readonly ChallengeAggregateFactory ChallengeAggregateFactory = ChallengeAggregateFactory.Instance;

        [TestMethod]
        public void Scoreboard_Default_ShouldBeEmpty()
        {
            // Arrange
            var challenge = new MockChallenge();

            // Act
            var strategy = new DefaultChallengeScoreboardStrategy(challenge);

            // Assert
            strategy.Scoreboard.Should().BeEmpty();
        }

        //[DataRow(50, 3)]
        //[DataRow(100, 3)]
        //[DataRow(1000, 5)]
        //[DataRow(500, 7)]
        //[DataTestMethod]
        //public void Scoreboard_MaxEntries_ShouldHaveCountOf(int entries, int bestOf)
        //{
        //    // Arrange
        //    var challenge = new MockChallenge(entries, bestOf);

        //    // Act
        //    var strategy = new DefaultChallengeScoreboardStrategy(challenge);

        //    // Assert
        //    strategy.Scoreboard.Should().HaveCount(challenge.Settings.Entries.ToInt32());
        //    strategy.Scoreboard.As<IReadOnlyDictionary<UserId, Score>>().Should().BeInDescendingOrder(participant => participant.Value);
        //}

        private static IChallengeScoringStrategy MockChallengeScoringStrategy()
        {
            var mock = new Mock<IChallengeScoringStrategy>();

            mock.SetupGet(strategy => strategy.Scoring).Returns(ChallengeAggregateFactory.CreateChallengeScoring());

            return mock.Object;
        }

        private class MockChallenge : Challenge
        {
            public MockChallenge(int entries, int bestOf) : base(
                Game.LeagueOfLegends,
                new ChallengeName(nameof(Challenge)),
                new ChallengeSettings(
                    bestOf,
                    entries,
                    EntryFee.Default.ToDecimal(),
                    PayoutRatio.Default.ToSingle(),
                    ServiceChargeRatio.Default.ToSingle()
                )
            )
            {
                this.Publish(MockChallengeScoringStrategy());

                for (var i = 0; i < Settings.Entries.ToInt32(); i++)
                {
                    var userId = new UserId();

                    var participant = this.RegisterParticipant(userId, LinkedAccount.FromGuid(Guid.NewGuid()));

                    var random = new Random();

                    for (var j = 0; j < random.Next(0, Settings.BestOf.ToInt32() + 10); j++)
                    {
                        this.SnapshotParticipantMatch(participant.Id, ChallengeAggregateFactory.CreateChallengeStats());
                    }
                }
            }

            public MockChallenge() : base(Game.LeagueOfLegends, new ChallengeName(nameof(Challenge)))
            {
            }
        }
    }
}