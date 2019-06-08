// Filename: ChallengeTimelineProfile.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Challenges.Application.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Application.Profiles
{
    internal sealed class ChallengeTimelineProfile : Profile
    {
        public ChallengeTimelineProfile()
        {
            this.CreateMap<ChallengeTimeline, ChallengeTimelineViewModel>()
                .ForMember(timeline => timeline.StartedAt, config => config.MapFrom(timeline => timeline.StartedAt))
                .ForMember(timeline => timeline.EndedAt, config => config.MapFrom(timeline => timeline.EndedAt))
                .ForMember(timeline => timeline.ClosedAt, config => config.MapFrom(timeline => timeline.ClosedAt));
        }
    }
}
