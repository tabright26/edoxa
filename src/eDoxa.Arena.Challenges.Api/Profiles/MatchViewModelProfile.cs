// Filename: MatchViewModelProfile.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using AutoMapper;

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels;

namespace eDoxa.Arena.Challenges.Api.Profiles
{
    internal sealed class MatchViewModelProfile : Profile
    {
        public MatchViewModelProfile()
        {
            this.CreateMap<IMatch, MatchViewModel>()
                .ForMember(match => match.Id, config => config.MapFrom<Guid>(match => match.Id))
                .ForMember(match => match.SynchronizedAt, config => config.MapFrom(match => match.SynchronizedAt))
                .ForMember(match => match.Score, config => config.MapFrom(match => match.TotalScore.ToDecimal()))
                .ForMember(match => match.ParticipantId, config => config.Ignore())
                .ForMember(match => match.Stats, config => config.MapFrom(match => match.Stats));
        }
    }
}
