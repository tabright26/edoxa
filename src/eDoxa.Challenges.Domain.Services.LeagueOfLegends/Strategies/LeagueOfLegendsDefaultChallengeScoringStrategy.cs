// Filename: LeagueOfLegendsDefaultChallengeScoringStrategy.cs
// Date Created: 2019-03-05
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.DTO;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.Strategies
{
    internal sealed class LeagueOfLegendsDefaultChallengeScoringStrategy : IChallengeScoringStrategy
    {
        public IChallengeScoring Scoring
        {
            get
            {
                return new ChallengeScoring
                {
                    [nameof(LeagueOfLegendsParticipantStatsDTO.Kills)] = 4,
                    [nameof(LeagueOfLegendsParticipantStatsDTO.Deaths)] = -3,
                    [nameof(LeagueOfLegendsParticipantStatsDTO.Assists)] = 3,
                    [nameof(LeagueOfLegendsParticipantStatsDTO.TotalDamageDealtToChampions)] = 0.0008F,
                    [nameof(LeagueOfLegendsParticipantStatsDTO.TotalHeal)] = 0.0015F
                };
            }
        }
    }
}