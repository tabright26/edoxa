// Filename: LeagueOfLegendsScoringStrategy.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.Abstractions.Strategies;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Services.LeagueOfLegends.Dtos;

namespace eDoxa.Arena.Challenges.Domain.Strategies
{
    public sealed class LeagueOfLegendsScoringStrategy : IScoringStrategy
    {
        public IScoring Scoring =>
            new Scoring(
                new HashSet<ScoringItem>
                {
                    new ScoringItem(new StatName(nameof(LeagueOfLegendsParticipantStatsDto.Kills)), new StatWeighting(4)),
                    new ScoringItem(new StatName(nameof(LeagueOfLegendsParticipantStatsDto.Deaths)), new StatWeighting(-3)),
                    new ScoringItem(new StatName(nameof(LeagueOfLegendsParticipantStatsDto.Assists)), new StatWeighting(3)),
                    new ScoringItem(new StatName(nameof(LeagueOfLegendsParticipantStatsDto.TotalDamageDealtToChampions)), new StatWeighting(0.0008F)),
                    new ScoringItem(new StatName(nameof(LeagueOfLegendsParticipantStatsDto.TotalHeal)), new StatWeighting(0.0015F))
                }
            );
    }
}
