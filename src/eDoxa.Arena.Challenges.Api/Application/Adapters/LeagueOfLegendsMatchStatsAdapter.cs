// Filename: LeagueOfLegendsMatchStatsAdapter.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.Adapters;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.LeagueOfLegends.Abstractions;

namespace eDoxa.Arena.Challenges.Api.Application.Adapters
{
    public sealed class LeagueOfLegendsMatchStatsAdapter : IMatchStatsAdapter
    {
        private readonly ILeagueOfLegendsProxy _leagueOfLegendsProxy;

        public LeagueOfLegendsMatchStatsAdapter(ILeagueOfLegendsProxy leagueOfLegendsProxy)
        {
            _leagueOfLegendsProxy = leagueOfLegendsProxy;
        }

        public ChallengeGame Game => ChallengeGame.LeagueOfLegends;

        public async Task<IMatchStats> GetMatchStatsAsync(GameAccountId gameAccountId, GameReference gameReference)
        {
            var match = await _leagueOfLegendsProxy.GetMatchAsync(gameReference.ToString());

            var participantId = match.ParticipantIdentities.Single(participantIdentity => participantIdentity.Player.AccountId == gameAccountId.ToString())
                .ParticipantId;

            var stats = match.Participants.Single(participant => participant.ParticipantId == participantId).Stats;

            return new MatchStats(stats);
        }
    }
}
