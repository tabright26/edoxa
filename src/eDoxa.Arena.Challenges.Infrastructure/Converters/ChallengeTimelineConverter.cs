﻿// Filename: TimelineConverter.cs
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

namespace eDoxa.Arena.Challenges.Infrastructure.Converters
{
    internal sealed class ChallengeTimelineConverter : IValueConverter<ChallengeTimeline, TimelineModel>
    {
        [NotNull]
        public TimelineModel Convert([NotNull] ChallengeTimeline sourceMember, [NotNull] ResolutionContext context)
        {
            return new TimelineModel
            {
                Duration = sourceMember.Duration.Ticks,
                StartedAt = sourceMember.StartedAt,
                ClosedAt = sourceMember.ClosedAt
            };
        }
    }
}
