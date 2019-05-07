// Filename: DefaultScoreboardStrategyTest.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate.Strategies;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ParticipantAggregate;
using eDoxa.Challenges.Domain.Entities.AggregateModels.UserAggregate;
using eDoxa.Challenges.Domain.Entities.Factories;
using eDoxa.Seedwork.Enumerations;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Entities.Tests.AggregateModels.ChallengeAggregate.Strategies
{
    [TestClass]
    public sealed class DefaultScoreboardStrategyTest
    {
        private static readonly ChallengeAggregateFactory ChallengeAggregateFactory = ChallengeAggregateFactory.Instance;

        [TestMethod]
        public void Scoreboard_Default_ShouldBeEmpty()
        {
            // Arrange
            var challenge = new MockChallenge();

            // Act
            var strategy = new DefaultScoreboardStrategy(challenge);

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

        private class MockChallenge : Challenge
        {
            public MockChallenge(Entries entries, BestOf bestOf) : base(
                Game.LeagueOfLegends,
                new ChallengeName(nameof(Challenge)),
                new ChallengeSetup(
                    bestOf,
                    entries,
                    EntryFee.DefaultValue,
                    PayoutRatio.DefaultValue,
                    ServiceChargeRatio.DefaultValue
                )
            )
            {
                this.Publish(ChallengeAggregateFactory.Instance.CreateScoringStrategy());

                for (var i = 0; i < Setup.Entries; i++)
                {
                    var userId = new UserId();

                    this.RegisterParticipant(userId, new LinkedAccount(Guid.NewGuid()));

                    var participant = Participants.Single(x => x.UserId == userId);

                    var random = new Random();

                    for (var j = 0; j < random.Next(0, Setup.BestOf + 10); j++)
                    {
                        this.SnapshotParticipantMatch(participant.Id, ChallengeAggregateFactory.CreateMatchStats());
                    }
                }
            }

            public MockChallenge() : base(Game.LeagueOfLegends, new ChallengeName(nameof(Challenge)), new DefaultChallengeSetup())
            {
            }
        }
    }
}