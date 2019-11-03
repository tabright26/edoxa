// Filename: ChallengeTimelineModelConverter.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Infrastructure.Models;

namespace eDoxa.Challenges.Infrastructure.Profiles.Converters
{
    internal sealed class ChallengeTimelineModelConverter : IValueConverter<ChallengeTimeline, ChallengeTimelineModel>
    {
        
        public ChallengeTimelineModel Convert( ChallengeTimeline sourceMember,  ResolutionContext context)
        {
            return new ChallengeTimelineModel
            {
                CreatedAt = sourceMember.CreatedAt,
                Duration = sourceMember.Duration.Ticks,
                StartedAt = sourceMember.StartedAt,
                ClosedAt = sourceMember.ClosedAt
            };
        }
    }
}
