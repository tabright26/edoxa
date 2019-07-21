// Filename: ChallengeTimelineViewModelProfile.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Api.Profiles
{
    internal sealed class ChallengeTimelineViewModelProfile : Profile
    {
        public ChallengeTimelineViewModelProfile()
        {
            this.CreateMap<ChallengeTimeline, ChallengeTimelineViewModel>()
                .ForMember(timeline => timeline.CreatedAt, config => config.MapFrom(timeline => timeline.CreatedAt))
                .ForMember(timeline => timeline.StartedAt, config => config.MapFrom(timeline => timeline.StartedAt))
                .ForMember(timeline => timeline.EndedAt, config => config.MapFrom(timeline => timeline.EndedAt))
                .ForMember(timeline => timeline.ClosedAt, config => config.MapFrom(timeline => timeline.ClosedAt));
        }
    }
}
