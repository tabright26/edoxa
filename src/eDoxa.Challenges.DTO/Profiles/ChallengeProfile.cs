// Filename: ChallengeProfile.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using AutoMapper;

using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Services.Factories;

namespace eDoxa.Challenges.DTO.Profiles
{
    internal sealed class ChallengeProfile : Profile
    {
        public ChallengeProfile()
        {
            this.CreateMap<Challenge, ChallengeDTO>()
                .ForMember(challenge => challenge.Id, config => config.MapFrom(challenge => challenge.Id.ToGuid()))
                .ForMember(challenge => challenge.Game, config => config.MapFrom(challenge => challenge.Game))
                .ForMember(challenge => challenge.Name, config => config.MapFrom(challenge => challenge.Name.ToString()))               
                .ForMember(challenge => challenge.Type, config => config.MapFrom(challenge => challenge.Setup.Type))
                .ForMember(challenge => challenge.State, config => config.MapFrom(challenge => challenge.Timeline.State))
                //.ForMember(challenge => challenge.LiveMode, config => config.MapFrom(challenge => challenge.Timeline.LiveMode))
                //.ForMember(challenge => challenge.Generated, config => config.MapFrom(challenge => challenge.Setup.Generated))
                .ForMember(
                    challenge => challenge.Scoring,
                    config =>
                    {
                        config.MapFrom(challenge => challenge.Scoring.SingleOrDefault());
                        config.Condition(challenge => challenge.Timeline.State.Value >= ChallengeState.Opened.Value);
                    }
                )
                .ForMember(challenge => challenge.Payout, config => config.MapFrom(challenge => PayoutFactory.Instance.CreatePayout(challenge).Payout))
                //.ForMember(challenge => challenge.LiveData, config => config.MapFrom(challenge => challenge.LiveData))
                .ForMember(challenge => challenge.Participants, config => config.MapFrom(challenge => challenge.Participants.OrderBy(participant => participant.Timestamp)));
        }
    }
}