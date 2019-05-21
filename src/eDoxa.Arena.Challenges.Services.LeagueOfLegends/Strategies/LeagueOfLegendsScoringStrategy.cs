// Filename: LeagueOfLegendsScoringStrategy.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Services.LeagueOfLegends.DTO;

namespace eDoxa.Arena.Challenges.Services.LeagueOfLegends.Strategies
{
    public sealed class LeagueOfLegendsScoringStrategy : IScoringStrategy
    {
        public IScoring Scoring =>
            new Scoring
            {
                [nameof(LeagueOfLegendsParticipantStatsDTO.Kills)] = new StatWeighting(4),
                [nameof(LeagueOfLegendsParticipantStatsDTO.Deaths)] = new StatWeighting(-3),
                [nameof(LeagueOfLegendsParticipantStatsDTO.Assists)] = new StatWeighting(3),
                [nameof(LeagueOfLegendsParticipantStatsDTO.TotalDamageDealtToChampions)] = new StatWeighting(0.0008F),
                [nameof(LeagueOfLegendsParticipantStatsDTO.TotalHeal)] = new StatWeighting(0.0015F)
            };
    }
}
