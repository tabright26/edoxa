// Filename: LeagueOfLegendsChallengeStatsAdapterTest.cs
// Date Created: 2019-03-05
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Services.Factories;
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.Adapters;
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.DTO.Tests;
using eDoxa.Seedwork.Domain.Common.Enums;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.Tests.Adapters
{
    [TestClass]
    public sealed class LeagueOfLegendsChallengeStatsAdapterTest
    {
        [TestMethod]
        public void SnapshotParticipantMatch_ShouldBeValid()
        {
            // Arrange
            var matches = LeagueOfLegendsDTO.DeserializeMatches();
            var userId = new UserId();
            var linkedAccount = LinkedAccount.Parse("NzH50JS-LCAu0UEY4EMjuS710F_U_8pLfEpNib9X06dD4w");
            var challenge = new MockChallenge();
            challenge.RegisterParticipant(userId, linkedAccount);

            foreach (var (_, match) in matches)
            {
                var adapter = new LeagueOfLegendsChallengeStatsAdapter(linkedAccount, match);

                challenge.SnapshotParticipantMatch(challenge.Participants.Single(x => x.UserId == userId).Id, adapter.Stats);
            }

            // Act => Assert
            challenge.Participants.Single(x => x.UserId == userId).Matches.Should().HaveCount(5);
        }

        private sealed class MockChallenge : Challenge
        {
            public MockChallenge() : base(Game.LeagueOfLegends, new ChallengeName(nameof(MockChallenge)))
            {
                var factory = ChallengeScoringFactory.Instance;

                var strategy = factory.Create(this);

                this.Publish(strategy);
            }
        }
    }
}