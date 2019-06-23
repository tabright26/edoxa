// Filename: FluentAssertionsExtensions.cs
// Date Created: 2019-06-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Seedwork.Domain.Extensions;

using FluentAssertions;

namespace eDoxa.Arena.Challenges.UnitTests.Extensions
{
    public static class FluentAssertionsExtensions
    {
        public static void AssertStateIsValid(this IEnumerable<IChallenge> challenges)
        {
            challenges.ForEach(challenge => challenge.AssertStateIsValid());
        }

        public static void AssertStateIsValid(this IChallenge challenge)
        {
            challenge.Payout.Buckets.Should().NotBeNullOrEmpty();
            challenge.Game.Should().Should().NotBe(ChallengeGame.All);
            challenge.Game.Should().Should().NotBe(new ChallengeGame());
            challenge.Timeline.State.Should().NotBe(ChallengeState.All);
            challenge.Timeline.State.Should().NotBe(new ChallengeState());
            challenge.Participants.Should().NotBeNullOrEmpty();

            challenge.Participants.ForEach(
                participant =>
                {
                    challenge.SynchronizedAt?.Should().BeAfter(participant.RegisteredAt);
                    participant.RegisteredAt.Should().BeAfter(challenge.Timeline.CreatedAt);

                    if (challenge.Timeline.State != ChallengeState.Inscription)
                    {
                        participant.Matches.Should().NotBeNullOrEmpty();
                    }

                    participant.Matches.ForEach(
                        match =>
                        {
                            challenge.SynchronizedAt?.Should().BeOnOrAfter(match.SynchronizedAt);
                            participant.SynchronizedAt?.Should().BeOnOrAfter(match.SynchronizedAt);
                            match.SynchronizedAt.Should().BeAfter(participant.RegisteredAt);
                        }
                    );
                }
            );

            challenge.Participants.Select(participant => participant.Id).Distinct().Should().HaveCount(challenge.Participants.Count);
            challenge.Participants.Select(participant => participant.UserId).Distinct().Should().HaveCount(challenge.Participants.Count);

            challenge.Participants.SelectMany(participant => participant.Matches)
                .Select(match => match.Id)
                .Distinct()
                .Should()
                .HaveCount(challenge.Participants.SelectMany(participant => participant.Matches).Count());

            challenge.Participants.SelectMany(participant => participant.Matches)
                .Select(match => match.GameMatchId)
                .Distinct()
                .Should()
                .HaveCount(challenge.Participants.SelectMany(participant => participant.Matches).Count());
        }

        public static void AssertMappingIsValid(this IEnumerable<ChallengeViewModel> challenges)
        {
            challenges.ForEach(challenge => challenge.AssertMappingIsValid());
        }

        public static void AssertMappingIsValid(this ChallengeViewModel challenge)
        {
            challenge.Should().NotBeNull();
            challenge.Id.Should().NotBeEmpty();
            challenge.Name.Should().NotBeNullOrWhiteSpace();
            challenge.Game.Should().NotBeNull();
            challenge.Game.Should().NotBe(new ChallengeGame());
            challenge.Game.Should().NotBe(ChallengeGame.All);
            challenge.State.Should().NotBeNull();
            challenge.State.Should().NotBe(new ChallengeState());
            challenge.State.Should().NotBe(ChallengeState.All);
            challenge.Setup.Should().NotBeNull();
            challenge.Timeline.Should().NotBeNull();
            challenge.Timeline.CreatedAt.Should().BeBefore(DateTime.UtcNow);
            challenge.Scoring.Should().NotBeNull();
            challenge.Scoring.Should().NotBeEmpty();
            challenge.Payout.AssertMappingIsValid();
            challenge.Participants.AssertMappingIsValid();
        }

        public static void AssertMappingIsValid(this PayoutViewModel payout)
        {
            payout.Should().NotBeNull();
            payout.Buckets.Should().NotBeNullOrEmpty();
        }

        public static void AssertMappingIsValid(this IEnumerable<ParticipantViewModel> participants)
        {
            participants.ForEach(participant => participant.AssertMappingIsValid());
        }

        public static void AssertMappingIsValid(this ParticipantViewModel participant)
        {
            participant.Id.Should().NotBeEmpty();
            participant.UserId.Should().NotBeEmpty();
            participant.Matches.AssertMappingIsValid();
        }

        public static void AssertMappingIsValid(this IEnumerable<MatchViewModel> matches)
        {
            matches.ForEach(match => match.AssertMappingIsValid());
        }

        public static void AssertMappingIsValid(this MatchViewModel match)
        {
            match.Id.Should().NotBeEmpty();
            match.TotalScore.Should().BeGreaterOrEqualTo(decimal.Zero);
            match.Stats.AssertMappingIsValid();
        }

        private static void AssertMappingIsValid(this IEnumerable<StatViewModel> stats)
        {
            stats.ForEach(stat => stat.AssertMappingIsValid());
        }

        private static void AssertMappingIsValid(this StatViewModel stat)
        {
            stat.Name.Should().NotBeNullOrWhiteSpace();
        }
    }
}
