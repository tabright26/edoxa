// Filename: LeagueOfLegendsChallengeMatchesAdapter.cs
// Date Created: 2019-11-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Games.Abstractions.Adapter;
using eDoxa.Games.LeagueOfLegends.Abstactions;
using eDoxa.Seedwork.Application.Dtos;
using eDoxa.Seedwork.Domain.Miscs;

using RiotSharp.Misc;

namespace eDoxa.Games.LeagueOfLegends.Adapter
{
    public sealed class LeagueOfLegendsChallengeMatchesAdapter : IChallengeMatchesAdapter
    {
        private readonly ILeagueOfLegendsService _leagueOfLegendsService;

        public LeagueOfLegendsChallengeMatchesAdapter(ILeagueOfLegendsService leagueOfLegendsService)
        {
            _leagueOfLegendsService = leagueOfLegendsService;
        }

        public Game Game => Game.LeagueOfLegends;

        public async Task<IReadOnlyCollection<MatchDto>> GetMatchesAsync(PlayerId playerId, DateTime? startedAt, DateTime? endedAt)
        {
            var matchList = await _leagueOfLegendsService.Match.GetMatchListAsync(
                Region.Na,
                playerId,
                beginTime: startedAt,
                endTime: endedAt);

            var matches = new List<MatchDto>();

            foreach (var reference in matchList.Matches)
            {
                var match = await _leagueOfLegendsService.Match.GetMatchAsync(Region.Na, reference.GameId);

                var stats = match.Participants.Single(
                        participant => participant.ParticipantId ==
                                       match.ParticipantIdentities.Single(identity => identity.Player.AccountId == playerId).ParticipantId)
                    .Stats;

                matches.Add(
                    new MatchDto(
                        match.GameId.ToString(),
                        match.GameCreation,
                        stats.GetType().GetProperties().ToDictionary(property => property.Name, property => Convert.ToDouble(property.GetValue(stats)))));
            }

            return matches;
        }
    }
}
