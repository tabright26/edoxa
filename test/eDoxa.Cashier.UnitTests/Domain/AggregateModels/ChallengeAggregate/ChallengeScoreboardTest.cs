// Filename: ChallengeScoreboardTest.cs
// Date Created: 2020-02-08
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Cashier.Api.Application.Factories;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeScoreboardTest : UnitTest
    {
        public ChallengeScoreboardTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        public static TheoryData<Dictionary<UserId, decimal?>, int, int> ChallengeScoreboardTheoryData =>
            new TheoryData<Dictionary<UserId, decimal?>, int, int>
            {
                {
                    new Dictionary<UserId, decimal?>
                    {
                        [new UserId()] = 100,
                        [new UserId()] = 75,
                        [new UserId()] = 50,
                        [new UserId()] = 25,
                        [new UserId()] = 10,
                        [new UserId()] = 5
                    },
                    3, 3
                },
                {
                    new Dictionary<UserId, decimal?>
                    {
                        [new UserId()] = 100,
                        [new UserId()] = 75,
                        [new UserId()] = 50,
                        [new UserId()] = 25,
                        [new UserId()] = null,
                        [new UserId()] = null
                    },
                    3, 1
                },
                {
                    new Dictionary<UserId, decimal?>
                    {
                        [new UserId()] = 100,
                        [new UserId()] = 75,
                        [new UserId()] = null,
                        [new UserId()] = null,
                        [new UserId()] = null,
                        [new UserId()] = null
                    },
                    2, 0
                }
            };

        [Theory]
        [MemberData(nameof(ChallengeScoreboardTheoryData))]
        public void ChallengeScoreboard_Constructor_ShouldHaveCount(Dictionary<UserId, decimal?> scoreboard, int winnerCount, int loserCount)
        {
            // Arrange
            var factory = new ChallengePayoutFactory();
            var instance = factory.CreateInstance();
            var entries = scoreboard.Count;
            var payoutEntries = entries / 2;

            // Act
            var challengeScoreboard = new ChallengeScoreboard(
                instance.GetChallengePayout(new ChallengePayoutEntries(payoutEntries), new EntryFee(100, CurrencyType.Money)),
                scoreboard);

            // Assert
            challengeScoreboard.Winners.Should().HaveCount(winnerCount);
            challengeScoreboard.Losers.Should().HaveCount(loserCount);
        }
    }
}
