// Filename: DefaultChallengePrizeBreakdownStrategyTest.cs
// Date Created: 2019-03-05
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Strategies;
using eDoxa.Challenges.Domain.Factories;
using eDoxa.Seedwork.Domain.Common.Enums;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate.Strategies
{
    [TestClass]
    public sealed class DefaultChallengePrizeBreakdownStrategyTest
    {
        private static readonly ChallengeAggregateFactory _factory = ChallengeAggregateFactory.Instance;

        //[TestMethod]
        //public void Constructor_NullReference_ShouldThrowArgumentNullException()
        //{
        //    // Act
        //    var action = new Action(() => new DefaultChallengePrizeBreakdownStrategy(null));

        //    // Assert
        //    action.Should().Throw<ArgumentNullException>();
        //}

        // TODO: This algorithm does not work when registration fees are higher than entries.
        //[DataRow(100, 100D, 0.3F, 0.29F)]
        //[DataRow(100, 500D, 0.35F, 0.3F)]
        //[DataRow(100, 750D, 0.35F, 0.3F)]
        [DataRow(1000, 0.25D, 0.5F, 0.21F)]
        [DataRow(100, 10D, 0.55F, 0.2F)]
        [DataRow(1000, 1000D, 0.4F, 0.3F)]
        [DataRow(1000, 100D, 0.4F, 0.3F)]
        [DataRow(1000, 500D, 0.45F, 0.3F)]
        [DataRow(1000, 750D, 0.6F, 0.24F)]
        [DataRow(1000, 1000D, 0.4F, 0.25F)]
        [DataTestMethod]
        public void PrizeBreakdown_Current_ShouldHaveCountOfCurrentPayoutEntries(int entries, double entryFee, float payoutRatio, float serviceChargeRatio)
        {
            // Arrange
            var challenge = new MockChallenge(entries, entryFee, payoutRatio, serviceChargeRatio);

            // Act
            var strategy = new DefaultChallengePrizeBreakdownStrategy(challenge.LiveData.PayoutEntries, challenge.LiveData.PrizePool);

            // Assert
            strategy.PrizeBreakdown.Should().HaveCount(challenge.LiveData.PayoutEntries);
            strategy.PrizeBreakdown.Sum(prize => prize.Value).Should().Be(challenge.LiveData.PrizePool);
        }

        [DataRow(1000, 0.25D, 0.5F, 0.21F)]
        [DataRow(100, 10D, 0.55F, 0.2F)]
        [DataRow(100, 100D, 0.3F, 0.29F)]
        [DataRow(100, 500D, 0.35F, 0.3F)]
        [DataRow(100, 750D, 0.35F, 0.3F)]
        [DataRow(1000, 1000D, 0.4F, 0.3F)]
        [DataRow(1000, 100D, 0.4F, 0.3F)]
        [DataRow(1000, 500D, 0.45F, 0.3F)]
        [DataRow(1000, 750D, 0.6F, 0.24F)]
        [DataRow(1000, 1000D, 0.4F, 0.25F)]
        [DataTestMethod]
        public void PrizeBreakdown_Potential_ShouldHaveCountOfPotentialPayoutEntries(int entries, double entryFee, float payoutRatio, float serviceChargeRatio)
        {
            // Arrange
            var challenge = new MockChallenge(entries, entryFee, payoutRatio, serviceChargeRatio);

            // Act
            var strategy = new DefaultChallengePrizeBreakdownStrategy(challenge.Settings.PayoutEntries, challenge.Settings.PrizePool);

            // Assert
            strategy.PrizeBreakdown.Should().HaveCount(challenge.Settings.PayoutEntries);
            strategy.PrizeBreakdown.Sum(prize => prize.Value).Should().Be(challenge.Settings.PrizePool);
        }

        private static IChallengeScoringStrategy MockChallengeScoringStrategy()
        {
            var mock = new Mock<IChallengeScoringStrategy>();

            mock.SetupGet(x => x.Scoring).Returns(_factory.CreateChallengeScoring());

            return mock.Object;
        }

        private class MockChallenge : Challenge
        {
            public MockChallenge(int entries, double entryFee, float payoutRatio, float serviceChargeRatio) : base(
                Game.LeagueOfLegends,
                new ChallengeName(nameof(MockChallenge)),
                new ChallengeSettings(ChallengeSettings.DefaultBestOf, entries, (decimal) entryFee, payoutRatio, serviceChargeRatio)
            )
            {
                this.Publish(MockChallengeScoringStrategy());

                var currentEntries = entries / 2;

                for (var index = 0; index < currentEntries; index++)
                {
                    this.RegisterParticipant(new UserId(), LinkedAccount.FromGuid(Guid.NewGuid()));
                }
            }
        }
    }
}