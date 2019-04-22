// Filename: ChallengeProfile.cs
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

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.DTO.Profiles
{
    internal sealed class ChallengeProfile : Profile
    {
        public ChallengeProfile()
        {
            this.CreateMap<Challenge, ChallengeDTO>()
                .ForMember(challenge => challenge.Id, config => config.MapFrom(challenge => challenge.Id.ToGuid()))
                .ForMember(challenge => challenge.Name, config => config.MapFrom(challenge => challenge.Name.ToString()))
                .ForMember(challenge => challenge.Game, config => config.MapFrom(challenge => challenge.Game))
                .ForMember(challenge => challenge.Type, config => config.MapFrom(challenge => challenge.Setup.Type))
                .ForMember(challenge => challenge.State, config => config.MapFrom(challenge => challenge.Timeline.State))
                .ForMember(challenge => challenge.LiveMode, config => config.MapFrom(challenge => challenge.Timeline.LiveMode))
                .ForMember(challenge => challenge.Generated, config => config.MapFrom(challenge => challenge.Setup.Generated))
                .ForMember(challenge => challenge.Payout, config => config.MapFrom(challenge => challenge.Payout))
                .ForMember(
                    challenge => challenge.Scoring,
                    config =>
                    {
                        config.MapFrom(challenge => challenge.Scoring);
                        config.Condition(challenge => challenge.Timeline.State >= ChallengeState.Opened);
                    }
                )
                .ForMember(
                    challenge => challenge.LiveData,
                    config =>
                    {
                        config.MapFrom(challenge => challenge.LiveData);
                        config.Condition(challenge => challenge.Timeline.State >= ChallengeState.Opened);
                    }
                ).ForMember(
                    challenge => challenge.Participants,
                    config =>
                    {
                        config.MapFrom(challenge => challenge.Participants.OrderBy(participant => participant.Timestamp));
                        config.Condition(challenge => challenge.Timeline.State >= ChallengeState.Opened);
                    }
                );
        }
    }
}