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

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles.Converters
{
    internal sealed class ChallengeTimelineModelConverter : IValueConverter<ChallengeTimeline, ChallengeTimelineModel>
    {
        [NotNull]
        public ChallengeTimelineModel Convert([NotNull] ChallengeTimeline sourceMember, [NotNull] ResolutionContext context)
        {
            return new ChallengeTimelineModel
            {
                Duration = sourceMember.Duration.Ticks,
                StartedAt = sourceMember.StartedAt,
                ClosedAt = sourceMember.ClosedAt
            };
        }
    }
}
