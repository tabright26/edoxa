// Filename: LeagueOfLegendsDefaultChallengeScoringStrategy.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.Entities.Abstractions;
using eDoxa.Challenges.Domain.Entities.AggregateModels.MatchAggregate;
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.DTO;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.Strategies
{
    internal sealed class LeagueOfLegendsDefaultScoringStrategy : IScoringStrategy
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