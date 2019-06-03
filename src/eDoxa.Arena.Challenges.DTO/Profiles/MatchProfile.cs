// Filename: MatchProfile.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;

namespace eDoxa.Arena.Challenges.DTO.Profiles
{
    internal sealed class MatchProfile : Profile
    {
        public MatchProfile()
        {
            this.CreateMap<Match, MatchDTO>()
                .ForMember(match => match.Id, config => config.MapFrom(match => match.Id.ToGuid()))
                .ForMember(match => match.Timestamp, config => config.MapFrom(match => match.Timestamp))
                .ForMember(match => match.TotalScore, config => config.MapFrom<decimal>(match => match.TotalScore))
                .ForMember(match => match.Stats, config => config.MapFrom(match => match.Stats.OrderBy(stat => stat.Name)));
        }
    }
}
