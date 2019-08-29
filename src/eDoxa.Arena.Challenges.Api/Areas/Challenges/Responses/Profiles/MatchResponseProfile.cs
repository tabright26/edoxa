// Filename: MatchResponseProfile.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels;

namespace eDoxa.Arena.Challenges.Api.Areas.Challenges.Responses.Profiles
{
    internal sealed class MatchResponseProfile : Profile
    {
        public MatchResponseProfile()
        {
            this.CreateMap<IMatch, MatchResponse>()
                .ForMember(match => match.Id, config => config.MapFrom<Guid>(match => match.Id))
                .ForMember(match => match.SynchronizedAt, config => config.MapFrom(match => match.SynchronizedAt))
                .ForMember(match => match.Score, config => config.MapFrom(match => match.Score.ToDecimal()))
                .ForMember(match => match.ParticipantId, config => config.Ignore())
                .ForMember(match => match.Stats, config => config.MapFrom(match => match.Stats));
        }
    }
}
