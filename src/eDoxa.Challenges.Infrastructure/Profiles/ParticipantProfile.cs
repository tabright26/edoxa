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
using System.Collections.Generic;

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Challenges.Infrastructure.Profiles.ConverterTypes;
using eDoxa.Challenges.Infrastructure.Profiles.Resolvers;

namespace eDoxa.Challenges.Infrastructure.Profiles
{
    internal sealed class ParticipantProfile : Profile
    {
        public ParticipantProfile()
        {
            this.CreateMap<ParticipantModel, Participant>().ConvertUsing(new ParticipantTypeConverter());

            this.CreateMap<Participant, ParticipantModel>()
                .ForMember(participant => participant.Id, config => config.MapFrom<Guid>(participant => participant.Id))
                .ForMember(participant => participant.RegisteredAt, config => config.MapFrom(participant => participant.RegisteredAt))
                .ForMember(participant => participant.SynchronizedAt, config => config.MapFrom(participant => participant.SynchronizedAt))
                .ForMember(participant => participant.PlayerId, config => config.MapFrom<string>(participant => participant.PlayerId))
                .ForMember(participant => participant.UserId, config => config.MapFrom<Guid>(participant => participant.UserId))
                .ForMember(
                    participant => participant.Matches,
                    config => config.MapFrom<MatchModelsResolver, IReadOnlyCollection<IMatch>>(participant => participant.Matches)
                )
                .ForMember(participant => participant.Challenge, config => config.Ignore());
        }
    }
}
