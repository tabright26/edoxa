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

using eDoxa.Arena.Services.LeagueOfLegends.DTO.Tests;
using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Challenges.Domain.Factories;
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.Adapters;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.Tests.Adapters
{
    [TestClass]
    public sealed class LeagueOfLegendsChallengeStatsAdapterTest
    {
        private static readonly FakeDefaultChallengeFactory FakeDefaultChallengeFactory = FakeDefaultChallengeFactory.Instance;

        [TestMethod]
        public void SnapshotParticipantMatch_ShouldBeValid()
        {
            // Arrange
            var matches = LeagueOfLegendsDTO.DeserializeMatches();

            var userId = new UserId();

            var linkedAccount = new LinkedAccount("NzH50JS-LCAu0UEY4EMjuS710F_U_8pLfEpNib9X06dD4w");

            var challenge = FakeDefaultChallengeFactory.CreateChallenge();

            challenge.RegisterParticipant(userId, linkedAccount);

            var timeline = FakeDefaultChallengeFactory.CreateChallengeTimeline(ChallengeState.InProgress);

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