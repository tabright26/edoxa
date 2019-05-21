// Filename: LeagueOfLegendsChallengeStatsAdapter.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Services.LeagueOfLegends.DTO;

namespace eDoxa.Arena.Challenges.Services.LeagueOfLegends.Adapters
{
    public sealed class LeagueOfLegendsMatchStatsAdapter : IMatchStatsAdapter
    {
        private readonly ParticipantExternalAccount _participantExternalAccount;
        private readonly LeagueOfLegendsMatchDTO _match;

        public LeagueOfLegendsMatchStatsAdapter(ParticipantExternalAccount participantExternalAccount, LeagueOfLegendsMatchDTO match)
        {
            _participantExternalAccount = participantExternalAccount;
            _match = match;
        }

        public IMatchStats MatchStats
        {
            get
            {
                var externalId = new MatchExternalId(_match.GameId);

                var participantId = _match.ParticipantIdentities
                                          .Single(participantIdentity => participantIdentity.Player.AccountId == _participantExternalAccount.ToString())
                                          .ParticipantId;

                var stats = _match.Participants.Single(participant => participant.ParticipantId == participantId).Stats;

                return new MatchStats(externalId, stats);
            }
        }
    }
}