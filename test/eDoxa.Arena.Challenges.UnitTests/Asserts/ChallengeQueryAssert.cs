// Filename: ChallengeQueryAssert.cs
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

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common.Enumerations;

using FluentAssertions;

namespace eDoxa.Arena.Challenges.UnitTests.Asserts
{
    public static class ChallengeQueryAssert
    {
        public static void IsMapped(ChallengeViewModel[] challenges)
        {
            challenges.Should().NotBeNullOrEmpty();

            foreach (var challenge in challenges)
            {
                IsMapped(challenge);
            }
        }

        public static void IsMapped(ChallengeViewModel challenge)
        {
            challenge.Should().NotBeNull();

            challenge.Id.Should().NotBeEmpty();

            challenge.Timestamp.Should().BeBefore(DateTime.UtcNow);

            challenge.Name.Should().NotBeNullOrWhiteSpace();

            challenge.Game.Should().NotBeNull();

            challenge.Game.Should().NotBe(new Game());

            challenge.Game.Should().NotBe(Game.All);

            challenge.State.Should().NotBeNull();

            challenge.State.Should().NotBe(new ChallengeState());

            challenge.State.Should().NotBe(ChallengeState.All);

            challenge.Setup.Should().NotBeNull();

            challenge.Timeline.Should().NotBeNull();

            challenge.Scoring.Should().NotBeNull();

            challenge.Scoring.Should().NotBeEmpty();

            challenge.Scoreboard.Should().NotBeNull();

            challenge.Scoreboard.Should().HaveCount(challenge.Participants.Length);

            IsMapped(challenge.Payout);

            IsMapped(challenge.Participants);
        }

        public static void IsMapped(PayoutViewModel payout)
        {
            payout.Should().NotBeNull();

            payout.Buckets.Should().NotBeNullOrEmpty();
        }

        public static void IsMapped(IList<ParticipantViewModel> participants)
        {
            participants.Should().NotBeNullOrEmpty();

            foreach (var participant in participants)
            {
                IsMapped(participant);
            }
        }

        public static void IsMapped(ParticipantViewModel participant)
        {
            participant.Id.Should().NotBeEmpty();

            participant.UserId.Should().NotBeEmpty();

            participant.Matches.Should().NotBeNullOrEmpty();

            IsMapped(participant.Matches);
        }

        public static void IsMapped(IList<MatchViewModel> matches)
        {
            matches.Should().NotBeNullOrEmpty();

            foreach (var match in matches)
            {
                IsMapped(match);
            }
        }

        public static void IsMapped(MatchViewModel match)
        {
            match.Id.Should().NotBeEmpty();

            match.Timestamp.Should().BeBefore(DateTime.UtcNow);

            match.TotalScore.Should().BeGreaterOrEqualTo(decimal.Zero);

            IsMapped(match.Stats);
        }

        private static void IsMapped(IList<StatViewModel> stats)
        {
            stats.Should().NotBeNullOrEmpty();

            foreach (var stat in stats)
            {
                IsMapped(stat);
            }
        }

        private static void IsMapped(StatViewModel stat)
        {
            stat.Name.Should().NotBeNullOrWhiteSpace();
        }
    }
}
