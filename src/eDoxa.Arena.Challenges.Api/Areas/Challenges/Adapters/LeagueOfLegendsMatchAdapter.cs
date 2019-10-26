// Filename: LeagueOfLegendsMatchAdapter.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.Adapters;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Games.LeagueOfLegends.Abstractions;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Arena.Challenges.Api.Areas.Challenges.Adapters
{
    public sealed class LeagueOfLegendsMatchAdapter : IMatchAdapter
    {
        private readonly ILeagueOfLegendsService _leagueOfLegendsService;

        public LeagueOfLegendsMatchAdapter(ILeagueOfLegendsService leagueOfLegendsService)
        {
            _leagueOfLegendsService = leagueOfLegendsService;
        }

        public ChallengeGame Game => ChallengeGame.LeagueOfLegends;

        public async Task<IMatch> GetMatchAsync(
            GameAccountId gameAccountId,
            GameReference gameReference,
            IScoring scoring,
            IDateTimeProvider synchronizedAt
        )
        {
            var stats = await this.GetGameStatsAsync(gameAccountId, gameReference);

            var match = new StatMatch(
                scoring,
                stats,
                gameReference,
                synchronizedAt);

            return match;
        }

        private async Task<IGameStats> GetGameStatsAsync(GameAccountId gameAccountId, GameReference gameReference)
        {
            var match = await _leagueOfLegendsService.GetMatchAsync(gameReference.ToString());

            var participantId = match.ParticipantIdentities.Single(participantIdentity => participantIdentity.Player.AccountId == gameAccountId.ToString())
                .ParticipantId;

            var stats = match.Participants.Single(participant => participant.ParticipantId == participantId).Stats;

            return new GameStats(stats);
        }
    }
}
