// Filename: LeagueOfLegendsChallengeStatsAdapter.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.DTO;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.Adapters
{
    public sealed class LeagueOfLegendsChallengeStatsAdapter : IChallengeStatsAdapter
    {
        private readonly LinkedAccount _linkedAccount;
        private readonly LeagueOfLegendsMatchDTO _match;

        public LeagueOfLegendsChallengeStatsAdapter(LinkedAccount linkedAccount, LeagueOfLegendsMatchDTO match)
        {
            _linkedAccount = linkedAccount ?? throw new ArgumentNullException(nameof(linkedAccount));
            _match = match ?? throw new ArgumentNullException(nameof(match));
        }

        public IChallengeStats Stats
        {
            get
            {
                var linkedMatch = LinkedMatch.FromLong(_match.GameId);

                var participantId = _match.ParticipantIdentities
                                          .Single(participantIdentity => participantIdentity.Player.AccountId == _linkedAccount.ToString())
                                          .ParticipantId;

                var stats = _match.Participants.Single(participant => participant.ParticipantId == participantId).Stats;

                return new ChallengeStats(linkedMatch, stats);
            }
        }
    }
}