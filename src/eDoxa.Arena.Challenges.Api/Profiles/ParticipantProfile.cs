// Filename: ParticipantProfile.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Challenges.Api.Profiles.Resolvers;
using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure.Models;

namespace eDoxa.Arena.Challenges.Api.Profiles
{
    internal sealed class ParticipantProfile : Profile
    {
        public ParticipantProfile()
        {
            this.CreateMap<ParticipantModel, ParticipantViewModel>()
                .ForMember(participant => participant.Id, config => config.MapFrom(participant => participant.Id))
                .ForMember(participant => participant.UserId, config => config.MapFrom(participant => participant.UserId))
                .ForMember(participant => participant.AverageScore, config => config.MapFrom<ParticipantScoreResolver>())
                .ForMember(participant => participant.Matches, config => config.MapFrom(participant => participant.Matches))
                .ForMember(participant => participant.ChallengeId, config => config.MapFrom(participant => participant.Challenge.Id));
        }
    }
}
