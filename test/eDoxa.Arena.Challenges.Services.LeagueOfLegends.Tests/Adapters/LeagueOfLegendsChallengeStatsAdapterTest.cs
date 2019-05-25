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

using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Services.LeagueOfLegends.Adapters;
using eDoxa.Arena.Challenges.Tests.Factories;
using eDoxa.Arena.Services.LeagueOfLegends.DTO.Tests;
using eDoxa.Seedwork.Domain.Entities;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.Services.LeagueOfLegends.Tests.Adapters
{
    [TestClass]
    public sealed class LeagueOfLegendsChallengeStatsAdapterTest
    {
        private static readonly FakeChallengeFactory FakeChallengeFactory = FakeChallengeFactory.Instance;

        [TestMethod]
        public void SnapshotParticipantMatch_ShouldBeValid()
        {
            // Arrange
            var matches = LeagueOfLegendsDTO.DeserializeMatches();

            var userId = new UserId();

            var externalAccount = new ParticipantExternalAccount("NzH50JS-LCAu0UEY4EMjuS710F_U_8pLfEpNib9X06dD4w");

            var challenge = FakeChallengeFactory.CreateChallenge();

            challenge.RegisterParticipant(userId, externalAccount);

            //var timeline = FakeChallengeFactory.CreateChallengeTimeline(ChallengeState.InProgress);

            //challenge.GetType().GetField("_timeline", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(challenge, timeline);

            foreach (var (_, match) in matches)
            {
                var adapter = new LeagueOfLegendsMatchStatsAdapter(externalAccount, match);

                challenge.SnapshotParticipantMatch(challenge.Participants.Single(x => x.UserId == userId).Id, adapter.MatchStats);
            }

            // Act => Assert
            challenge.Participants.Single(x => x.UserId == userId).Matches.Should().HaveCount(5);
        }
    }
}