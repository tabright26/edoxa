// Filename: DefaultChallengePayoutStrategyTest.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate.Strategies
{
    [TestClass]
    public sealed class DefaultChallengePayoutStrategyTest
    {
        //private static readonly ChallengeAggregateFactory ChallengeAggregateFactory = ChallengeAggregateFactory.Instance;

        //// TODO: This algorithm does not work when registration fees are higher than entries.
        ////[DataRow(100, 100D, 0.3F, 0.29F)]
        ////[DataRow(100, 500D, 0.35F, 0.3F)]
        ////[DataRow(100, 750D, 0.35F, 0.3F)]
        //[DataRow(1000, 0.25D, 0.5F, 0.21F)]
        //[DataRow(100, 10D, 0.55F, 0.2F)]
        //[DataRow(1000, 1000D, 0.4F, 0.3F)]
        //[DataRow(1000, 100D, 0.4F, 0.3F)]
        //[DataRow(1000, 500D, 0.45F, 0.3F)]
        //[DataRow(1000, 750D, 0.6F, 0.24F)]
        //[DataRow(1000, 1000D, 0.4F, 0.25F)]
        //[DataTestMethod]
        //public void Payout_Current_ShouldHaveCountOfCurrentPayoutEntries(int entries, double entryFee, float payoutRatio, float serviceChargeRatio)
        //{
        //    // Arrange
        //    var challenge = new MockChallenge(entries, entryFee, payoutRatio, serviceChargeRatio);

        //    // Act
        //    var strategy = new DefaultChallengePayoutStrategy(challenge.LiveData.PayoutEntries, challenge.LiveData.PrizePool);

        //    // Assert
        //    strategy.Payout.Should().HaveCount(challenge.LiveData.PayoutEntries.ToInt32());
        //    strategy.Payout.Sum(prize => prize.Value).Should().Be(challenge.LiveData.PrizePool.ToDecimal());
        //}

        //[DataRow(1000, 0.25D, 0.5F, 0.21F)]
        //[DataRow(100, 10D, 0.55F, 0.2F)]
        //[DataRow(100, 100D, 0.3F, 0.29F)]
        //[DataRow(100, 500D, 0.35F, 0.3F)]
        //[DataRow(100, 750D, 0.35F, 0.3F)]
        //[DataRow(1000, 1000D, 0.4F, 0.3F)]
        //[DataRow(1000, 100D, 0.4F, 0.3F)]
        //[DataRow(1000, 500D, 0.45F, 0.3F)]
        //[DataRow(1000, 750D, 0.6F, 0.24F)]
        //[DataRow(1000, 1000D, 0.4F, 0.25F)]
        //[DataTestMethod]
        //public void Payout_Potential_ShouldHaveCountOfPotentialPayoutEntries(int entries, double entryFee, float payoutRatio, float serviceChargeRatio)
        //{
        //    // Arrange
        //    var challenge = new MockChallenge(entries, entryFee, payoutRatio, serviceChargeRatio);

        //    // Act
        //    var strategy = new DefaultChallengePayoutStrategy(challenge.Settings.PayoutEntries, challenge.Settings.PrizePool);

        //    // Assert
        //    strategy.Payout.Should().HaveCount(challenge.Settings.PayoutEntries.ToInt32());
        //    strategy.Payout.Sum(prize => prize.Value).Should().Be(challenge.Settings.PrizePool.ToDecimal());
        //}

        //private static IChallengeScoringStrategy MockChallengeScoringStrategy()
        //{
        //    var mock = new Mock<IChallengeScoringStrategy>();

        //    mock.SetupGet(x => x.Scoring).Returns(ChallengeAggregateFactory.CreateChallengeScoring());

        //    return mock.Object;
        //}

        //private class MockChallenge : Challenge
        //{
        //    public MockChallenge(int entries, double entryFee, float payoutRatio, float serviceChargeRatio) : base(
        //        Game.LeagueOfLegends,
        //        new ChallengeName(nameof(MockChallenge)),
        //        new ChallengeSettings(BestOf.Default.ToInt32(), entries, (decimal) entryFee, payoutRatio, serviceChargeRatio)
        //    )
        //    {
        //        this.Publish(MockChallengeScoringStrategy());

        //        var currentEntries = entries / 2;

        //        for (var index = 0; index < currentEntries; index++)
        //        {
        //            this.RegisterParticipant(new UserId(), LinkedAccount.FromGuid(Guid.NewGuid()));
        //        }
        //    }
        //}
    }
}