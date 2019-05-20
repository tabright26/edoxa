﻿// Filename: ChallengeScoringProfile.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;

namespace eDoxa.Arena.Challenges.DTO.Profiles
{
    internal sealed class ChallengeScoringProfile : Profile
    {
        public ChallengeScoringProfile()
        {
            this.CreateMap<Scoring, ChallengeScoringDTO>();
        }
    }
}