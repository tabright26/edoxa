// Filename: ParticipantProfile.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;

namespace eDoxa.Arena.Challenges.DTO.Profiles
{
    internal sealed class ParticipantProfile : Profile
    {
        public ParticipantProfile()
        {
            this.CreateMap<Participant, ParticipantDTO>()
                .ForMember(participant => participant.Id, config => config.MapFrom(participant => participant.Id.ToGuid()))
                .ForMember(participant => participant.UserId, config => config.MapFrom(participant => participant.UserId.ToGuid()))
                .ForMember(
                    participant => participant.AverageScore,
                    config => config.MapFrom(participant => participant.AverageScore.Select(score => (decimal?) score).SingleOrDefault())
                )
                .ForMember(participant => participant.Matches, config => config.MapFrom(participant => participant.Matches.OrderBy(match => match.Timestamp)));
        }
    }
}
