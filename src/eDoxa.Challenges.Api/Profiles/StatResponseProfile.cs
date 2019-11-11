// Filename: StatResponseProfile.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Responses;

namespace eDoxa.Challenges.Api.Profiles
{
    internal sealed class StatResponseProfile : Profile
    {
        public StatResponseProfile()
        {
            this.CreateMap<Stat, StatResponse>()
                .ForMember(stat => stat.Name, config => config.MapFrom<string>(stat => stat.Name))
                .ForMember(stat => stat.Value, config => config.MapFrom<double>(stat => stat.Value))
                .ForMember(stat => stat.Weighting, config => config.MapFrom<float>(stat => stat.Weighting))
                .ForMember(stat => stat.Score, config => config.MapFrom(stat => stat.Score.ToDecimal()));
        }
    }
}
