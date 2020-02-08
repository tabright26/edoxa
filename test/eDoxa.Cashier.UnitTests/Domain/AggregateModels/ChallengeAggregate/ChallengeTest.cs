// Filename: ChallengeTest.cs
// Date Created: 2020-02-08
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Cashier.Api.Application.Factories;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.DomainEvents;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeTest : UnitTest
    {
        public ChallengeTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(testData, testMapper, testValidator)
        {
        }

        public static TheoryData<Dictionary<UserId, decimal?>> ChallengeTestData =>
            new TheoryData<Dictionary<UserId, decimal?>>
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
                new Dictionary<UserId, decimal?>
                {
                    [new UserId()] = 100,
                    [new UserId()] = 75,
                    [new UserId()] = 50,
                    [new UserId()] = 25,
                    [new UserId()] = null,
                    [new UserId()] = null
                },
                new Dictionary<UserId, decimal?>
                {
                    [new UserId()] = 100,
                    [new UserId()] = 75,
                    [new UserId()] = null,
                    [new UserId()] = null,
                    [new UserId()] = null,
                    [new UserId()] = null
                }
            };

        [Theory]
        [MemberData(nameof(ChallengeTestData))]
        public void Close_DomainEvents_ShouldHaveCountOfParticipantWithScore(Dictionary<UserId, decimal?> scoreboard)
        {
            // Arrange
            var factory = new ChallengePayoutFactory();
            var instance = factory.CreateInstance();

            var challenge = new Challenge(
                new ChallengeId(),
                instance.GetChallengePayout(new ChallengePayoutEntries(scoreboard.Count / 2), new EntryFee(100, CurrencyType.Money)));

            var challengeScoreboard = new ChallengeScoreboard(challenge.Payout, scoreboard);
            var participantCount = scoreboard.Count(participant => participant.Value != null);

            // Act
            challenge.Close(challengeScoreboard);

            // Assert
            challenge.DomainEvents.Where(domainEvent => domainEvent is ChallengeParticipantPayoutDomainEvent).Should().HaveCount(participantCount);
        }
    }
}
