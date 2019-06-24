// Filename: StatProfile.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright � 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Challenges.Api.Profiles.Resolvers;
using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure.Models;

namespace eDoxa.Arena.Challenges.Api.Profiles
{
    internal sealed class StatProfile : Profile
    {
        public StatProfile()
        {
            this.CreateMap<StatModel, StatViewModel>()
                .ForMember(stat => stat.Name, config => config.MapFrom(stat => stat.Name))
                .ForMember(stat => stat.Value, config => config.MapFrom(stat => stat.Value))
                .ForMember(stat => stat.Weighting, config => config.MapFrom(stat => stat.Weighting))
                .ForMember(stat => stat.Score, config => config.MapFrom<StatScoreResolver>());
        }
    }
}
