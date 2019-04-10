// Filename: ChallengePrizeBreakdownProfile.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.DTO.Profiles
{
    internal sealed class ChallengePrizeBreakdownProfile : Profile
    {
        public ChallengePrizeBreakdownProfile()
        {
            this.CreateMap<ChallengePrizeBreakdown, ChallengePrizeBreakdownDTO>();
        }
    }
}