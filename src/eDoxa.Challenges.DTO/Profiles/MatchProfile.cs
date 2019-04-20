// Filename: MatchProfile.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.DTO.Profiles
{
    internal sealed class MatchProfile : Profile
    {
        public MatchProfile()
        {
            this.CreateMap<Match, MatchDTO>()
                .ForMember(match => match.Id, configuration => configuration.MapFrom(match => match.Id.ToGuid()))
                .ForMember(match => match.Timestamp, configuration => configuration.MapFrom(match => match.Timestamp))
                .ForMember(match => match.TotalScore, configuration => configuration.MapFrom(match => match.TotalScore))
                .ForMember(match => match.Stats, configuration => configuration.MapFrom(match => match.Stats.OrderBy(stat => stat.Name)));
        }
    }
}