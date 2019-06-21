// Filename: TimelineConverter.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure.Models;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Profiles.Converters
{
    public class TimelineConverter : IValueConverter<ChallengeModel, TimelineViewModel>
    {
        [NotNull]
        public TimelineViewModel Convert([NotNull] ChallengeModel challenge, [NotNull] ResolutionContext context)
        {
            return new TimelineViewModel
            {
                StartedAt = challenge.Timeline.StartedAt,
                EndedAt = challenge.Timeline.StartedAt + TimeSpan.FromTicks(challenge.Timeline.Duration),
                ClosedAt = challenge.Timeline.ClosedAt
            };
        }
    }
}
