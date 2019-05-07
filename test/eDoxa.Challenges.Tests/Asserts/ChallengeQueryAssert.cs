// Filename: ChallengeQueriesAssert.cs
// Date Created: 2019-05-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.DTO;
using eDoxa.Seedwork.Enumerations;

using FluentAssertions;

namespace eDoxa.Challenges.Tests.Asserts
{
    public static class ChallengeQueryAssert
    {
        public static void IsMapped(ChallengeListDTO challenges)
        {
            challenges.Should().NotBeNullOrEmpty();

            foreach (var challenge in challenges)
            {
                IsMapped(challenge);
            }
        }

        public static void IsMapped(ChallengeDTO challenge)
        {
            challenge.Should().NotBeNull();

            challenge.Id.Should().NotBeEmpty();

            challenge.Game.Should().NotBe(Game.All);

            challenge.Game.Should().NotBe(Game.None);

            challenge.Name.Should().NotBeNullOrWhiteSpace();

            challenge.Setup.Should().NotBeNull();

            challenge.Timeline.Should().NotBeNull();

            //challenge.LiveData.Should().NotBeNull();

            challenge.Scoring.Should().NotBeNull();

            challenge.Payout.Should().NotBeNull();

            IsMapped(challenge.Participants);
        }

        public static void IsMapped(ParticipantListDTO participants)
        {
            participants.Should().NotBeNullOrEmpty();

            foreach (var participant in participants)
            {
                IsMapped(participant);
            }
        }

        public static void IsMapped(ParticipantDTO participant)
        {
            participant.Id.Should().NotBeEmpty();

            participant.UserId.Should().NotBeEmpty();

            participant.Matches.Should().NotBeNullOrEmpty();

            IsMapped(participant.Matches);
        }

        public static void IsMapped(MatchListDTO matches)
        {
            matches.Should().NotBeNullOrEmpty();

            foreach (var match in matches)
            {
                IsMapped(match);
            }
        }

        public static void IsMapped(MatchDTO match)
        {
            match.Id.Should().NotBeEmpty();

            match.Timestamp.Should().BeBefore(DateTime.UtcNow);

            IsMapped(match.Stats);
        }

        private static void IsMapped(StatListDTO stats)
        {
            stats.Should().NotBeNullOrEmpty();

            foreach (var stat in stats)
            {
                IsMapped(stat);
            }
        }

        private static void IsMapped(StatDTO stat)
        {
            stat.Name.Should().NotBeNullOrWhiteSpace();
        }
    }
}