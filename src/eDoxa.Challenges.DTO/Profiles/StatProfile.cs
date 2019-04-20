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

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.DTO.Profiles
{
    internal sealed class StatProfile : Profile
    {
        public StatProfile()
        {
            this.CreateMap<Stat, StatDTO>()
                .ForMember(stat => stat.Name, configuration => configuration.MapFrom(stat => stat.Name.ToString()))
                .ForMember(stat => stat.Value, configuration => configuration.MapFrom<double>(stat => stat.Value))
                .ForMember(stat => stat.Weighting, configuration => configuration.MapFrom<float>(stat => stat.Weighting))
                .ForMember(stat => stat.Score, configuration => configuration.MapFrom<decimal>(stat => stat.Score));
        }
    }
}