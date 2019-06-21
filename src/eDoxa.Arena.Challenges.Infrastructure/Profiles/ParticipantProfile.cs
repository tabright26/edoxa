// Filename: ParticipantProfile.cs
// Date Created: 2019-06-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Converters;
using eDoxa.Arena.Challenges.Infrastructure.Models;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles
{
    internal sealed class ParticipantProfile : Profile
    {
        public ParticipantProfile()
        {
            this.CreateMap<ParticipantModel, Participant>().ConvertUsing(new ParticipantConverter());

            this.CreateMap<Participant, ParticipantModel>()
                .ForMember(participant => participant.Id, config => config.MapFrom<Guid>(participant => participant.Id))
                .ForMember(participant => participant.Timestamp, config => config.MapFrom(participant => participant.RegisteredAt))
                .ForMember(participant => participant.LastSync, config => config.MapFrom(participant => participant.SynchronizedAt))
                .ForMember(participant => participant.GameAccountId, config => config.MapFrom<string>(participant => participant.GameAccountId))
                .ForMember(participant => participant.UserId, config => config.MapFrom<Guid>(participant => participant.UserId))
                .ForMember(participant => participant.Matches, config => config.MapFrom(participant => participant.Matches))
                .ForMember(participant => participant.Challenge, config => config.Ignore())
                .ForMember(participant => participant.ChallengeId, config => config.Ignore());
        }
    }
}
