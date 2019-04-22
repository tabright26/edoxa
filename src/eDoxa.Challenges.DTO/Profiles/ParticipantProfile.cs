﻿// Filename: ParticipantProfile.cs
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

using eDoxa.AutoMapper.Extensions;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.DTO.Profiles
{
    internal sealed class ParticipantProfile : Profile
    {
        public ParticipantProfile()
        {
            this.CreateMap<Participant, ParticipantDTO>()
                .ForMember(participant => participant.Id, config => config.MapFrom(participant => participant.Id.ToGuid()))
                .ForMember(participant => participant.UserId, config => config.MapFrom(participant => participant.UserId.ToGuid()))
                .ForMember(participant => participant.AverageScore, config => config.OptionalMapFrom(participant => participant.AverageScore.Map<decimal?>(score => score)))
                .ForMember(participant => participant.Matches, config => config.MapFrom(participant => participant.Matches.OrderBy(match => match.Timestamp)));
        }
    }
}