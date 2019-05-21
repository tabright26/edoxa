// Filename: ChallengeProfile.cs
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

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.DTO.Profiles
{
    internal sealed class ChallengeProfile : Profile
    {
        public ChallengeProfile()
        {
            this.CreateMap<Challenge, ChallengeDTO>()
                .ForMember(challenge => challenge.Id, config => config.MapFrom(challenge => challenge.Id.ToGuid()))
                .ForMember(challenge => challenge.Game, config => config.MapFrom(challenge => challenge.Game))
                .ForMember(challenge => challenge.Name, config => config.MapFrom(challenge => challenge.Name.ToString()))
                .ForMember(challenge => challenge.Scoring, config => config.MapFrom(challenge => challenge.Scoring))
                .ForMember(challenge => challenge.Payout, config => config.MapFrom(challenge => challenge.Payout))
                .ForMember(challenge => challenge.Participants, config => config.MapFrom(challenge => challenge.Participants.OrderBy(participant => participant.Timestamp)));
        }
    }
}
