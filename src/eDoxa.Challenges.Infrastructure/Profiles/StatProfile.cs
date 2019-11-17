// Filename: StatProfile.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Challenges.Infrastructure.Profiles.ConverterTypes;

namespace eDoxa.Challenges.Infrastructure.Profiles
{
    internal sealed class StatProfile : Profile
    {
        public StatProfile()
        {
            this.CreateMap<StatModel, Stat>().ConvertUsing(new StatTypeConverter());

            this.CreateMap<Stat, StatModel>()
                .ForMember(stat => stat.Name, config => config.MapFrom<string>(stat => stat.Name))
                .ForMember(stat => stat.Value, config => config.MapFrom<double>(stat => stat.Value))
                .ForMember(stat => stat.Weighting, config => config.MapFrom<float>(stat => stat.Weighting));
        }
    }
}
