// Filename: LeagueOfLegendsChallengeStatsAdapterTest.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Reflection;

using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ParticipantAggregate;
using eDoxa.Challenges.Domain.Entities.AggregateModels.UserAggregate;
using eDoxa.Challenges.Domain.Entities.Factories;
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.Adapters;
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.DTO.Tests;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.Tests.Adapters
{
    [TestClass]
    public sealed class LeagueOfLegendsChallengeStatsAdapterTest
    {
        private static readonly ChallengeAggregateFactory ChallengeAggregateFactory = ChallengeAggregateFactory.Instance;

        [TestMethod]
        public void SnapshotParticipantMatch_ShouldBeValid()
        {
            // Arrange
            var matches = LeagueOfLegendsDTO.DeserializeMatches();

            var userId = new UserId();

            var linkedAccount = new LinkedAccount("NzH50JS-LCAu0UEY4EMjuS710F_U_8pLfEpNib9X06dD4w");

            var challenge = ChallengeAggregateFactory.CreateChallenge();

            challenge.RegisterParticipant(userId, linkedAccount);

            var timeline = ChallengeAggregateFactory.CreateChallengeTimeline(ChallengeState1.InProgress);

            challenge.GetType().GetField("_timeline", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(challenge, timeline);

            foreach (var (_, match) in matches)
            {
                var adapter = new LeagueOfLegendsMatchStatsAdapter(linkedAccount, match);

                challenge.SnapshotParticipantMatch(challenge.Participants.Single(x => x.UserId == userId).Id, adapter.MatchStats);
            }

            // Act => Assert
            challenge.Participants.Single(x => x.UserId == userId).Matches.Should().HaveCount(5);
        }
    }
}