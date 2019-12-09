// Filename: MatchResponseProfile.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Responses;

namespace eDoxa.Challenges.Api.Application.Profiles
{
    internal sealed class MatchResponseProfile : Profile
    {
        public MatchResponseProfile()
        {
            this.CreateMap<IMatch, MatchResponse>()
                .ForMember(match => match.Id, config => config.MapFrom<Guid>(match => match.Id))
                .ForMember(match => match.Score, config => config.MapFrom(match => match.Score.ToDecimal()))
                .ForMember(match => match.ParticipantId, config => config.Ignore())
                .ForMember(match => match.Stats, config => config.MapFrom(match => match.Stats));
        }
    }
}
