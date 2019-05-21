// Filename: ScoringProfile.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;

namespace eDoxa.Arena.Challenges.DTO.Profiles
{
    internal sealed class ScoringProfile : Profile
    {
        public ScoringProfile()
        {
            this.CreateMap<StatName, string>().ConvertUsing(name => name.ToString());
            this.CreateMap<StatWeighting, float>().ConvertUsing(weighting => Convert.ToSingle(weighting));
            this.CreateMap<Scoring, ScoringDTO>();
        }
    }
}
