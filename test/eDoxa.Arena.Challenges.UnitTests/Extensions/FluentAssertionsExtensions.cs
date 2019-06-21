// Filename: FluentAssertionsExtensions.cs
// Date Created: 2019-06-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Domain.Extensions;

using FluentAssertions;

namespace eDoxa.Arena.Challenges.UnitTests.Extensions
{
    public static class FluentAssertionsExtensions
    {
        public static void ShouldBeValidObjectState(this IEnumerable<IChallenge> challenges)
        {
            challenges.ForEach(challenge => challenge.ShouldBeValidObjectState());
        }

        public static void ShouldBeValidObjectState(this IChallenge challenge)
        {
            challenge.Payout.Buckets.Should().NotBeNullOrEmpty();

            challenge.Game.Should().Should().NotBe(Game.All);

            challenge.Game.Should().Should().NotBe(new Game());

            challenge.State.Should().NotBe(ChallengeState.All);

            challenge.State.Should().NotBe(new ChallengeState());

            challenge.Participants.Should().NotBeNullOrEmpty();

            challenge.Participants.ForEach(
                participant =>
                {
                    challenge.LastSync?.Should().BeAfter(participant.RegisteredAt);

                    participant.RegisteredAt.Should().BeAfter(challenge.CreatedAt);

                    if (challenge.State != ChallengeState.Inscription)
                    {
                        participant.Matches.Should().NotBeNullOrEmpty();
                    }

                    participant.Matches.ForEach(
                        match =>
                        {
                            challenge.LastSync?.Should().BeOnOrAfter(match.SynchronizedAt);

                            participant.SynchronizedAt?.Should().BeOnOrAfter(match.SynchronizedAt);

                            match.SynchronizedAt.Should().BeAfter(participant.RegisteredAt);
                        }
                    );
                }
            );

            //challenge.Participants.Select(participant => participant.Id).Distinct().Should().HaveCount(challenge.Participants.Count);

            //challenge.Participants.Select(participant => participant.UserId).Distinct().Should().HaveCount(challenge.Participants.Count);

            //challenge.Participants.SelectMany(participant => participant.Matches)
            //    .Select(match => match.Id)
            //    .Distinct()
            //    .Should()
            //    .HaveCount(challenge.Participants.SelectMany(participant => participant.Matches).Count());

            challenge.Participants.SelectMany(participant => participant.Matches)
                .Select(match => match.GameMatchId)
                .Distinct()
                .Should()
                .HaveCount(challenge.Participants.SelectMany(participant => participant.Matches).Count());
        }
    }
}
