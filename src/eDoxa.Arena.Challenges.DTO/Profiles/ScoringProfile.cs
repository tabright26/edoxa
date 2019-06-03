// Filename: ScoringProfile.cs
// Date Created: 2019-06-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;

namespace eDoxa.Arena.Challenges.DTO.Profiles
{
    internal sealed class ScoringProfile : Profile
    {
        public ScoringProfile()
        {
            this.CreateMap<Scoring, ScoringDTO>().ConstructUsing(scoring => new ScoringDTO(scoring));
        }
    }
}
