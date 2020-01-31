// Filename: ChallengeTimelineExtensions.cs
// Date Created: 2020-01-23
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Infrastructure.Models;

namespace eDoxa.Challenges.Infrastructure.Extensions
{
    public static class ChallengeTimelineExtensions
    {
        public static ChallengeTimelineModel ToModel(this ChallengeTimeline timeline)
        {
            return new ChallengeTimelineModel
            {
                CreatedAt = timeline.CreatedAt,
                Duration = timeline.Duration.Ticks,
                StartedAt = timeline.StartedAt,
                ClosedAt = timeline.ClosedAt
            };
        }
    }
}
