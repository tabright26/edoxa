// Filename: LeagueOfLegendsScoringStrategy.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Strategies;
using eDoxa.Arena.LeagueOfLegends.Dtos;

namespace eDoxa.Arena.Challenges.Api.Application.Strategies
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
