// Filename: TimelineResponseProfile.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.Api.Areas.Challenges.Responses.Profiles
{
    internal sealed class TimelineResponseProfile : Profile
    {
        public TimelineResponseProfile()
        {
            this.CreateMap<ChallengeTimeline, TimelineResponse>()
                .ForMember(timeline => timeline.CreatedAt, config => config.MapFrom(timeline => timeline.CreatedAt))
                .ForMember(timeline => timeline.StartedAt, config => config.MapFrom(timeline => timeline.StartedAt))
                .ForMember(timeline => timeline.EndedAt, config => config.MapFrom(timeline => timeline.EndedAt))
                .ForMember(timeline => timeline.ClosedAt, config => config.MapFrom(timeline => timeline.ClosedAt));
        }
    }
}
