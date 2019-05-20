// Filename: StatProfile.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels.MatchAggregate;

namespace eDoxa.Challenges.DTO.Profiles
{
    internal sealed class StatProfile : Profile
    {
        public StatProfile()
        {
            this.CreateMap<Stat, StatDTO>()
                .ForMember(stat => stat.Name, config => config.MapFrom(stat => stat.Name.ToString()))
                .ForMember(stat => stat.Value, config => config.MapFrom<double>(stat => stat.Value))
                .ForMember(stat => stat.Weighting, config => config.MapFrom<float>(stat => stat.Weighting))
                .ForMember(stat => stat.Score, config => config.MapFrom<decimal>(stat => stat.Score));
        }
    }
}