// Filename: LeagueOfLegendsChallengeMatchesAdapter.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Games.Domain.Adapters;
using eDoxa.Games.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Games.LeagueOfLegends.Abstactions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

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

        public async Task<IReadOnlyCollection<ChallengeMatch>> GetMatchesAsync(
            PlayerId playerId,
            DateTime? startedAt,
            DateTime? endedAt,
            IImmutableSet<string> matchIds
        )
        {
            var matchList = await _leagueOfLegendsService.Match.GetMatchListAsync(
                Region.Na,
                playerId,
                beginTime: startedAt,
                endTime: endedAt);

            var matches = new List<ChallengeMatch>();

            foreach (var reference in matchList.Matches.Where(
                match => !matchIds.Contains(match.GameId.ToString()) && match.Queue == 420 /* 5v5 Ranked Solo games */))
            {
                var match = await _leagueOfLegendsService.Match.GetMatchAsync(Region.Na, reference.GameId);

                var stats = match.Participants.Single(
                        participant => participant.ParticipantId ==
                                       match.ParticipantIdentities.Single(identity => identity.Player.AccountId == playerId).ParticipantId)
                    .Stats;

                matches.Add(
                    new ChallengeMatch(
                        match.GameId.ToString(),
                        new DateTimeProvider(match.GameCreation),
                        match.GameDuration,
                        stats.GetType().GetProperties().ToDictionary(property => property.Name, property => Convert.ToDouble(property.GetValue(stats)))));
            }

            return matches;
        }
    }
}
