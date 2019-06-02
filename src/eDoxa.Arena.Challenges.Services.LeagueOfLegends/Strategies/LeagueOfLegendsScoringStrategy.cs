// Filename: LeagueOfLegendsScoringStrategy.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Services.LeagueOfLegends.DTO;

namespace eDoxa.Arena.Challenges.Services.LeagueOfLegends.Strategies
{
    public sealed class LeagueOfLegendsScoringStrategy : IScoringStrategy
    {
        public IScoring Scoring =>
            new Scoring(
                new HashSet<ChallengeStat>
                {
                    new ChallengeStat(new StatName(nameof(LeagueOfLegendsParticipantStatsDTO.Kills)), new StatWeighting(4)),
                    new ChallengeStat(new StatName(nameof(LeagueOfLegendsParticipantStatsDTO.Deaths)), new StatWeighting(-3)),
                    new ChallengeStat(new StatName(nameof(LeagueOfLegendsParticipantStatsDTO.Assists)), new StatWeighting(3)),
                    new ChallengeStat(new StatName(nameof(LeagueOfLegendsParticipantStatsDTO.TotalDamageDealtToChampions)), new StatWeighting(0.0008F)),
                    new ChallengeStat(new StatName(nameof(LeagueOfLegendsParticipantStatsDTO.TotalHeal)), new StatWeighting(0.0015F))
                }
            );
    }
}
