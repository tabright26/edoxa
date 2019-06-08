// Filename: LeagueOfLegendsMatchStatsAdapter.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.Abstractions.Adapters;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Services.LeagueOfLegends.Dtos;

namespace eDoxa.Arena.Challenges.Domain.Adapters
{
    public sealed class LeagueOfLegendsMatchStatsAdapter : IMatchStatsAdapter
    {
        private readonly ExternalAccount _externalAccount;
        private readonly LeagueOfLegendsMatchDto _match;

        public LeagueOfLegendsMatchStatsAdapter(ExternalAccount externalAccount, LeagueOfLegendsMatchDto match)
        {
            _externalAccount = externalAccount;
            _match = match;
        }

        public IMatchStats MatchStats
        {
            get
            {
                var participantId = _match.ParticipantIdentities
                    .Single(participantIdentity => participantIdentity.Player.AccountId == _externalAccount.ToString())
                    .ParticipantId;

                var stats = _match.Participants.Single(participant => participant.ParticipantId == participantId).Stats;

                return new MatchStats(stats);
            }
        }
    }
}
