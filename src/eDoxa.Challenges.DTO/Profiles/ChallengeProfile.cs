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
                .ForMember(challenge => challenge.Id, configuration => configuration.MapFrom(challenge => challenge.Id.ToGuid()))
                .ForMember(challenge => challenge.Name, configuration => configuration.MapFrom(challenge => challenge.Name.ToString()))
                .ForMember(challenge => challenge.Game, configuration => configuration.MapFrom(challenge => challenge.Game))
                .ForMember(challenge => challenge.Type, configuration => configuration.MapFrom(challenge => challenge.Settings.Type))
                .ForMember(challenge => challenge.State, configuration => configuration.MapFrom(challenge => challenge.Timeline.State))
                .ForMember(challenge => challenge.LiveMode, configuration => configuration.MapFrom(challenge => challenge.Timeline.LiveMode))
                .ForMember(challenge => challenge.Generated, configuration => configuration.MapFrom(challenge => challenge.Settings.Generated))
                .ForMember(challenge => challenge.Payout, configuration => configuration.MapFrom(challenge => challenge.Payout))
                .ForMember(
                    challenge => challenge.Scoring,
                    configuration =>
                    {
                        configuration.MapFrom(challenge => challenge.Scoring);
                        configuration.Condition(challenge => challenge.Timeline.State >= ChallengeState.Opened);
                    }
                )
                .ForMember(
                    challenge => challenge.LiveData,
                    configuration =>
                    {
                        configuration.MapFrom(challenge => challenge.LiveData);
                        configuration.Condition(challenge => challenge.Timeline.State >= ChallengeState.Opened);
                    }
                ).ForMember(
                    challenge => challenge.Participants,
                    configuration =>
                    {
                        configuration.MapFrom(challenge => challenge.Participants.OrderBy(participant => participant.Timestamp));
                        configuration.Condition(challenge => challenge.Timeline.State >= ChallengeState.Opened);
                    }
                );
        }
    }
}