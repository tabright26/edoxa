// Filename: LeagueOfLegendsScoringStrategy.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Challenges.Api.Temp.LeagueOfLegends.Dtos;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Strategies;

namespace eDoxa.Arena.Challenges.Api.Areas.Challenges.Strategies
{
    public sealed class LeagueOfLegendsScoringStrategy : IScoringStrategy
    {
        public ChallengeGame Game => ChallengeGame.LeagueOfLegends;

        public IScoring Scoring =>
            new Scoring
            {
                [new StatName(nameof(LeagueOfLegendsParticipantStatsDto.Kills))] = new StatWeighting(4),
                [new StatName(nameof(LeagueOfLegendsParticipantStatsDto.Deaths))] = new StatWeighting(-3),
                [new StatName(nameof(LeagueOfLegendsParticipantStatsDto.Assists))] = new StatWeighting(3),
                [new StatName(nameof(LeagueOfLegendsParticipantStatsDto.TotalDamageDealtToChampions))] = new StatWeighting(0.0008F),
                [new StatName(nameof(LeagueOfLegendsParticipantStatsDto.TotalHeal))] = new StatWeighting(0.0015F)
            };
    }
}
