// Filename: MatchProfile.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Challenges.Api.Application.Profiles.Resolvers;
using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure.Models;

namespace eDoxa.Arena.Challenges.Api.Application.Profiles
{
    internal sealed class MatchProfile : Profile
    {
        public MatchProfile()
        {
            this.CreateMap<MatchModel, MatchViewModel>()
                .ForMember(match => match.Id, config => config.MapFrom(match => match.Id))
                .ForMember(match => match.SynchronizedAt, config => config.MapFrom(match => match.SynchronizedAt))
                .ForMember(match => match.TotalScore, config => config.MapFrom<MatchScoreResolver>())
                .ForMember(match => match.Stats, config => config.MapFrom(match => match.Stats))
                .ForMember(match => match.ParticipantId, config => config.MapFrom(match => match.Participant.Id));
        }
    }
}
