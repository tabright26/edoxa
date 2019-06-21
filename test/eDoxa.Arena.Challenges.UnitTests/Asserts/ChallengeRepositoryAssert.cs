// Filename: ChallengeRepositoryAssert.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using FluentAssertions;

namespace eDoxa.Arena.Challenges.UnitTests.Asserts
{
    public static class ChallengeRepositoryAssert
    {
        internal static void IsLoaded(IEnumerable<Challenge> challenges)
        {
            foreach (var challenge in challenges)
            {
                IsLoaded(challenge);
            }
        }

        internal static void IsLoaded(Challenge challenge)
        {
            challenge.Should().NotBeNull();
            challenge.Game.Should().NotBe(null);
            challenge.Name.ToString().Should().NotBeNullOrWhiteSpace();
            challenge.Setup.Should().NotBeNull();
            challenge.Participants.Should().NotBeNullOrEmpty();

            foreach (var participant in challenge.Participants)
            {
                participant.RegisteredAt.Should().BeBefore(DateTime.UtcNow);
                participant.GameAccountId.ToString().Should().NotBeNullOrWhiteSpace();
                participant.UserId.ToGuid().Should().NotBeEmpty();
                participant.Matches.Should().NotBeNullOrEmpty();

                foreach (var match in participant.Matches)
                {
                    match.SynchronizedAt.Should().BeBefore(DateTime.UtcNow);
                    match.GameMatchId.ToString().Should().NotBeNullOrWhiteSpace();
                    match.TotalScore.Should().NotBeNull();
                    match.Stats.Should().NotBeNullOrEmpty();

                    foreach (var stat in match.Stats)
                    {
                        stat.Name.Should().NotBeNull();
                        stat.Score.Should().NotBeNull();
                    }
                }
            }
        }
    }
}
