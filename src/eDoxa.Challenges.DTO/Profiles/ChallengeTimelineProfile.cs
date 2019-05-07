// Filename: ChallengeTimelineProfile.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.DTO.Profiles
{
    internal sealed class ChallengeTimelineProfile : Profile
    {
        public ChallengeTimelineProfile()
        {
            this.CreateMap<ChallengeTimeline, ChallengeTimelineDTO>()
                .ForMember(timeline => timeline.CreatedAt, config => config.MapFrom(timeline => timeline.CreatedAt))
                .ForMember(timeline => timeline.PublishedAt, config => config.MapFrom(timeline => timeline.PublishedAt))
                .ForMember(timeline => timeline.StartedAt, config => config.MapFrom(timeline => timeline.StartedAt))
                .ForMember(timeline => timeline.EndedAt, config => config.MapFrom(timeline => timeline.EndedAt))
                .ForMember(timeline => timeline.ClosedAt, config => config.MapFrom(timeline => timeline.ClosedAt));
        }
    }
}