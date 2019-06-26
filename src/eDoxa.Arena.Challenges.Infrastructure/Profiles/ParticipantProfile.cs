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

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Arena.Challenges.Infrastructure.Profiles.ConverterTypes;
using eDoxa.Arena.Challenges.Infrastructure.Profiles.Resolvers;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles
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
                .ForMember(participant => participant.GameAccountId, config => config.MapFrom<string>(participant => participant.GameAccountId))
                .ForMember(participant => participant.UserId, config => config.MapFrom<Guid>(participant => participant.UserId))
                .ForMember(
                    participant => participant.Matches,
                    config => config.MapFrom<MatchModelsResolver, IReadOnlyCollection<Match>>(participant => participant.Matches)
                )
                .ForMember(participant => participant.Challenge, config => config.Ignore());
        }
    }
}
